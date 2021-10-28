﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Models.Standard;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
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

        public static Dictionary<string, string> DebugValidatorFormReference(List<ValidationRule> reference)
        {
            var validatorXpathList = new Dictionary<string, string>();
            foreach (var validationRule in reference)
            {
                var validtorXpath =Helpers.DebugValidatorFormReference(validationRule.Id);
                validatorXpathList.Add(validationRule.Id, $"{validtorXpath} -'XmlElement':{validationRule.XmlElement}, 'XpathField':{validationRule.XpathField}, 'Rule':{validationRule.Rule}");
            }
            return validatorXpathList;
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

            //throw new ArgumentException("Not found.", nameof(validatorId));
            return default(T);
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
