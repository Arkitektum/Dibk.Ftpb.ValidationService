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
                            var enumDisplayStatus = (EntityValidatorEnum)enumNumber;

                            string stringValue = enumDisplayStatus.ToString();
                            if (!int.TryParse(stringValue, out int theNumber))
                            {
                                validatorPath = ValidationEnumsNumber.Length == i + 1 ? $"{validatorPath}/({stringValue})?" : $"{validatorPath}/{stringValue}";
                            }
                            else
                            {
                                validatorPath = $"{validatorPath}/Enum:'{theNumber}'";
                            }
                        }
                    }

                }
                foreach (var validatorNumber in ValidationEnumsNumber)
                {

                }
            }
            return validatorPath;
        }
    }
}
