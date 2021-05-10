using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomsAdresseValidator : EntityValidatorBase
    {
        public EiendomsAdresseValidator() : base()
        {
        }
        public ValidationResult Validate(string parentContext, EiendomsAdresse eiendomsAdresse)
        {
            string context = $"{parentContext}/adresse";
            InitializeValidationRules(context);
            ValidateEntityFields(eiendomsAdresse, context);

            return ValidationResponse;
        }

        public override void InitializeValidationRules(string context)
        {
            //ArbeidstilsynetsSamtykke/eiendomByggested/adresse/adresselinje1  ==>  ArbeidstilsynetsSamtykke/eiendomByggested[0]/adresse/adresselinje1
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje1_utfylt", Xpath = $"{context}/adresselinje1" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje2_utfylt", Xpath = $"{context}/adresselinje2" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje3_utfylt", Xpath = $"{context}/adresselinje3" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_landkode_utfylt", Xpath = $"{context}/landkode", ChecklistReference = "13.1" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_postnr_utfylt", Xpath = $"{context}/postnr" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_poststed_utfylt", Xpath = $"{context}/poststed" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_gatenavn_utfylt", Xpath = $"{context}/gatenavn" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_husnr_utfylt", Xpath = $"{context}/husnr" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_bokstav_utfylt", Xpath = $"{context}/bokstav" });
            ValidationResponse.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_postnr_4siffer", Xpath = $"{context}/postnr" });
        }

        public void ValidateEntityFields(EiendomsAdresse eiendomsAdresse, string context)
        {
            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje1))
                AddMessageFromRule("eiendomsAdresse_adresselinje1_utfylt" );

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje2))
                AddMessageFromRule("eiendomsAdresse_adresselinje2_utfylt");

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje3))
                AddMessageFromRule("eiendomsAdresse_adresselinje3_utfylt");

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Landkode))
                AddMessageFromRule("eiendomsAdresse_landkode_utfylt");

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_utfylt");

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Postnr))
                AddMessageFromRule("eiendomsAdresse_poststed_utfylt");

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Gatenavn))
                AddMessageFromRule("eiendomsAdresse_gatenavn_utfylt");

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Husnr))
                AddMessageFromRule("eiendomsAdresse_husnr_utfylt");

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Bokstav))
                AddMessageFromRule("eiendomsAdresse_bokstav_utfylt");

            if (!StringIs4digitNumber(eiendomsAdresse.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_4siffer");
        }

        private bool StringIs4digitNumber(string input)
        {
            try
            {
                int number = int.Parse(input);

                return (number >= 0 && number <= 9999);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
