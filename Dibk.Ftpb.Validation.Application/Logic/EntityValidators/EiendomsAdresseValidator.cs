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
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje1_utfylt", Xpath = $"{context}/Adresselinje1", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje2_utfylt", Xpath = $"{context}/Adresselinje2", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_adresselinje3_utfylt", Xpath = $"{context}/Adresselinje3", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_landkode_utfylt", Xpath = $"{context}/Landkode", ValidationResult = ValidationResultEnum.Unused, ChecklistReference = "13.1" });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_postnr_utfylt", Xpath =   $"{context}/Postnr", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_poststed_utfylt", Xpath = $"{context}/Poststed", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_gatenavn_utfylt", Xpath = $"{context}/Gatenavn", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_husnr_utfylt", Xpath = $"{context}/Husnr", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_bokstav_utfylt", Xpath = $"{context}/Bokstav", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "eiendomsAdresse_postnr_4siffer", Xpath = $"{context}/Postnr", ValidationResult = ValidationResultEnum.Unused });
        }

        public override void ValidateEntityFields(object entityData)
        {
            EiendomsAdresse eiendomsAdresse = (EiendomsAdresse)entityData;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_adresselinje1_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Adresselinje1) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_adresselinje2_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Adresselinje2) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_adresselinje3_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Adresselinje3) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_landkode_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Landkode) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_postnr_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Postnr) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_poststed_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Poststed) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_gatenavn_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Gatenavn) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_husnr_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Husnr) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_bokstav_utfylt")).FirstOrDefault().ValidationResult
                = string.IsNullOrEmpty(eiendomsAdresse.Bokstav) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;


            ValidationRules.Where(crit => crit.Id.Equals("eiendomsAdresse_postnr_4siffer")).FirstOrDefault().ValidationResult
                =!StringIs4digitNumber(eiendomsAdresse.Postnr) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;


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
