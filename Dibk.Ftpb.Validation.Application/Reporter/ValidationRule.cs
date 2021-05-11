using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationRule
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Xpath { get; set; }
        [JsonIgnore] 
        public string XmlElement { get; set; }
        public string PreCondition { get; set; }
        public string ChecklistReference { get; set; }
    }
}
