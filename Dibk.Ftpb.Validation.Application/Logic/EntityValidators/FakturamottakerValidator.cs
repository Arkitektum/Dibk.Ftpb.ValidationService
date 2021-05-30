﻿using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FakturamottakerValidator : EntityValidatorBase
    {
        private EnkelAdresseValidator _enkelAdresseValidator;
        public FakturamottakerValidator(string xPath) : base()
        {
            InitializeValidationRules(xPath);            
            _enkelAdresseValidator = new EnkelAdresseValidator($"{xPath}/adresse");
            
            //TODO: Automize this?
            this.ValidationResult.ValidationRules.AddRange(_enkelAdresseValidator.ValidationResult.ValidationRules);

        }
        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.fakturamottaker_utfylt, xPathForEntity);
        }

        public ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null)
        {

            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.fakturamottaker_utfylt);
            }
            else
            {
                var adresseValidationResult =  _enkelAdresseValidator.Validate(fakturamottaker.ModelData.Adresse);
                UpdateValidationResultWithSubValidations(adresseValidationResult);
            }
            return ValidationResult;
        }
    }
}
