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

        public TiltakshaverValidator(ICodeListService codeListService) : base()
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = new EnkelAdresseValidator();
        }
        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule("tiltakshaver_utfylt", xPathForEntity);
            AddValidationRule("tiltakshaver_foedselnummer_utfylt", xPathForEntity, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_Dekryptering", xPathForEntity, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_kontrollsiffer", xPathForEntity, "foedselsnummer");
            AddValidationRule("tiltakshaver_foedselnummer_ugyldig", xPathForEntity, "foedselsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_utfylt", xPathForEntity, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_kontrollsiffer", xPathForEntity, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_organisasjonsnummer_ugyldig", xPathForEntity, "organisasjonsnummer");
            AddValidationRule("tiltakshaver_TelMob_Utfylt", xPathForEntity);
            AddValidationRule("tiltakshaver_epost_Utfylt", xPathForEntity, "epost");
            AddValidationRule("tiltakshaver_Navn_Utfylt", xPathForEntity, "navn");
        }
        public ValidationResult Validate(AktoerValidationEntity tiltakshaver = null)
        {
            InitializeValidationRules(tiltakshaver.DataModelXpath);

            if (Helpers.ObjectIsNullOrEmpty(tiltakshaver.ModelData))
            {
                AddMessageFromRule("tiltakshaver_utfylt");
            }
            else
            {
                var partstypeValidatinResults = new PartstypeValidator(_codeListService).Validate(tiltakshaver.ModelData.Partstype);
                UpdateValidationResultWithSubValidations(partstypeValidatinResults);
                //TODO diskutere hvordan man bruke svaret 
                //if validation message have any with tiltakshaver.Partstype.Kodeverdi (ok)
                if (!partstypeValidatinResults.ValidationMessages.Any())
                {
                    ValidateEntityFields(tiltakshaver.ModelData);
                }
            }
            return _validationResult;
        }

        private void ValidateEntityFields(Aktoer tiltakshaver)
        {
            if (tiltakshaver.Partstype.ModelData.Kodeverdi == "Privatperson")
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

                var kontaktpersonResult = new KontaktpersonValidator().Validate(tiltakshaver.Kontaktperson);
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
            _validationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            _validationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }
    }
}
