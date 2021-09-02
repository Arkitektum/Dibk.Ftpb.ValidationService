using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarligSoekerValidator : AktoerValidator
    {
        public AnsvarligSoekerValidator(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator,
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