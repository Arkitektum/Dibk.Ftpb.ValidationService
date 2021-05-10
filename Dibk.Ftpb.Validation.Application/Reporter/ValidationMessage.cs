using Dibk.Ftpb.Validation.Application.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessage
    {
        public string Reference { get; set; }
        public string Message { get; set; }
        public string Xpath { get; set; }
        //[JsonConverter(typeof(StringEnumConverter))]
        public ValidationResultEnum? ValidationResult { get; set; }
        //public string PreCondition { get; set; }
        public string ChecklistReference { get; set; }
        [JsonIgnore]
        public List<string> MessageParameters { get; set; }
    }
}
