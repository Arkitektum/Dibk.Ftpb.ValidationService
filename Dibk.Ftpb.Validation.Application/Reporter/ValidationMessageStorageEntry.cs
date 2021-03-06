using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageStorageEntry
    {
        //public ValidationRuleEnum Id { get; set; }
        public string Rule { get; set; }
        public string XPath { get; set; }
        public string LanguageCode { get; set; }
        public string Message { get; set; }
        public ValidationResultSeverityEnum? Messagetype { get; set; }
        public string ChecklistReference { get; set; }
        public string DataFormatVersion { get; set; }
        public string DataForm { get; set; }
        
        //public ValidationResultSeverityEnum ValidationResultSeverity { get; set; }
    }
}
