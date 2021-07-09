using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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


        public AktoerValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IEnkelAdresseValidator enkelAdresseValidator,
            IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(AktoerValidationEnum.utfylt, null);
            AddValidationRule(AktoerValidationEnum.foedselnummer_utfylt, "foedselsnummer");
            AddValidationRule(AktoerValidationEnum.foedselnummer_dekryptering, "foedselsnummer");
            AddValidationRule(AktoerValidationEnum.foedselnummer_kontrollsiffer, "foedselsnummer");
            AddValidationRule(AktoerValidationEnum.foedselnummer_gyldig, "foedselsnummer");
            AddValidationRule(AktoerValidationEnum.organisasjonsnummer_utfylt, "organisasjonsnummer");
            AddValidationRule(AktoerValidationEnum.organisasjonsnummer_kontrollsiffer, "organisasjonsnummer");
            AddValidationRule(AktoerValidationEnum.organisasjonsnummer_gyldig, "organisasjonsnummer");

            AddValidationRule(AktoerValidationEnum.telmob_utfylt, "mobilnummer");

            AddValidationRule(AktoerValidationEnum.epost_utfylt, "epost");
            AddValidationRule(AktoerValidationEnum.navn_utfylt, "navn");
        }


        public ValidationResult Validate(AktoerValidationEntity aktoer = null)
        {
            var xpath = aktoer.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(aktoer.ModelData))
            {
                AddMessageFromRule(AktoerValidationEnum.utfylt, xpath);
            }
            else
            {
                var aktoerPartsType = aktoer.ModelData.Partstype;

                var partstypeValidatinResults = _partstypeValidator.Validate(aktoerPartsType);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                var codeValueHaveError = IsAnyValidationMessagesWithXpath(aktoerPartsType.DataModelXpath, Helpers.GetNodeNamefromClass(() => aktoerPartsType.ModelData.Kodeverdi)).GetValueOrDefault();
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
            var xpath = aktoerValidationEntity.DataModelXpath;
            var aktoer = aktoerValidationEntity.ModelData;

            if (aktoer.Partstype?.ModelData?.Kodeverdi == "Privatperson")
            {
                var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(aktoer.Foedselsnummer);
                switch (foedselsnummerValidation)
                {
                    case FoedselnumerValidation.Empty:
                        AddMessageFromRule(AktoerValidationEnum.foedselnummer_utfylt, xpath);
                        break;
                    case FoedselnumerValidation.InvalidEncryption:
                        AddMessageFromRule(AktoerValidationEnum.foedselnummer_dekryptering, xpath);
                        break;
                    case FoedselnumerValidation.InvalidDigitsControl:
                        AddMessageFromRule(AktoerValidationEnum.foedselnummer_kontrollsiffer, xpath);
                        break;
                    case FoedselnumerValidation.Invalid:
                        AddMessageFromRule(AktoerValidationEnum.foedselnummer_gyldig, xpath);
                        break;
                }
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(aktoer.Organisasjonsnummer);
                switch (organisasjonsnummerValidation)
                {
                    case OrganisasjonsnummerValidation.Empty:
                        AddMessageFromRule(AktoerValidationEnum.organisasjonsnummer_utfylt, xpath);
                        break;
                    case OrganisasjonsnummerValidation.InvalidDigitsControl:
                        AddMessageFromRule(AktoerValidationEnum.organisasjonsnummer_kontrollsiffer, xpath);
                        break;
                    case OrganisasjonsnummerValidation.Invalid:
                        AddMessageFromRule(AktoerValidationEnum.organisasjonsnummer_gyldig, xpath);
                        break;
                }
                var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(aktoer.Kontaktperson);
                UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);
            }

            var enkeladressResult = _enkelAdresseValidator.Validate(aktoer.Adresse);
            UpdateValidationResultWithSubValidations(enkeladressResult);
            
            if (string.IsNullOrEmpty(aktoer.Navn))
                AddMessageFromRule(AktoerValidationEnum.navn_utfylt, xpath);

            if (string.IsNullOrEmpty(aktoer.Epost))
                AddMessageFromRule(AktoerValidationEnum.epost_utfylt, xpath);

            if (string.IsNullOrEmpty(aktoer.Telefonnummer) && string.IsNullOrEmpty(aktoer.Mobilnummer))
            {
                AddMessageFromRule(AktoerValidationEnum.telmob_utfylt, xpath);
            }
            else
            {
                if (!string.IsNullOrEmpty(aktoer.Telefonnummer))
                {
                    var telefonNumber = aktoer?.Telefonnummer;
                    var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                    if (!isValidTelefonNumber)
                    {
                        AddMessageFromRule(AktoerValidationEnum.telefonnummer_ugyldig, xpath);

                    }
                }
                if (!string.IsNullOrEmpty(aktoer.Mobilnummer))
                {
                    var mobilNummer = aktoer.Mobilnummer;
                    var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                    if (!isValidmobilnummer)
                    {
                        AddMessageFromRule(AktoerValidationEnum.mobilnummer_ugyldig, xpath);
                    }
                }
            }

        }
    }
}
