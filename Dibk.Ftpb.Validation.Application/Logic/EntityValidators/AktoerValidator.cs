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
        private static string _xPath = null;


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


        public ValidationResult Validate(AktoerValidationEntity aktoer = null)
        {
            var xpath = aktoer?.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(aktoer?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
            }
            else
            {
                var aktoerPartsType = aktoer.ModelData.Partstype;

                var partstypeValidatinResults = _partstypeValidator.Validate(aktoerPartsType);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                var codeValueHaveError = IsAnyValidationMessagesWithXpath(CombineDataModelXpathAndFieldName(FieldNameEnum.kodeverdi, aktoer?.ModelData?.Partstype?.DataModelXpath)).GetValueOrDefault();
                var partypeIsNullOrEmpty = IsAnyValidationMessagesWithXpath(aktoerPartsType.DataModelXpath).GetValueOrDefault();

                if (!codeValueHaveError && !partypeIsNullOrEmpty)
                {
                    ValidateEntityFields(aktoer);
                }
            }
            return _validationResult;
        }

        private void ValidateEntityFields(AktoerValidationEntity aktoerValidationEntity)
        {
           _xPath = aktoerValidationEntity.DataModelXpath;
            var aktoer = aktoerValidationEntity.ModelData;

            if (aktoer.Partstype?.ModelData?.Kodeverdi == "Privatperson")
            {
                var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(aktoer.Foedselsnummer);
                switch (foedselsnummerValidation)
                {
                    case FoedselnumerValidation.Empty:
                        AddMessageFromRule(ValidationRuleEnum.utfylt, CombineDataModelXpathAndFieldName(FieldNameEnum.foedselsnummer));
                        break;
                    case FoedselnumerValidation.InvalidEncryption:
                        AddMessageFromRule(ValidationRuleEnum.dekryptering, CombineDataModelXpathAndFieldName(FieldNameEnum.foedselsnummer));
                        break;
                    case FoedselnumerValidation.InvalidDigitsControl:
                        AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, CombineDataModelXpathAndFieldName(FieldNameEnum.foedselsnummer));
                        break;
                    case FoedselnumerValidation.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.gyldig, CombineDataModelXpathAndFieldName(FieldNameEnum.foedselsnummer));
                        break;
                }
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(aktoer.Organisasjonsnummer);
                switch (organisasjonsnummerValidation)
                {
                    case OrganisasjonsnummerValidation.Empty:
                        AddMessageFromRule(ValidationRuleEnum.utfylt, CombineDataModelXpathAndFieldName(FieldNameEnum.organisasjonsnummer));
                        break;
                    case OrganisasjonsnummerValidation.InvalidDigitsControl:
                        AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, CombineDataModelXpathAndFieldName(FieldNameEnum.organisasjonsnummer));
                        break;
                    case OrganisasjonsnummerValidation.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.gyldig, CombineDataModelXpathAndFieldName(FieldNameEnum.organisasjonsnummer));
                        break;
                }
                var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(aktoer.Kontaktperson);
                UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);
            }

            var enkeladressResult = _enkelAdresseValidator.Validate(aktoer.Adresse);
            UpdateValidationResultWithSubValidations(enkeladressResult);

            if (string.IsNullOrEmpty(aktoer.Navn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, CombineDataModelXpathAndFieldName(FieldNameEnum.navn));

            if (string.IsNullOrEmpty(aktoer.Epost))
                AddMessageFromRule(ValidationRuleEnum.utfylt, CombineDataModelXpathAndFieldName(FieldNameEnum.epost));

            if (string.IsNullOrEmpty(aktoer.Telefonnummer) && string.IsNullOrEmpty(aktoer.Mobilnummer))
            {
                AddMessageFromRule(ValidationRuleEnum.telmob_utfylt, CombineDataModelXpathAndFieldName());
            }
            else
            {
                if (!string.IsNullOrEmpty(aktoer.Telefonnummer))
                {
                    var telefonNumber = aktoer?.Telefonnummer;
                    var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                    if (!isValidTelefonNumber)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{_xPath}/{FieldNameEnum.telefonnummer}");
                    }
                }
                if (!string.IsNullOrEmpty(aktoer.Mobilnummer))
                {
                    var mobilNummer = aktoer.Mobilnummer;
                    var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                    if (!isValidmobilnummer)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{_xPath}/{FieldNameEnum.mobilnummer}");
                    }
                }
            }
        }
        private static string CombineDataModelXpathAndFieldName(FieldNameEnum? fieldName = null, string entityXpath = null)
        {
            var xpath = entityXpath ?? _xPath;
            var xpathComposed = string.Format("{0}/{1}",new [] { xpath, fieldName?.ToString()});
            return xpathComposed;
        }

    }
}
