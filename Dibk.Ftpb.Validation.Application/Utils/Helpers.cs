using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Utils
{
    public class Helpers
    {
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
            if (mainObject.GetType().IsArray)
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
                var props = mainObject.GetType().GetProperties().Where(p => !p.Name.EndsWith("Specified"));

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
        public static string GetEnumXmlNodeName(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            
            var noko = fi.GetCustomAttributes(typeof(EnumerationAttribute), false) as EnumerationAttribute[];

            var xmlNode = string.Empty;
            if (noko != null && noko.Any())
            {
                xmlNode= noko.First().XmlNode;
            }

            return xmlNode;
        }
        public static string GetEnumValidatorRuleNumber(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var ruleNumber = string.Empty;

            if (fieldInfo?.GetCustomAttributes(typeof(EnumerationAttribute), false) is EnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any())
            {
                ruleNumber= enumerationAttributes.First().ValidatorId;
            }
            return ruleNumber;

        }

        public static string GetNodeNamefromClass<T>(Expression<Func<T>> action)
        {
            var path = action.Body.ToString();
            var index = path.LastIndexOf(".");
            var nodeName = index>0? path.Remove(0, index + 1):"";
            return nodeName;
        }
        public static string GetFullClassPath<T>(Expression<Func<T>> action)
        {

            var path = action.Body.ToString();
            int index = path.LastIndexOf("form.", StringComparison.InvariantCultureIgnoreCase);

            var classPathTemp = index > 0 ? path.Remove(0, index) : path.Remove(0, path.IndexOf(")", StringComparison.Ordinal) + 2);

            string classPath = String.Empty;
            int index2 = classPathTemp.LastIndexOf(".form", StringComparison.InvariantCultureIgnoreCase);


            if (index2 > 0 && classPathTemp.Length == index2 + 5 && !classPathTemp.Contains("form."))
            {
                classPath = classPathTemp.Remove(0, index2 + 1);
            }
            else
            {
                classPath = classPathTemp;
            }

            classPath = classPath.Replace(".", "/");
            return classPath;
        }
    }
}
