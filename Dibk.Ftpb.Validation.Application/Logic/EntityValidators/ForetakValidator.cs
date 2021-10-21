using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ForetakValidator : EntityValidatorBase, IForetakValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }


        protected ICodeListService _codeListService;

        protected IEnkelAdresseValidator _enkelAdresseValidator;
        protected IKontaktpersonValidator _kontaktpersonValidator;
        protected IKodelisteValidator _partstypeValidator;

        public ForetakValidator(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator,
            IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
            : base(entityValidatorTree)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);

            //Check this rules
            AddValidationRule(ValidationRuleEnum.gyldig, null, $"{_entityXPath}/partstype/{FieldNameEnum.kodeverdi}");

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
            AddValidationRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.mobilnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);



            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);
        }


        public ValidationResult Validate(Foretak foretak = null)
        {

            if (Helpers.ObjectIsNullOrEmpty(foretak))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                var aktoerPartsType = foretak.Partstype;
                var partstypeValidatinResults = _partstypeValidator.Validate(aktoerPartsType);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                var codeValueHaveError = IsAnyValidationMessagesWithXpath($"{_partstypeValidator._entityXPath}/{FieldNameEnum.kodeverdi}");
                var partypeIsNullOrEmpty = IsAnyValidationMessagesWithXpath(_partstypeValidator._entityXPath);

                if (!codeValueHaveError && !partypeIsNullOrEmpty)
                {
                    ValidateEntityFields(foretak);

                    if (foretak.Partstype.Kodeverdi.Equals("Foretak"))
                    {
                        var enkeladressResult = _enkelAdresseValidator.Validate(foretak.Adresse);
                        UpdateValidationResultWithSubValidations(enkeladressResult);

                        var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(foretak.Kontaktperson);
                        UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);
                    }
                }
            }
            return _validationResult;
        }

        public void ValidateEntityFields(Foretak foretak)
        {
            if (!foretak.Partstype.Kodeverdi.Equals("Foretak"))
            {
                AddMessageFromRule(ValidationRuleEnum.gyldig, $"{_partstypeValidator._entityXPath}/{FieldNameEnum.kodeverdi}");
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(foretak.Organisasjonsnummer);
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

                if (string.IsNullOrEmpty(foretak.Navn))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);

                if (string.IsNullOrEmpty(foretak.Telefonnummer) && string.IsNullOrEmpty(foretak.Mobilnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.mobilnummer);
                }
                else
                {
                    if (!string.IsNullOrEmpty(foretak.Telefonnummer))
                    {
                        var telefonNumber = foretak?.Telefonnummer;
                        var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                        if (!isValidTelefonNumber)
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);
                        }
                    }
                    if (!string.IsNullOrEmpty(foretak.Mobilnummer))
                    {
                        var mobilNummer = foretak.Mobilnummer;
                        var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                        if (!isValidmobilnummer)
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);
                        }
                    }
                }

                if (string.IsNullOrEmpty(foretak.Epost))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);
            }
        }
    }
}
