using Dibk.Ftpb.Validation.Application.Enums;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessage
    {
        //public ValidationRuleEnum Reference { get; set; }
        public string Rule { get; set; }
        public string Reference { get; set; }
        public string Message { get; set; }
        public ValidationResultSeverityEnum? Messagetype { get; set; }
        
        public string XpathField { get; set; }
        public string PreCondition { get; set; }
        [JsonIgnore] 
        public string ChecklistReference { get; set; }
        [JsonIgnore]
        public string[] MessageParameters { get; set; }
    }
}
