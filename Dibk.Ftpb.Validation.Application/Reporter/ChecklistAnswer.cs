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
        public string ChecklistReference { get; set; }
        public string ChecklistQuestion { get; set; }
        public bool YesNo { get; set; }
        
        [XmlArrayItem("validationRuleId")]
        public List<string> SupportingDataValidationRuleId { get; set; }
        
        [XmlArrayItem("xpathField")]
        public List<string> SupportingDataXpathField { get; set; }
        public string Documentation { get; set; }

    }
    public class PrefillChecklist
    {
        public List<ChecklistAnswer> ChecklistAnswer { get; set; }
    }
}
