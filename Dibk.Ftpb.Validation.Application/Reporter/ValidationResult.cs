using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationResult
    {
        public int Errors { get; set; }
        public int Warnings { get; set; }
        public string Soknadtype { get; set; }

        public List<ValidationMessage> messages { get; set; }
        public List<ValidationRule> rulesChecked { get; set; }
        public List<ChecklistAnswer> PrefillChecklist { get; set; }

        [JsonIgnore]
        public List<ValidationMessage> ValidationMessages { get; set; }
        [JsonIgnore] 
        public List<ValidationRule> ValidationRules { get; set; }
        
        public ValidationResult()
        { }
    }

    //public class Messages
    //{
    //    public List<ValidationMessage> ValidationMessage { get; set; }
    //}
    //public class RulesChecked
    //{
    //    public List<ValidationRule> ValidationRule { get; set; }
    //}
}
