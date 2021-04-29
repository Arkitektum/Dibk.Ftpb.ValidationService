using Arkitektum.XmlSchemaValidator.Config;
using Dibk.Ftpb.Validation.Application.Constants;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dibk.Ftpb.Validation.Web.Config
{
    public static class XsdValidatorConfig
    {
        public static void AddXmlSchemaValidator(this IServiceCollection services)
        {
            services.AddXmlSchemaValidator(options =>
            {
                options.AddSchema(
                    DataType.ArbeidstilsynetsSamtykke.ToString(),
                    GetXsdResourceStream("arbeidstilsynetsSamtykke2.xsd")
                );

                options.CacheDurationDays = 30;
            });
        }

        private static Stream GetXsdResourceStream(string fileName)
        {
            var assembly = Assembly.Load("Dibk.Ftpb.Validation.Application");
            var name = assembly.GetManifestResourceNames().SingleOrDefault(name => name.EndsWith(fileName));

            return name != null ? assembly.GetManifestResourceStream(name) : null;
        }
    }
}
