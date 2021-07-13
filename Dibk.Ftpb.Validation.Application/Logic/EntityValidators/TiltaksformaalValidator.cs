using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltaksformaalValidator : KodelisteValidator
    {
        public TiltaksformaalValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeid, ICodeListService codeListService)
            : base(entityValidatorTree, nodeid, FtbKodeListeEnum.Tiltaksformaal,RegistryType.Byggesoknad, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
