using Dibk.Ftpb.Validation.Application.Enums;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationRule
    {
        public ValidationRuleEnum Id { get; set; }
        public string IdSt { get; set; }
        public string Message { get; set; }
        public ValidationResultSeverityEnum? Messagetype { get; set; }
        public string Xpath { get; set; }
        [JsonIgnore] 
        public string XmlElement { get; set; }
        public string PreCondition { get; set; }
        public string ChecklistReference { get; set; }
    }
}
