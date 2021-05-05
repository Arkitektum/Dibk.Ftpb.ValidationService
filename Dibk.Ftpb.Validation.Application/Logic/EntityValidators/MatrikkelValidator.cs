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
            string newContext = $"{context}/Matrikkel";
            InitializeValidationRules(newContext);
            ValidateEntityFields(matrikkel);

            return ValidationRules;
        }

        public override void InitializeValidationRules(string context)
        {
            ValidationRules.Add(new ValidationRule() { id = "kommunenummer_utfylt", xpath = $"{context}/Kommunenummer", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "gaardsnummer_utfylt", xpath = $"{context}/Gaardsnummer", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "bruksnummer_utfylt", xpath = $"{context}/Bruksnummer", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "festenummer_utfylt", xpath = $"{context}/Festenummer", validationResult = ValidationResultEnum.Unused, checklistReference = "13.1" });
            ValidationRules.Add(new ValidationRule() { id = "seksjonsnummer_utfylt", xpath =   $"{context}/Seksjonsnummer", validationResult = ValidationResultEnum.Unused });
        }

        public override void ValidateEntityFields(object entityData)
        {
            Matrikkel matrikkel = (Matrikkel)entityData;

            ValidationRules.Where(crit => crit.id.Equals("kommunenummer_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(matrikkel.Kommunenummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("gaardsnummer_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(matrikkel.Gaardsnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("bruksnummer_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(matrikkel.Bruksnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("festenummer_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(matrikkel.Festenummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("seksjonsnummer_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(matrikkel.Seksjonsnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

        }
    }
}
