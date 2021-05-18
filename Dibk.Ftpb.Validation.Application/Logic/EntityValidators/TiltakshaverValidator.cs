using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class TiltakshaverValidator : EntityValidatorBase
    {

        private string _xPath;
        private const string _entityName = "tiltakshaver";

        public TiltakshaverValidator(string parentxPath)
        {
            _xPath = $"{parentxPath}/{_entityName}";
            InitializeValidationRules();
        }
        public override void InitializeValidationRules(string parentContext = null)
        {
            AddValidationRule("tiltakshaver_utfylt", _xPath, "bygningsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_utfylt", _xPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_Dekryptering", _xPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_kontrollsiffer", _xPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_ugyldig", _xPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_utfyltg", _xPath, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_kontrollsiffer", _xPath, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_ugyldig", _xPath, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_TelMob_Utfylt", _xPath);
            AddValidationRule("tiltakshaver_epost_Utfylt", _xPath, "epost");
            AddValidationRule("tiltakshaver_Navn_Utfylt", _xPath, "navn");
        }

        public ValidationResult Validate(string xPath = null, Aktoer tiltakshaver = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(tiltakshaver))
            {
                AddMessageFromRule("tiltakshaver_utfylt");
            }
            else
            {
                var partstypeValidatinResults = new PartstypeValidator(_xPath).Validate(null, tiltakshaver.Partstype);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);
                //TODO diskutere hvordan man bruke svaret
                //if validation message have any with tiltakshaver.Partstype.Kodeverdi
                if (!partstypeValidatinResults.ValidationMessages.Any())
                {
                    ValidateEntityFields(tiltakshaver);
                }
            }

            return ValidationResult;
        }

        private void ValidateEntityFields(Aktoer tiltakshaver)
        {
            if (tiltakshaver.Partstype.Kodeverdi == "Privatperson")
            {
                var foedselsnummerValidation = NorskStandardValidator.Validate_foedselsnummer(tiltakshaver.Foedselsnummer);
                switch (foedselsnummerValidation)
                {
                    case FoedselnumerValidation.Empty:
                        AddMessageFromRule("tiltakshaver_foedselnummer_utfylt");
                        break;
                    case FoedselnumerValidation.InvalidEncryption:
                        AddMessageFromRule("tiltakshaver_foedselnummer_Dekryptering");
                        break;
                    case FoedselnumerValidation.InvalidDigitsControl:
                        AddMessageFromRule("tiltakshaver_foedselnummer_kontrollsiffer");
                        break;
                    case FoedselnumerValidation.Invalid:
                        AddMessageFromRule("tiltakshaver_foedselnummer_ugyldig");
                        break;
                }
            }
            else
            {
                var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(tiltakshaver.Organisasjonsnummer);
                switch (organisasjonsnummerValidation)
                {
                    case OrganisasjonsnummerValidation.Empty:
                        AddMessageFromRule("tiltakshaver_organisasjonsnummer_utfylt");
                        break;
                    case OrganisasjonsnummerValidation.InvalidDigitsControl:
                        AddMessageFromRule("tiltakshaver_organisasjonsnummer_kontrollsiffer");
                        break;
                    case OrganisasjonsnummerValidation.Invalid:
                        AddMessageFromRule("tiltakshaver_organisasjonsnummer_ugyldig");
                        break;
                }

                var enkeladressResult = new EnkelAdresseValidator(_xPath).Validate(null, tiltakshaver.Adresse);
                UpdateValidationResultWithSubValidations(enkeladressResult);

                var kontaktpersonResult = new KontaktpersonValidator(_xPath).Validate(null, tiltakshaver.Kontaktperson);
                UpdateValidationResultWithSubValidations(kontaktpersonResult);

                if (string.IsNullOrEmpty(tiltakshaver.Mobilnummer) && string.IsNullOrEmpty(tiltakshaver.Telefonnummer))
                    AddMessageFromRule("tiltakshaver_TelMob_Utfylt");


                if (string.IsNullOrEmpty(tiltakshaver.Epost))
                    AddMessageFromRule("tiltakshaver_epost_Utfylt");

                if (string.IsNullOrEmpty(tiltakshaver.Navn))
                    AddMessageFromRule("tiltakshaver_Navn_Utfylt");

            }
        }

        private void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult)
        {
            ValidationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            ValidationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }
    }
}
