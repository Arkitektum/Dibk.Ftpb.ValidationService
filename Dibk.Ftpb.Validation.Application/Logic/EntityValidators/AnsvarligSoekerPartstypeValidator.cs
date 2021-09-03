using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarligSoekerPartstypeValidator : KodelisteValidator
    {
        public AnsvarligSoekerPartstypeValidator(IList<EntityValidatorNode> entityValidatorTree, ICodeListService codeListService)
            : base(entityValidatorTree,  FtbKodeListeEnum.Partstype, RegistryType.Byggesoknad, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
