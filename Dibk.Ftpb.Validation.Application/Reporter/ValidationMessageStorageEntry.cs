using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageStorageEntry
    {
        public string Id { get; set; }
        public string XPath { get; set; }
        public string LanguageCode { get; set; }
        public string Message { get; set; }
        public string ChecklistReference { get; set; }
        public ValidationResultSeverityEnum ValidationResultSeverity { get; set; }
    }
}
