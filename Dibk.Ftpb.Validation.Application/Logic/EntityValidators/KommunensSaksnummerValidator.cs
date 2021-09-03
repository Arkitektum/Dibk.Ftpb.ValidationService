using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    class KommunensSaksnummerValidator : SaksnummerValidator
    {
        public KommunensSaksnummerValidator(IList<EntityValidatorNode> entityValidatorTree)
            : base(entityValidatorTree)
        {
        }
    }
}