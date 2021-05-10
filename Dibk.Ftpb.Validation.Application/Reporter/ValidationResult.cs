using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationResult
    {
        public ValidationResult()
        { }
        public List<ValidationMessage> ValidationMessages { get; set; }
        public List<ValidationRule> ValidationRules { get; set; }
    }
}
