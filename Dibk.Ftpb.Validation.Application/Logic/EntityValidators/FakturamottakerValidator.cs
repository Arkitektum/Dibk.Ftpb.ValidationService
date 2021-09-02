using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FakturamottakerValidator : EntityValidatorBase, IFakturamottakerValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly IEnkelAdresseValidator _enkelAdresseValidator;

        public FakturamottakerValidator(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator) 
            : base(entityValidatorTree)
        {
            _enkelAdresseValidator = enkelAdresseValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.ehf_eller_papir);
        }

        public ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null)
        {
            var xpath = fakturamottaker.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData.EhfFaktura) && Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData.FakturaPapir))
                {
                    AddValidationRule(ValidationRuleEnum.ehf_eller_papir, xpath);
                }

                if (string.IsNullOrEmpty(fakturamottaker.ModelData.Organisasjonsnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/{FieldNameEnum.organisasjonsnummer}");
                }
                else
                {
                    var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(fakturamottaker.ModelData.Organisasjonsnummer);
                    switch (organisasjonsnummerValidation)
                    {
                        case OrganisasjonsnummerValidation.Empty:
                            AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/{FieldNameEnum.organisasjonsnummer}");
                            break;
                        case OrganisasjonsnummerValidation.InvalidDigitsControl:
                            AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, $"{xpath}/{FieldNameEnum.organisasjonsnummer}");
                            break;
                        case OrganisasjonsnummerValidation.Invalid:
                            AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xpath}/{FieldNameEnum.organisasjonsnummer}");
                            break;
                    }
                }

                var adresseValidationResult = _enkelAdresseValidator.Validate(fakturamottaker.ModelData.Adresse);
                UpdateValidationResultWithSubValidations(adresseValidationResult);
            }
            return ValidationResult;
        }
    }
}
