using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class NaeringsgruppeValidator : KodelisteValidator
    {
        public NaeringsgruppeValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId,FtbKodeListeEnum.Naeringsgruppe, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
