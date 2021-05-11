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
        public EiendomsAdresseValidator(string templateXPath) : base()
        {
            InitializeValidationRules(templateXPath);
        }
        public ValidationResult Validate(string xPath, EiendomsAdresse eiendomsAdresse)
        {
            ValidateEntityFields(xPath, eiendomsAdresse);

            return ValidationResult;
        }

        public override void InitializeValidationRules(string xPath)
        {
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje1_utfylt", Xpath = xPath, XmlElement = "/adresselinje1" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje2_utfylt", Xpath = xPath, XmlElement = "/adresselinje2" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje3_utfylt", Xpath = xPath, XmlElement = "/adresselinje3" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_landkode_utfylt", Xpath = xPath, XmlElement = "/landkode", ChecklistReference = "13.1" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_postnr_utfylt", Xpath = xPath, XmlElement = "/postnr" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_poststed_utfylt", Xpath = xPath, XmlElement = "/poststed" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_gatenavn_utfylt", Xpath = xPath, XmlElement = "/gatenavn" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_husnr_utfylt", Xpath = xPath, XmlElement = "/husnr" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_bokstav_utfylt", Xpath = xPath, XmlElement = "/bokstav" });
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_postnr_4siffer", Xpath = xPath, XmlElement = "/postnr" });
        }

        public void ValidateEntityFields(string xPath, EiendomsAdresse eiendomsAdresse)
        {
            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje1))
                AddMessageFromRule("eiendomsAdresse_adresselinje1_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje2))
                AddMessageFromRule("eiendomsAdresse_adresselinje2_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Adresselinje3))
                AddMessageFromRule("eiendomsAdresse_adresselinje3_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Landkode))
                AddMessageFromRule("eiendomsAdresse_landkode_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Poststed))
                AddMessageFromRule("eiendomsAdresse_poststed_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Gatenavn))
                AddMessageFromRule("eiendomsAdresse_gatenavn_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Husnr))
                AddMessageFromRule("eiendomsAdresse_husnr_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.Bokstav))
                AddMessageFromRule("eiendomsAdresse_bokstav_utfylt", xPath);

            if (!StringIs4digitNumber(eiendomsAdresse.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_4siffer", xPath);
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
