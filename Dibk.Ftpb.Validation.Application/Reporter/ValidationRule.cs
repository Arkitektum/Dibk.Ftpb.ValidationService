using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationRule
    {
        public string Id { get; set; }
        public string Rule { get; set; }
        public string Message { get; set; }
        public ValidationResultSeverityEnum? Messagetype { get; set; }
        public string XpathField { get; set; }
        [JsonIgnore] 
        public string XmlElement { get; set; }
        public string PreCondition { get; set; }
        //public string ChecklistReference { get; set; }
    }
}
