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
        private static readonly Assembly _assembly = Assembly.Load("Dibk.Ftpb.Validation.Application");

        public static void AddXmlSchemaValidator(this IServiceCollection services)
        {
            services.AddXmlSchemaValidator(options =>
            {
                options.AddSchema(DataType.ArbeidstilsynetsSamtykke, GetXsdResourceStream("arbeidstilsynetsSamtykke.xsd"));
                options.AddSchema(DataType.ArbeidstilsynetsSamtykke2, GetXsdResourceStream("arbeidstilsynetsSamtykke2.xsd"));

                options.CacheFiles = false;
            });
        }

        private static Stream GetXsdResourceStream(string fileName)
        {
            var name = _assembly.GetManifestResourceNames().SingleOrDefault(name => name.EndsWith(fileName));

            return name != null ? _assembly.GetManifestResourceStream(name) : null;
        }
    }
}
