using Dibk.Ftpb.Validation.Application.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationRule
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Xpath { get; set; }
        public string PreCondition { get; set; }
        public string ChecklistReference { get; set; }
    }
}
