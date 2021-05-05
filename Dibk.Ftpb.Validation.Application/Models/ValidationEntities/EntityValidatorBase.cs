using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public abstract class EntityValidatorBase
    {
        protected List<ValidationRule> ValidationRules = new List<ValidationRule>();
        public EntityValidatorBase()
        {
            ValidationRules = new List<ValidationRule>();
        }
    }
}
