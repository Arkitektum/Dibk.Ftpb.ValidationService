using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public sealed class TiltakshaverValidator : EntityValidatorBase
    {
        private readonly ICodeListService _codeListService;

        private EnkelAdresseValidator _enkelAdresseValidator;

        public TiltakshaverValidator(string parentXPath, ICodeListService codeListService) : base(parentXPath, "tiltakshaver")
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = new EnkelAdresseValidator(EntityXPath);
            InitializeValidationRules();
        }
        public override void InitializeValidationRules()
        {
            AddValidationRule("tiltakshaver_utfylt", EntityXPath);
            AddValidationRule("tiltakshaver_foedselnummer_utfylt", EntityXPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_Dekryptering", EntityXPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_kontrollsiffer", EntityXPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_ugyldig", EntityXPath, "foedselsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_utfylt", EntityXPath, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_kontrollsiffer", EntityXPath, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_ugyldig", EntityXPath, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_TelMob_Utfylt", EntityXPath);
            AddValidationRule("tiltakshaver_epost_Utfylt", EntityXPath, "epost");
            AddValidationRule("tiltakshaver_Navn_Utfylt", EntityXPath, "navn");
        }
        public ValidationResult Validate(Aktoer tiltakshaver = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(tiltakshaver))
            {
                AddMessageFromRule("tiltakshaver_utfylt");
            }
            else
            {
                var partstypeValidatinResults = new PartstypeValidator(EntityXPath, _codeListService).Validate(null, tiltakshaver.Partstype);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);
                //TODO diskutere hvordan man bruke svaret 
                //if validation message have any with tiltakshaver.Partstype.Kodeverdi (ok)
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

                var enkeladressResult = _enkelAdresseValidator.Validate(tiltakshaver.Adresse);
                UpdateValidationResultWithSubValidations(enkeladressResult);

                var kontaktpersonResult = new KontaktpersonValidator(EntityXPath).Validate(null, tiltakshaver.Kontaktperson);
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
