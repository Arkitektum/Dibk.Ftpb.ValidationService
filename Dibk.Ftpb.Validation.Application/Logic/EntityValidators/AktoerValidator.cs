using System;
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
    public abstract class AktoerValidator : EntityValidatorBase, IAktoerValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }
        
        protected ICodeListService _codeListService;

        protected IEnkelAdresseValidator _enkelAdresseValidator;
        protected IKontaktpersonValidator _kontaktpersonValidator;
        protected IKodelisteValidator _partstypeValidator;

        public AktoerValidator(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator,
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

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.foedselsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.foedselsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.foedselsnummer);
            AddValidationRule(ValidationRuleEnum.dekryptering, FieldNameEnum.foedselsnummer);

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);

            AddValidationRule(ValidationRuleEnum.telmob_utfylt);

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
        }


        public ValidationResult Validate(Aktoer aktoer = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(aktoer))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                var aktoerPartsType = aktoer.Partstype;

                var partstypeValidatinResults = _partstypeValidator.Validate(aktoerPartsType);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                var codeValueHaveError = IsAnyValidationMessagesWithXpath($"{_partstypeValidator._entityXPath}/{FieldNameEnum.kodeverdi}");
                var partypeIsNullOrEmpty = IsAnyValidationMessagesWithXpath(_partstypeValidator._entityXPath);

                if (!codeValueHaveError && !partypeIsNullOrEmpty)
                {
                    ValidateEntityFields(aktoer);
                }
            }
            return _validationResult;
        }

        private void ValidateEntityFields(Aktoer aktoer)
        {
            if (aktoer.Partstype?.Kodeverdi == "Privatperson")
            {
                var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(aktoer.Foedselsnummer);
                switch (foedselsnummerValidation)
                {
                    case FoedselnumerValidation.Empty:
                        AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.foedselsnummer);
                        break;
                    case FoedselnumerValidation.InvalidEncryption:
                        AddMessageFromRule(ValidationRuleEnum.dekryptering, FieldNameEnum.foedselsnummer);
                        break;
                    case FoedselnumerValidation.InvalidDigitsControl:
                        AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.foedselsnummer);
                        break;
                    case FoedselnumerValidation.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.foedselsnummer);
                        break;
                }
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(aktoer.Organisasjonsnummer);
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
                var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(aktoer.Kontaktperson);
                UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);
            }

            var enkeladressResult = _enkelAdresseValidator.Validate(aktoer.Adresse);
            UpdateValidationResultWithSubValidations(enkeladressResult);

            if (string.IsNullOrEmpty(aktoer.Navn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);

            if (string.IsNullOrEmpty(aktoer.Epost))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);

            if (string.IsNullOrEmpty(aktoer.Telefonnummer) && string.IsNullOrEmpty(aktoer.Mobilnummer))
            {
                AddMessageFromRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.mobilnummer);
            }
            else
            {
                if (!string.IsNullOrEmpty(aktoer.Telefonnummer))
                {
                    var telefonNumber = aktoer?.Telefonnummer;
                    var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                    if (!isValidTelefonNumber)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);
                    }
                }
                if (!string.IsNullOrEmpty(aktoer.Mobilnummer))
                {
                    var mobilNummer = aktoer.Mobilnummer;
                    var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                    if (!isValidmobilnummer)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);
                    }
                }
            }
        }
    }
}
