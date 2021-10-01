﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Dibk.Ftpb.Validation.Application.Reporter
{

    public class Validations
    {
        public int Errors { get; set; }
        public int Warnings { get; set; }
        public string Soknadtype { get; set; }
        public Validations()
        { }
        public List<ValidationMessage> messages { get; set; }
        public List<ValidationRule> rulesChecked { get; set; }

        [JsonIgnore]
        public List<ValidationMessage> ValidationMessages { get; set; }
        [JsonIgnore]
        public List<ValidationRule> ValidationRules { get; set; }
    }
}
