using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class BygningstypeValidator : KodelisteValidatorV2
    {
        public BygningstypeValidator(IList<EntityValidatorNode> entityValidatorTree, ICodeListService codeListService)
            : base(entityValidatorTree, null, FtbKodeListeEnum.Bygningstype, RegistryType.Byggesoknad, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
