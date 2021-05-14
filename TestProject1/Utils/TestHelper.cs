﻿using System.Text.RegularExpressions;

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
    }
}
