using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    class ArbeidstilsynetsSaksnummerValidator : SaksnummerValidator
    {
        public ArbeidstilsynetsSaksnummerValidator(IList<EntityValidatorNode> entityValidatorTree)
            : base(entityValidatorTree)
        {
        }
    }
}