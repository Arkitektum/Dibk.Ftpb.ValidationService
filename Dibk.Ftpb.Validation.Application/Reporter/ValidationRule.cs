using Dibk.Ftpb.Validation.Application.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationRule
    {
        public string id;
        public string xpath;
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidationResultEnum validationResult;
        //public string message;
        public string preCondition;
        public string checklistReference;
    }
}
