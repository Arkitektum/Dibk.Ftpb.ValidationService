using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class BygningstypeValidator : KodelisteValidator
    {
        public BygningstypeValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId, FtbKodeListeEnums.Bygningstype, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
