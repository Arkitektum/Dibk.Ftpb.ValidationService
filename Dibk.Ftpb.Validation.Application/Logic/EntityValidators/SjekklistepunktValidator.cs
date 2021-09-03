using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SjekklistepunktValidator : KodelisteValidator
    {
        public SjekklistepunktValidator(IList<EntityValidatorNode> entityValidatorTree, ICodeListService codeListService)
            : base(entityValidatorTree, null, FtbKodeListeEnum.Sjekklistepunkttype, RegistryType.Arbeidstilsynet, codeListService)
        {
            _codeListService = codeListService;
        }
    }
}
