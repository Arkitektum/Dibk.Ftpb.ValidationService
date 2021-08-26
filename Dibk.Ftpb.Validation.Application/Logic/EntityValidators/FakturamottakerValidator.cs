using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System;
using System.Linq;
using System.Reflection;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FakturamottakerValidator : EntityValidatorBase, IFakturamottakerValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly IEnkelAdresseValidator _enkelAdresseValidator;

        public FakturamottakerValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IEnkelAdresseValidator enkelAdresseValidator) 
            : base(entityValidatorTree, nodeId)
        {
            //TODO: Automize this?
            _enkelAdresseValidator = enkelAdresseValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, "organisasjonsnummer");
            AddValidationRule(ValidationRuleEnum.gyldig, "organisasjonsnummer");
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
                if (string.IsNullOrEmpty(fakturamottaker.ModelData.Organisasjonsnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/organisasjonsnummer");
                }
                else
                {
                    var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(fakturamottaker.ModelData.Organisasjonsnummer);
                    switch (organisasjonsnummerValidation)
                    {
                        case OrganisasjonsnummerValidation.Empty:
                            AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/organisasjonsnummer");
                            break;
                        case OrganisasjonsnummerValidation.InvalidDigitsControl:
                            AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, $"{xpath}/organisasjonsnummer");
                            break;
                        case OrganisasjonsnummerValidation.Invalid:
                            AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xpath}/organisasjonsnummer");
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
