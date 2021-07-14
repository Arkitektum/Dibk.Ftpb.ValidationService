using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Tests.Utils
{
    public class TestHelper
    {
        public static string GetXmlWithoutSpaces(string formAsXml)
        {
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
                    typeof(EnumerationAttribute)) is EnumerationAttribute attribute)
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
    }
}
