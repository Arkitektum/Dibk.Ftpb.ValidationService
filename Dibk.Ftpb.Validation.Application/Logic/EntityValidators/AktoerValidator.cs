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
        //private string _aktoer { get; set; }
        //public override string ruleXmlElement { get { return $"{_aktoer}"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        protected ICodeListService _codeListService;

        protected IEnkelAdresseValidator _enkelAdresseValidator;
        protected IKontaktpersonValidator _kontaktpersonValidator;
        protected IKodelisteValidator _kodelisteValidator;

        
        public AktoerValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IEnkelAdresseValidator enkelAdresseValidator,
            IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator kodelisteValidator, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _kodelisteValidator = kodelisteValidator;

        }
        //public AktoerValidator(FormValidatorConfiguration formValidatorConfiguration, IEnkelAdresseValidator enkelAdresseValidator,
        //    IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator kodelisteValidator, ICodeListService codeListService)
        //    : base(formValidatorConfiguration)
        //{
        //    _codeListService = codeListService;
        //    _enkelAdresseValidator = enkelAdresseValidator;
        //    _kontaktpersonValidator = kontaktpersonValidator;
        //    _kodelisteValidator = kodelisteValidator;

        //}
        protected override void InitializeValidationRules()
        {
            AddValidationRule(AktoerValidationEnums.utfylt, null);
            AddValidationRule(AktoerValidationEnums.foedselnummer_utfylt, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.foedselnummer_dekryptering, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.foedselnummer_kontrollsiffer, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.foedselnummer_ugyldig, "foedselsnummer");
            AddValidationRule(AktoerValidationEnums.organisasjonsnummer_utfylt, "organisasjonsnummer");
            AddValidationRule(AktoerValidationEnums.organisasjonsnummer_kontrollsiffer, "organisasjonsnummer");
            AddValidationRule(AktoerValidationEnums.organisasjonsnummer_ugyldig, "organisasjonsnummer");

            AddValidationRule(AktoerValidationEnums.telmob_utfylt, "mobilnummer");

            AddValidationRule(AktoerValidationEnums.epost_utfylt, "epost");
            AddValidationRule(AktoerValidationEnums.navn_utfylt, "navn");
        }


        public ValidationResult Validate(AktoerValidationEntity aktoer = null)
        {
            var xpath = aktoer.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(aktoer.ModelData))
            {
                AddMessageFromRule(AktoerValidationEnums.utfylt);
            }
            else
            {
                var tiltakshaverPartsType = aktoer.ModelData.Partstype;
                var partstypeValidatinResults = _kodelisteValidator.Validate(tiltakshaverPartsType);

                UpdateValidationResultWithSubValidations(partstypeValidatinResults);

                var codeValueHaveError = IsAnyValidationMessagesWithXpath(tiltakshaverPartsType.DataModelXpath, "Kodeverdi").GetValueOrDefault();

                if (!codeValueHaveError)
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

            if (Helpers.ObjectIsNullOrEmpty(tiltakshaver.Partstype.ModelData))
            {
                AddMessageFromRule(AktoerValidationEnums.partstype_utfylt);
            }
            else
            { 
                if (tiltakshaver.Partstype.ModelData.Kodeverdi == "Privatperson")
                {
                    var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(tiltakshaver.Foedselsnummer);
                    switch (foedselsnummerValidation)
                    {
                        case FoedselnumerValidation.Empty:
                            AddMessageFromRule(AktoerValidationEnums.foedselnummer_utfylt);
                            break;
                        case FoedselnumerValidation.InvalidEncryption:
                            AddMessageFromRule(AktoerValidationEnums.foedselnummer_dekryptering);
                            break;
                        case FoedselnumerValidation.InvalidDigitsControl:
                            AddMessageFromRule(AktoerValidationEnums.foedselnummer_kontrollsiffer);
                            break;
                        case FoedselnumerValidation.Invalid:
                            AddMessageFromRule(AktoerValidationEnums.foedselnummer_ugyldig);
                            break;
                    }
                }
                else
                {
                    var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(tiltakshaver.Organisasjonsnummer);
                    switch (organisasjonsnummerValidation)
                    {
                        case OrganisasjonsnummerValidation.Empty:
                            AddMessageFromRule(AktoerValidationEnums.organisasjonsnummer_utfylt);
                            break;
                        case OrganisasjonsnummerValidation.InvalidDigitsControl:
                            AddMessageFromRule(AktoerValidationEnums.organisasjonsnummer_kontrollsiffer);
                            break;
                        case OrganisasjonsnummerValidation.Invalid:
                            AddMessageFromRule(AktoerValidationEnums.organisasjonsnummer_ugyldig);
                            break;
                    }

                    //var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(tiltakshaver.Kontaktperson);
                    //UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);

                    if (string.IsNullOrEmpty(tiltakshaver.Epost))
                        AddMessageFromRule(AktoerValidationEnums.epost_utfylt);

                    if (string.IsNullOrEmpty(tiltakshaver.Navn))
                        AddMessageFromRule(AktoerValidationEnums.navn_utfylt);
                }
            }

            //var enkeladressResult = _enkelAdresseValidator.Validate(tiltakshaver.Adresse);
            //UpdateValidationResultWithSubValidations(enkeladressResult);

            if (string.IsNullOrEmpty(tiltakshaver.Mobilnummer) && string.IsNullOrEmpty(tiltakshaver.Telefonnummer))
                AddMessageFromRule(AktoerValidationEnums.telmob_utfylt);

            if (string.IsNullOrEmpty(tiltakshaver.Telefonnummer) && string.IsNullOrEmpty(tiltakshaver.Mobilnummer))
            {
                AddMessageFromRule(AktoerValidationEnums.telmob_utfylt);
            }
            else
            {
                if (!string.IsNullOrEmpty(tiltakshaver.Telefonnummer))
                {
                    var telefonNumber = tiltakshaver?.Telefonnummer;
                    var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                    if (!isValidTelefonNumber)
                    {
                        //_validationResult.AddMessage("5721.1.5.6.5.1", null);
                    }
                }
                if (!string.IsNullOrEmpty(tiltakshaver.Mobilnummer))
                {
                    var mobilNummer = tiltakshaver.Mobilnummer;
                    var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                    if (!isValidmobilnummer)
                    {
                        //_validationResult.AddMessage("5721.1.5.6.5.2", null);
                    }
                }
            }

        }

        private void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult)
        {
            _validationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            _validationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }
    }
}
