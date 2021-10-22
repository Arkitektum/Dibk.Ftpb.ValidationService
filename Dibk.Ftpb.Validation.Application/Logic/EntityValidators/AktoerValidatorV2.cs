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
    public abstract class AktoerValidatorV2 : EntityValidatorBase, IAktoerValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        protected ICodeListService _codeListService;
        protected string[] _allowedPartstypes;

        protected IEnkelAdresseValidator _enkelAdresseValidator;
        protected IKontaktpersonValidator _kontaktpersonValidator;
        protected IKodelisteValidator _partstypeValidator;

        public AktoerValidatorV2(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator,
            IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService, string[] partypes = null)
            : base(entityValidatorTree)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;
            _allowedPartstypes = partypes;
            InitializeConditionalValidationRules();
        }

        private void InitializeConditionalValidationRules()
        {
            if (_allowedPartstypes != null && _allowedPartstypes.Any())
            {
                AddValidationRule(ValidationRuleEnum.tillatt, null, $"{_entityXPath}/partstype/{FieldNameEnum.kodeverdi}");
            }

            if (IncludePrivatperson())
            {
                AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.foedselsnummer);
                AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.foedselsnummer);
                AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.foedselsnummer);
                AddValidationRule(ValidationRuleEnum.dekryptering, FieldNameEnum.foedselsnummer);
            }
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);

            AddValidationRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.telefonnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);


        }

        public ValidationResult Validate(Aktoer aktoer = null)
        {
            ValidateEntityFields(aktoer);
            if (!Helpers.ObjectIsNullOrEmpty(aktoer))
            {
                var aktoerPartsType = aktoer.Partstype;

                var partstypeValidatinResults = _partstypeValidator.Validate(aktoerPartsType);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                var codeValueHaveError = IsAnyValidationMessagesWithXpath($"{_partstypeValidator._entityXPath}/{FieldNameEnum.kodeverdi}");
                var partypeIsNullOrEmpty = IsAnyValidationMessagesWithXpath(_partstypeValidator._entityXPath);

                if (!codeValueHaveError && !partypeIsNullOrEmpty)
                {
                    ValidateDataRelations(aktoer);

                    var enkeladressResult = _enkelAdresseValidator.Validate(aktoer.Adresse);
                    UpdateValidationResultWithSubValidations(enkeladressResult);
                }

            }
            return _validationResult;
        }

        private void ValidateDataRelations(Aktoer aktoer)
        {
            if (_allowedPartstypes != null && _allowedPartstypes.All(p => !p.Equals(aktoer.Partstype.Kodeverdi)))
            {
                AddMessageFromRule(ValidationRuleEnum.tillatt, $"{_entityXPath}/partstype/{FieldNameEnum.kodeverdi}");
                return;
            }

            if (aktoer.Partstype?.Kodeverdi == "Privatperson" && IncludePrivatperson())
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
                var organisasjonsnummerValidation =
                    NorskStandardValidator.Validate_OrgnummerEnum(aktoer.Organisasjonsnummer);
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

            if (string.IsNullOrEmpty(aktoer.Navn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);

            if (string.IsNullOrEmpty(aktoer.Epost))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);

            if (string.IsNullOrEmpty(aktoer.Telefonnummer) && string.IsNullOrEmpty(aktoer.Mobilnummer))
            {
                AddMessageFromRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.telefonnummer);
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

        private void ValidateEntityFields(Aktoer aktoer)
        {
            if (Helpers.ObjectIsNullOrEmpty(aktoer))
                AddMessageFromRule(ValidationRuleEnum.utfylt);

        }

        private bool IncludePrivatperson()
        {
            if (_allowedPartstypes == null || !_allowedPartstypes.Any())
                return true;

            if (_allowedPartstypes.Any(p => p.Equals("Privatperson")))
                return true;

            return false;
        }

    }
}
