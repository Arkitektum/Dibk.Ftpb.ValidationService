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
            AddValidationRule(AktoerValidationEnums.utfylt, null);
            AddValidationRule(AktoerValidationEnums.foedselnummer_utfylt, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.foedselnummer_dekryptering, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.foedselnummer_kontrollsiffer, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.foedselnummer_gyldig, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.organisasjonsnummer_utfylt, "organisasjonsnummer");
            AddValidationRule(AktoerValidationEnums.organisasjonsnummer_kontrollsiffer, "organisasjonsnummer");
            AddValidationRule(AktoerValidationEnums.organisasjonsnummer_gyldig, "organisasjonsnummer");

            AddValidationRule(AktoerValidationEnums.telmob_utfylt, "mobilnummer");

            AddValidationRule(AktoerValidationEnums.epost_utfylt, "epost");
            AddValidationRule(AktoerValidationEnums.navn_utfylt, "navn");
        }


        public ValidationResult Validate(AktoerValidationEntity aktoer = null)
        {
            var xpath = aktoer.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(aktoer.ModelData))
            {
                AddMessageFromRule(AktoerValidationEnums.utfylt, xpath);
            }
            else
            {
                var tiltakshaverPartsType = aktoer.ModelData.Partstype;

                var partstypeValidatinResults = _partstypeValidator.Validate(tiltakshaverPartsType);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                var codeValueHaveError = IsAnyValidationMessagesWithXpath(tiltakshaverPartsType.DataModelXpath, Helpers.GetNodeNamefromClass(() => tiltakshaverPartsType.ModelData.Kodeverdi)).GetValueOrDefault();
                var partypeIsNullOrEmpty = IsAnyValidationMessagesWithXpath(tiltakshaverPartsType.DataModelXpath).GetValueOrDefault();

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
            var tiltakshaver = aktoerValidationEntity.ModelData;

            if (tiltakshaver.Partstype?.ModelData?.Kodeverdi == "Privatperson")
            {
                var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(tiltakshaver.Foedselsnummer);
                switch (foedselsnummerValidation)
                {
                    case FoedselnumerValidation.Empty:
                        AddMessageFromRule(AktoerValidationEnums.foedselnummer_utfylt, xpath);
                        break;
                    case FoedselnumerValidation.InvalidEncryption:
                        AddMessageFromRule(AktoerValidationEnums.foedselnummer_dekryptering, xpath);
                        break;
                    case FoedselnumerValidation.InvalidDigitsControl:
                        AddMessageFromRule(AktoerValidationEnums.foedselnummer_kontrollsiffer, xpath);
                        break;
                    case FoedselnumerValidation.Invalid:
                        AddMessageFromRule(AktoerValidationEnums.foedselnummer_gyldig, xpath);
                        break;
                }
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(tiltakshaver.Organisasjonsnummer);
                switch (organisasjonsnummerValidation)
                {
                    case OrganisasjonsnummerValidation.Empty:
                        AddMessageFromRule(AktoerValidationEnums.organisasjonsnummer_utfylt, xpath);
                        break;
                    case OrganisasjonsnummerValidation.InvalidDigitsControl:
                        AddMessageFromRule(AktoerValidationEnums.organisasjonsnummer_kontrollsiffer, xpath);
                        break;
                    case OrganisasjonsnummerValidation.Invalid:
                        AddMessageFromRule(AktoerValidationEnums.organisasjonsnummer_gyldig, xpath);
                        break;
                }
                var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(tiltakshaver.Kontaktperson);
                UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);
            }

            var enkeladressResult = _enkelAdresseValidator.Validate(tiltakshaver.Adresse);
            UpdateValidationResultWithSubValidations(enkeladressResult);
            
            if (string.IsNullOrEmpty(tiltakshaver.Navn))
                AddMessageFromRule(AktoerValidationEnums.navn_utfylt, xpath);

            if (string.IsNullOrEmpty(tiltakshaver.Epost))
                AddMessageFromRule(AktoerValidationEnums.epost_utfylt, xpath);

            if (string.IsNullOrEmpty(tiltakshaver.Telefonnummer) && string.IsNullOrEmpty(tiltakshaver.Mobilnummer))
            {
                AddMessageFromRule(AktoerValidationEnums.telmob_utfylt, xpath);
            }
            else
            {
                if (!string.IsNullOrEmpty(tiltakshaver.Telefonnummer))
                {
                    var telefonNumber = tiltakshaver?.Telefonnummer;
                    var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                    if (!isValidTelefonNumber)
                    {
                        AddMessageFromRule(AktoerValidationEnums.telefonnummer_ugyldig, xpath);

                    }
                }
                if (!string.IsNullOrEmpty(tiltakshaver.Mobilnummer))
                {
                    var mobilNummer = tiltakshaver.Mobilnummer;
                    var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                    if (!isValidmobilnummer)
                    {
                        AddMessageFromRule(AktoerValidationEnums.mobilnummer_ugyldig, xpath);
                    }
                }
            }

        }
    }
}
