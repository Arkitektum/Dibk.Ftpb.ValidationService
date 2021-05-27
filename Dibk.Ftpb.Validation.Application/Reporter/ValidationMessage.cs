using Dibk.Ftpb.Validation.Application.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessage
    {
        public ValidationRuleEnum Reference { get; set; }
        public string Message { get; set; }
        public ValidationResultSeverityEnum? Messagetype { get; set; }
        
        public string XpathField { get; set; }
        //[JsonConverter(typeof(StringEnumConverter))]
        //public ValidationResultEnum? ValidationResult { get; set; }
        //public string PreCondition { get; set; }
        public string ChecklistReference { get; set; }
        [JsonIgnore]
        public List<string> MessageParameters { get; set; }
    }
}
