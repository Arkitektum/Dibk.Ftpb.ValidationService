using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Utils
{
    public class Helpers
    {
        /// <summary>
        /// check if the object and properties are empty
        /// it doesn't work with the new mapping structure because the "ModelData" and "DataModelXpath" properties always have data 
        /// </summary>
        /// <param name="mainObject"></param>
        /// <returns></returns>
        public static bool ObjectIsNullOrEmpty(object mainObject)
        {
            if (mainObject == null) return true;

            // Simple types
            if (IsSimple(mainObject.GetType()))
            {
                var stringValue = mainObject.ToString();
                return string.IsNullOrEmpty(stringValue);
            }

            //Complex types - Arrays
            if (mainObject.GetType().IsArray || mainObject.GetType().IsEnumerableType() || mainObject.GetType().IsListType())
            {
                foreach (dynamic objectItem in (IEnumerable)mainObject)
                {
                    var isNullOrEmpty = ObjectIsNullOrEmpty(objectItem);
                    if (!isNullOrEmpty)
                        return false;
                }
                return true;

            }

            //Complex type - Child properties
            if (mainObject.GetType().GetProperties().Any())
            {
                // TODO test this, returns false when in xml all nodes are "xsi:nil="true" "
                var props = mainObject.GetType().GetProperties().Where(p => !p.Name.EndsWith("Specified", StringComparison.CurrentCultureIgnoreCase) && !p.Name.Equals("DataModelXpath", StringComparison.CurrentCultureIgnoreCase));

                foreach (var propertyInfo in props)
                {
                    var isNullOrEmpty = ObjectIsNullOrEmpty(propertyInfo.GetValue(mainObject, null));
                    if (!isNullOrEmpty)
                        return false;
                }
            }

            return true;
        }

        private static bool IsSimple(Type type)
        {
            // https://stackoverflow.com/questions/863881/how-do-i-tell-if-a-type-is-a-simple-type-i-e-holds-a-single-value
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // nullable type, check if the nested type is simple.
                return IsSimple(type.GetGenericArguments()[0]);
            }
            return type.IsPrimitive
                   || type.IsEnum
                   || type == typeof(string)
                   || type == typeof(decimal);
        }

        public static string ReplaceCurlyBracketInXPath(int index, string xPath)
        {
            string newXPath = null;
            if (!string.IsNullOrEmpty(xPath))
            {
                var searchText = "{0}";
                int lastIndex = xPath.LastIndexOf(searchText);
                if (lastIndex >= 0)
                {
                    newXPath = xPath.Remove(lastIndex, searchText.Length).Insert(lastIndex, $"[{index}]");
                }
                else
                {
                    newXPath = xPath;
                }
            }
            return newXPath;
        }

        public static string DebugValidatorFormReference(string reference)
        {
            var validatorPath = string.Empty;

            if (reference.Contains("."))
            {
                var validationEnumsNumber = reference.Split(".");

                string[] ruleIdNumber = new string[] { };

                if (int.TryParse(validationEnumsNumber[0], out int number))
                {
                    ruleIdNumber = number >= 10000 ? validationEnumsNumber.Skip(2).ToArray() : validationEnumsNumber;
                }

                var index = ruleIdNumber.Length;

                for (int i = 0; i < index; i++)
                {
                    if (int.TryParse(ruleIdNumber[i], out int enumNumber))
                    {
                        string validatorText;
                        string stringValue;

                        if (index == i + 2)
                        {
                            var validatorEnum = EnumHelper.GetEnumFromFieldNameId(enumNumber.ToString());
                            stringValue = validatorEnum.ToString();
                        }
                        else if (index == i + 1)
                        {
                            var validatorEnum = EnumHelper.GetEnumFromValidationRuleId(enumNumber.ToString());
                            stringValue = validatorEnum.ToString();
                        }
                        else
                        {
                            var validatorEnum = EnumHelper.GetEnumFromValidationId(enumNumber.ToString());
                            stringValue = validatorEnum.ToString();
                        }

                        if (!int.TryParse(stringValue, out int theNumber))
                        {
                            validatorText = ruleIdNumber.Length == i + 1 ? $" rule: {stringValue}" : $"/{stringValue}";
                        }
                        else
                        {
                            validatorText = $"/Enum:'{theNumber}'";
                        }

                        validatorPath = $"{validatorPath}{validatorText}";
                    }
                }
            }
            return validatorPath;
        }

        //public static List<ATILSjekklistekravEnum> GetSjekklistekravEnumFromIndex(string checklistNumber)
        //{
        //    var enumsForChecklistNumber = new List<ATILSjekklistekravEnum>();

        //    var enumList = Enum.GetValues(typeof(ATILSjekklistekravEnum)).OfType<ATILSjekklistekravEnum>().ToList();
        //    foreach (var theEnum in enumList)
        //    {
        //        FieldInfo fieldInfo = theEnum.GetType().GetField(theEnum.ToString());
        //        var entityValidatorNumber = string.Empty;

        //        if (fieldInfo?.GetCustomAttributes(typeof(SjekklistekravEnumerationAttribute), false) is SjekklistekravEnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any()
        //            && enumerationAttributes.First().SjekklistepunktVerdi.Equals(checklistNumber))
        //        {
        //            enumsForChecklistNumber.Add(theEnum);
        //        }
        //    }

        //    return enumsForChecklistNumber;
        //}
    }
}
