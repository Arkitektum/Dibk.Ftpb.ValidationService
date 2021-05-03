using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomValidator
    {
        public EiendomValidator()
        {
            
        }
        public List<ValidationRule> Validate(string context, IEnumerable<Eiendom> eiendoms)
        {
            var entityRules = EntityRules();

            return new List<ValidationRule>();
        }

        public List<ValidationRule> EntityRules()
        {
            return new List<ValidationRule>();
        }
    }
}
