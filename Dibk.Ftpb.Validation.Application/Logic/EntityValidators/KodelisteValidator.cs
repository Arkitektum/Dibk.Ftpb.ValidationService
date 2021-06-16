using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class KodelisteValidator : EntityValidatorBase, IKodelisteValidator
    {
        protected ICodeListService _codeListService;

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KodelisteValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, ICodeListService codeListService)
            : base(formValidatorConfiguration, parentValidator)
        {
            _codeListService = codeListService;
        }        
        public KodelisteValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator, EntityValidatorEnum grandParentValidator, ICodeListService codeListService) 
            : base(formValidatorConfiguration, parentValidator, grandParentValidator)
        {
            _codeListService = codeListService;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(KodeListValidationEnums.utfylt, "kodeverdi");
            AddValidationRule(KodeListValidationEnums.ugyldig, "kodeverdi");
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeliste)
        {
            if (Helpers.ObjectIsNullOrEmpty(kodeliste.ModelData?.Kodeverdi))
            {
                AddMessageFromRule(KodeListValidationEnums.utfylt);
            }
            else
            {
                //TODO Sjekk hva "partstypeKodeverdi_ugyldig" er. Den er ikke initialisert....
                if (!_codeListService.IsCodelistValid(FtbCodeListNames.Partstype, kodeliste.ModelData?.Kodeverdi))
                {
                    AddMessageFromRule(KodeListValidationEnums.ugyldig);
                }
            }
            return ValidationResult;
        }
    }
}
