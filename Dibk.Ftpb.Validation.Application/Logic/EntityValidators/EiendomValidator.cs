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
        public List<ValidationMessage> Validate(string context, IEnumerable<Eiendom> eiendoms)
        {
            return new List<ValidationMessage>();
        }

        public List<ValidationMessage> EntityRules()
        {
            return new List<ValidationMessage>();
        }
    }
}
