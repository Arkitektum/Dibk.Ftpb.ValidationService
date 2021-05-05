using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomsAdresseValidator : EntityValidatorBase
    {
        public EiendomsAdresseValidator() : base()
        {
        }
        public List<ValidationRule> Validate(string context, EiendomsAdresse eiendomsAdresse) 
        {
            string newContext = $"{context}/EiendomsAdresse";
            InitializeValidationRules(newContext);
            ValidateEntityFields(eiendomsAdresse);

            return ValidationRules;
        }

        public override void InitializeValidationRules(string context)
        {
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_adresselinje1_utfylt", xpath = $"{context}/Adresselinje1", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_adresselinje2_utfylt", xpath = $"{context}/Adresselinje2", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_adresselinje3_utfylt", xpath = $"{context}/Adresselinje3", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_landkode_utfylt", xpath = $"{context}/Landkode", validationResult = ValidationResultEnum.Unused, checklistReference = "13.1" });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_postnr_utfylt", xpath =   $"{context}/Postnr", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_poststed_utfylt", xpath = $"{context}/Poststed", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_gatenavn_utfylt", xpath = $"{context}/Gatenavn", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_husnr_utfylt", xpath = $"{context}/Husnr", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "eiendomsAdresse_bokstav_utfylt", xpath = $"{context}/Bokstav", validationResult = ValidationResultEnum.Unused });
        }

        public override void ValidateEntityFields(object entityData)
        {
            EiendomsAdresse eiendomsAdresse = (EiendomsAdresse)entityData;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_adresselinje1_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Adresselinje1) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_adresselinje2_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Adresselinje2) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_adresselinje3_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Adresselinje3) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_landkode_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Landkode) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_postnr_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Postnr) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_poststed_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Poststed) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_gatenavn_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Gatenavn) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_husnr_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Husnr) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("eiendomsAdresse_bokstav_utfylt")).FirstOrDefault().validationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Bokstav) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

        }
    }
}
