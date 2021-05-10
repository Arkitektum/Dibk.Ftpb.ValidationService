using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationResult
    {
        public List<ValidationRule> ValidationRules { get; set; }
        public List<ValidationMessage> ValidationMessages { get; set; }
    }
}
