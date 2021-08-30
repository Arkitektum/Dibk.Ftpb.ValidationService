using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

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
        public static string GetEnumXmlNodeName(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            var enumerationAttributes = fi.GetCustomAttributes(typeof(EntityValidatorEnumerationAttribute), false) as EntityValidatorEnumerationAttribute[];

            var xmlNode = string.Empty;
            if (enumerationAttributes != null && enumerationAttributes.Any())
            {
                xmlNode = enumerationAttributes.First().XmlNode;
            }

            return xmlNode;
        }
        public static string GetEnumEntityValidatorNumber(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var entityValidatorNumber = string.Empty;

            if (fieldInfo?.GetCustomAttributes(typeof(EntityValidatorEnumerationAttribute), false) is EntityValidatorEnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any())
            {
                entityValidatorNumber = enumerationAttributes.First().ValidatorId;
            }
            return entityValidatorNumber;

        }

        public static string GetEnumFieldNameNumber(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var entityValidatorNumber = string.Empty;

            if (fieldInfo?.GetCustomAttributes(typeof(FieldNameEnumerationAttribute), false) is FieldNameEnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any())
            {
                entityValidatorNumber = enumerationAttributes.First().FieldNameId;
            }
            return entityValidatorNumber;

        }

        public static string GetEnumValidationRuleType(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var validationRuleNumber = string.Empty;

            if (fieldInfo?.GetCustomAttributes(typeof(ValidationRuleTypeEnumerationAttribute), false) is ValidationRuleTypeEnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any())
            {
                validationRuleNumber = enumerationAttributes.First().ValidationRuleTypeId;
            }
            return validationRuleNumber;

        }

        public static List<ATILSjekklistekravEnum> GetSjekklistekravEnumFromIndex(string checklistNumber)
        {
            var enumsForChecklistNumber = new List<ATILSjekklistekravEnum>();

            var enumList = Enum.GetValues(typeof(ATILSjekklistekravEnum)).OfType<ATILSjekklistekravEnum>().ToList();
            foreach (var theEnum in enumList)
            {
                FieldInfo fieldInfo = theEnum.GetType().GetField(theEnum.ToString());
                var entityValidatorNumber = string.Empty;

                if (fieldInfo?.GetCustomAttributes(typeof(SjekklistekravEnumerationAttribute), false) is SjekklistekravEnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any()
                    && enumerationAttributes.First().SjekklistepunktVerdi.Equals(checklistNumber))
                {
                    enumsForChecklistNumber.Add(theEnum);
                }
            }

            return enumsForChecklistNumber;
        }




        public static string GetNodeNamefromClass<T>(Expression<Func<T>> action)
        {
            var path = action.Body.ToString();
            var index = path.LastIndexOf(".");
            var nodeName = index > 0 ? path.Remove(0, index + 1) : "";
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
