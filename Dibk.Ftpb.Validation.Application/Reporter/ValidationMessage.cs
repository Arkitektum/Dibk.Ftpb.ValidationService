using Dibk.Ftpb.Validation.Application.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessage
    {
        public ValidationRuleEnum Reference { get; set; }
        public string ReferenceSt { get; set; }
        public string RulePath { get; set; }
        public string Message { get; set; }
        public ValidationResultSeverityEnum? Messagetype { get; set; }
        
        public string XpathField { get; set; }
        //[JsonConverter(typeof(StringEnumConverter))]
        //public string PreCondition { get; set; }
        public string ChecklistReference { get; set; }
        [JsonIgnore]
        public string[] MessageParameters { get; set; }
    }
}
