using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ChecklistAnswer
    {
        public string checklistReference { get; set; }
        public string checklistQuestion { get; set; }
        public bool yesNo { get; set; }
        //public SupportingDataValidationRuleIds supportingDataValidationRuleIds { get; set; }
        [XmlArrayItem("validationRuleId")]
        public List<string> supportingDataValidationRuleId { get; set; }
        [XmlArrayItem("xpathField")]
        public List<string> supportingDataXpathField { get; set; }
        public string documentation { get; set; }

    }
    public class PrefillChecklist
    {
        public List<ChecklistAnswer> ChecklistAnswers { get; set; }
    }

    //public class SupportingDataXpathFields
    //{
    //    public List<XpathField> XpathField { get; set; }
    //}

    ////public class SupportingDataValidationRuleIds
    ////{
    ////    public List<ValidationRuleId> ValidationRuleId { get; set; }
    ////}
    ////public class SupportingDataValidationRuleIds
    ////{
    ////    public List<ValidationRuleObject> validationRuleId { get; set; }
    ////    //public List<ValidationRuleObject> validationRuleObjects { get; set; }
    ////}
    //public class XpathField
    //{
    //    public string Field { get; set; }
    //}
    //public class ValidationRuleId
    //{
    //    public string validationRuleId { get; set; }
    //}
}
