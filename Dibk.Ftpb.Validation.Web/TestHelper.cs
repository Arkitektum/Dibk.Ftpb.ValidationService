using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Web
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
    }
}
