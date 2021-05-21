using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationResult
    {
        public int Errors { get; set; }
        public int Warnings { get; set; }
        public List<ValidationMessage> messages { get; set; }
        public List<ValidationRule> rulesChecked { get; set; }
        public ValidationResult()
        { }
        [JsonIgnore]
        public List<ValidationMessage> ValidationMessages { get; set; }
        [JsonIgnore] 
        public List<ValidationRule> ValidationRules { get; set; }
    }
}
