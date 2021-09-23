using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltaksformaalValidator : KodelisteValidatorV2
    {
        public TiltaksformaalValidator(IList<EntityValidatorNode> entityValidatorTree,  ICodeListService codeListService)
            : base(entityValidatorTree, null, FtbKodeListeEnum.tiltaksformal,RegistryType.Byggesoknad, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
