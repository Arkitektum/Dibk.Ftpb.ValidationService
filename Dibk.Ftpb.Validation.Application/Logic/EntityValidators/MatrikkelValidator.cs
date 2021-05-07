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
    public class MatrikkelValidator : EntityValidatorBase
    {
        public MatrikkelValidator() : base()
        {
        }
        public List<ValidationRule> Validate(string context, Matrikkel matrikkel)
        {
            string newContext = $"{context}/eiendomsidentifikasjon";
            InitializeValidationRules(newContext);
            ValidateEntityFields(matrikkel);

            return ValidationRules;
        }

        public override void InitializeValidationRules(string context)
        {
            ValidationRules.Add(new ValidationRule() { Id = "kommunenummer_utfylt", Xpath = $"{context}/Kommunenummer", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "gaardsnummer_utfylt", Xpath = $"{context}/Gaardsnummer", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "bruksnummer_utfylt", Xpath = $"{context}/Bruksnummer", ValidationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { Id = "festenummer_utfylt", Xpath = $"{context}/Festenummer", ValidationResult = ValidationResultEnum.Unused, ChecklistReference = "13.1" });
            ValidationRules.Add(new ValidationRule() { Id = "seksjonsnummer_utfylt", Xpath =   $"{context}/Seksjonsnummer", ValidationResult = ValidationResultEnum.Unused });
        }

        public override void ValidateEntityFields(object entityData)
        {
            Matrikkel matrikkel = (Matrikkel)entityData;

            ValidationRules.Where(crit => crit.Id.Equals("kommunenummer_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(matrikkel.Kommunenummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("gaardsnummer_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(matrikkel.Gaardsnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("bruksnummer_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(matrikkel.Bruksnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("festenummer_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(matrikkel.Festenummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.Id.Equals("seksjonsnummer_utfylt")).FirstOrDefault().ValidationResult
                = (string.IsNullOrEmpty(matrikkel.Seksjonsnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

        }
    }
}
