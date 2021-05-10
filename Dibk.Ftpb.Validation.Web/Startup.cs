using Arkitektum.XmlSchemaValidator.Config;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Process;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Web.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Dibk.Ftpb.Api.Validation", Version = "v1" });
            });

            services.AddXmlSchemaValidator();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                //options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IInputDataService, InputDataService>();
            services.AddTransient<IXsdValidationService, XsdValidationService>();
            services.AddTransient<IValidationOrchestrator, ValidationOrchestrator>();
            services.AddTransient<IMunicipalityValidator, MunicipalityValidator>();
            services.AddScoped<ArbeidstilsynetsSamtykke2_45957_Validator>();

            services.AddAzureAppConfiguration();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSerilogRequestLogging();

            ConfigureLogging();

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dibk.Ftpb.Api.Validation v1"));

            app.UseXmlSchemaValidator();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private void ConfigureLogging()
        {
            var logLevel = LogEventLevel.Debug;
            var elasticSearchUrl = Configuration["Serilog:ConnectionUrl"];
            var elasticUsername = Configuration["Serilog:Username"];
            var elasticPassword = Configuration["Serilog:Password"];
            var elasticIndexFormat = Configuration["Serilog:IndexFormat"];



            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(logLevel)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                /* Berre tilgjengeleg for .net framewok via Serilog.Classic pakkene :grubble-fjes: */
                //.Enrich.WithWebApiControllerName()
                //.Enrich.WithWebApiActionName()
                //.Enrich.WithHttpRequestClientHostIP()
                /*.Enrich.WithCorrelationIdHeader()*/
                .WriteTo.Trace(outputTemplate: "{Timestamp:HH:mm:ss.fff} {SourceContext} [{Level}] {ArchiveReference} {Message}{NewLine}{Exception}")
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchUrl))
                {
                    DetectElasticsearchVersion = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    AutoRegisterTemplate = true,
                    ModifyConnectionSettings = x => x.BasicAuthentication(elasticUsername, elasticPassword),
                    IndexFormat = elasticIndexFormat
                })
                .CreateLogger();          
        }
    }
}
