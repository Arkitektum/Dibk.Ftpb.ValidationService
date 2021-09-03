using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakshaverPartstypeValidator : KodelisteValidator
    {
        public TiltakshaverPartstypeValidator(IList<EntityValidatorNode> entityValidatorTree, ICodeListService codeListService)
            : base(entityValidatorTree, FtbKodeListeEnum.Partstype, RegistryType.Byggesoknad, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
