using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
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
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            
            var noko = fi.GetCustomAttributes(typeof(EnumerationAttribute), false) as EnumerationAttribute[];

            var xmlNode = string.Empty;
            if (noko != null && noko.Any())
            {
                xmlNode= noko.First().XmlNode;
            }

            return xmlNode;
            //DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            //if (attributes != null && attributes.Any())
            //{
            //    return attributes.First().Description;
            //}
            //return value.ToString();
        }
    }
}
