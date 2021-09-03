using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakshaverKontaktpersonValidator : KontaktpersonValidator
    {
        public TiltakshaverKontaktpersonValidator(IList<EntityValidatorNode> entityValidatorTree) : base(entityValidatorTree)
        {
        }
    }
}
