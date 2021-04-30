using Arkitektum.XmlSchemaValidator.Config;
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
using System.Configuration;

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

            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient<IInputDataService, InputDataService>();
            services.AddTransient<IXsdValidationService, XsdValidationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                     
            }

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
                .Enrich.WithWebApiControllerName()
                .Enrich.WithWebApiActionName()
                .Enrich.WithMvcControllerName()
                .Enrich.WithMvcActionName()
                .Enrich.WithMachineName()
                /*.Enrich.WithBasicAuthUserName()*/
                .Enrich.WithHttpRequestClientHostIP()
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
