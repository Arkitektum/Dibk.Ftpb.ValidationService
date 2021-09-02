
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakshaverValidator : AktoerValidator
    {
        public TiltakshaverValidator(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator, 
               IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
            : base(entityValidatorTree, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, codeListService)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;
        }
    }
}
