using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;
using System.Linq;
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
            AddValidationRule(ValidationRuleEnum.ehf_eller_papir);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.bestillerReferanse);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.fakturareferanser);



        }

        public ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                ValidateEntityFields(fakturamottaker);

                if (RuleIsValid(ValidationRuleEnum.ehf_eller_papir))
                {
                    var adresseValidationResult = _enkelAdresseValidator.Validate(fakturamottaker.ModelData.Adresse);
                    UpdateValidationResultWithSubValidations(adresseValidationResult);
                }
            }
            return ValidationResult;
        }

        public void ValidateEntityFields(FakturamottakerValidationEntity fakturamottaker = null)
        {
            var booleans = new[]
            {
                fakturamottaker.ModelData.EhfFaktura.GetValueOrDefault(),
                fakturamottaker.ModelData.FakturaPapir.GetValueOrDefault(),
            };
            var trueCount = booleans.Count(c => c);
            if (trueCount != 1)
                AddMessageFromRule(ValidationRuleEnum.ehf_eller_papir);
            else
            {
                if (fakturamottaker.ModelData.EhfFaktura.GetValueOrDefault())
                {
                    var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(fakturamottaker.ModelData.Organisasjonsnummer);
                    switch (organisasjonsnummerValidation)
                    {
                        case OrganisasjonsnummerValidation.Empty:
                            AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
                            break;
                        case OrganisasjonsnummerValidation.InvalidDigitsControl:
                            AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);
                            break;
                        case OrganisasjonsnummerValidation.Invalid:
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
                            break;
                    }
                }

                if (fakturamottaker.ModelData.FakturaPapir.GetValueOrDefault())
                {
                    if (string.IsNullOrEmpty(fakturamottaker.ModelData.Navn))
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
                    }
                }

                if (string.IsNullOrEmpty(fakturamottaker.ModelData.BestillerReferanse))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.bestillerReferanse);

                if (string.IsNullOrEmpty(fakturamottaker.ModelData.Fakturareferanser))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.fakturareferanser);

            }

        }
    }
}
