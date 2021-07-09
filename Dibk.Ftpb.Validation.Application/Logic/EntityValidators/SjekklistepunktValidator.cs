using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SjekklistepunktValidator : KodelisteValidator
    {
        public SjekklistepunktValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId, FtbKodeListeEnum.Sjekklistepunkttype, codeListService)
        {
            _codeListService = codeListService;
        }



    }
}
