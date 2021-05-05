using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationRule
    {
        public string xpath;
        public string id;
        [JsonConverter(typeof(StringEnumConverter))]
        public ValidationResultEnum validationResult;
        public string message;
        public string preCondition;
        public string checklistReference;
    }
}
