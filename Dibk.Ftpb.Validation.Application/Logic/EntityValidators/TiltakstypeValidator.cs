using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakstypeValidator : KodelisteValidator
    {
        public TiltakstypeValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid, ICodeListService codeListService)
            : base(entityValidatorTree, nodeid, FtbKodeListeEnum.Tiltakstype, RegistryType.Byggesoknad, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
