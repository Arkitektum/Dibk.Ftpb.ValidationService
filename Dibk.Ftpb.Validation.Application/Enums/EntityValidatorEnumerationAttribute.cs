using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EntityValidatorEnumerationAttribute : Attribute
    {
        public string XmlNode { get; set; }
        public string ValidatorId { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SjekklistekravEnumerationAttribute : Attribute
    {
        public string SjekklistepunktVerdi { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class FieldNameEnumerationAttribute : Attribute
    {
        public string FieldNameId { get; set; }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ValidationRuleTypeEnumerationAttribute : Attribute
    {
        public string ValidationRuleTypeId { get; set; }
    }
}
