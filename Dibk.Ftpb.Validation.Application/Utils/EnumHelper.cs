using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

namespace Dibk.Ftpb.Validation.Application.Utils
{
    public class EnumHelper
    {
        public static ValidationRuleEnum GetEnumFromValidationRuleId(string validationRuleTypeId)
        {
            foreach (var field in typeof(ValidationRuleEnum).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,typeof(ValidationRuleTypeEnumerationAttribute)) is ValidationRuleTypeEnumerationAttribute attribute)
                {
                    if (attribute.ValidationRuleTypeId == validationRuleTypeId)
                        return (ValidationRuleEnum) field.GetValue(null);
                }
                else
                {
                    if (field.Name == validationRuleTypeId)
                        return (ValidationRuleEnum)field.GetValue(null);
                }
            }
            //Log
            return default;
        }

        public static string GetRuleNumberFromValidationRuleEmum(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var validationRuleNumber = string.Empty;

            if (fieldInfo?.GetCustomAttributes(typeof(ValidationRuleTypeEnumerationAttribute), false) is ValidationRuleTypeEnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any())
            {
                validationRuleNumber = enumerationAttributes.First().ValidationRuleTypeId;
            }
            return validationRuleNumber;
        }

        public static FieldNameEnum GetEnumFromFieldNameId(string FieldNameId)
        {
            foreach (var field in typeof(FieldNameEnum).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(FieldNameEnumerationAttribute)) is FieldNameEnumerationAttribute attribute)
                {
                    if (attribute.FieldNameId == FieldNameId)
                        return (FieldNameEnum)field.GetValue(null);
                }
                else
                {
                    if (field.Name == FieldNameId)
                        return (FieldNameEnum)field.GetValue(null);
                }
            }
            //Log
            return default;
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

        //https://stackoverflow.com/a/4367868
        public static EntityValidatorEnum GetEnumFromValidationId(string validatorId)
        {
            foreach (var field in typeof(EntityValidatorEnum).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(EntityValidatorEnumerationAttribute)) is EntityValidatorEnumerationAttribute attribute)
                {
                    if (attribute.ValidatorId == validatorId)
                        return (EntityValidatorEnum)field.GetValue(null);
                }
                else
                {
                    if (field.Name == validatorId)
                        return (EntityValidatorEnum)field.GetValue(null);
                }
            }
            //Log
            return default;
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

        public static string GetCodelistUrl(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            var codelistUrl = string.Empty;

            if (fieldInfo?.GetCustomAttributes(typeof(CodelistEnumerationAttribute), false) is CodelistEnumerationAttribute[] enumerationAttributes && enumerationAttributes.Any())
            {
                codelistUrl = enumerationAttributes.First().CodelistUrl;
            }
            return codelistUrl;
        }
    }
}
