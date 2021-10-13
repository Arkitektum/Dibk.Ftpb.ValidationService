using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Dibk.Ftpb.Validation.Application.Tests.Utils
{
    public class TestHelper
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        public static string GetXmlWithoutSpaces(string formAsXml)
        {
            if (string.IsNullOrEmpty(formAsXml))
                return null;

            Regex Parser = new Regex(@">\s*<");
            var xml = Parser.Replace(formAsXml, "><");
            xml.Trim();
            return xml;
        }

        public static string DebugValidatorFormReference(string reference)
        {
            var validatorPath = string.Empty;

            if (reference.Contains("."))
            {
                var ValidationEnumsNumber = reference.Split(".");

                for (int i = 0; i < ValidationEnumsNumber.Length; i++)
                {
                    if (int.TryParse(ValidationEnumsNumber[i], out int enumNumber))
                    {
                        if (enumNumber < 100)
                        {
                            string validatorText;
                            var validatorEnum = GetEnumFromValidationId<EntityValidatorEnum>(enumNumber.ToString());
                            string stringValue = validatorEnum.ToString();
                            if (!int.TryParse(stringValue, out int theNumber))
                            {
                                validatorText = ValidationEnumsNumber.Length == i + 1 ? $"/({stringValue})?" : $"/{stringValue}";
                            }
                            else
                            {
                                validatorText = $"/Enum:'{theNumber}'";
                            }

                            validatorPath = $"{validatorPath}{validatorText}";
                        }
                    }
                }
            }
            return validatorPath;
        }

        //https://stackoverflow.com/a/4367868
        public static T GetEnumFromValidationId<T>(string validatorId) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(EntityValidatorEnumerationAttribute)) is EntityValidatorEnumerationAttribute attribute)
                {
                    if (attribute.ValidatorId == validatorId)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == validatorId)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(validatorId));
            // Or return default(T);
        }
        //TODO how to convert new entities to main XML/parse to the main class?
        public static JObject GetJsonForPostman(string xmlData, object form = null)
        {
            string data = xmlData;
            if (form != null)
                data = SerializeUtil.Serialize(form);
            ValidationInput validationInput = new ValidationInput()
            {
                FormData = GetXmlWithoutSpaces(data)
            };
            var validateFormv2JObject = JObject.FromObject(validationInput);
            return validateFormv2JObject;
        }

    }
}
