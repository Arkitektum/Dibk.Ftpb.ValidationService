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
    public class EiendomValidator : EntityValidatorBase
    {
        private EiendomsAdresseValidator _eiendomsAdresseValidator;
        public EiendomValidator() : base()
        {
            _eiendomsAdresseValidator = new EiendomsAdresseValidator();
        }
        public List<ValidationRule> Validate(string context, Eiendom eiendom)
        {
            string newContext = $"{context}/Eiendom";
            InitializeValidationRules(newContext);
            LocalValidate(newContext, eiendom);
            
            var eiendomsAdresseValidationRules = _eiendomsAdresseValidator.Validate(newContext, eiendom.Adresse).ToList();
            ValidationRules.AddRange(eiendomsAdresseValidationRules);

            return ValidationRules;
        }

        private void LocalValidate(string context, Eiendom eiendom)
        {
            ValidationRules.Where(crit => crit.id.Equals("bygningsnummer_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(eiendom.Bygningsnummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("bolignummer_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(eiendom.Bolignummer)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;

            ValidationRules.Where(crit => crit.id.Equals("kommunenavn_utfylt")).FirstOrDefault().validationResult
                = (string.IsNullOrEmpty(eiendom.Kommunenavn)) ? ValidationResultEnum.ValidationFailed : ValidationResultEnum.ValidationOk;
        }

        private void InitializeValidationRules(string context)
        {
            ValidationRules.Add(new ValidationRule() { id = "bygningsnummer_utfylt", xpath = $"{context}/Bygningsnummer", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "bolignummer_utfylt", xpath = $"{context}/Bolignummer", validationResult = ValidationResultEnum.Unused });
            ValidationRules.Add(new ValidationRule() { id = "kommunenavn_utfylt", xpath = $"{context}/Kommunenavn", validationResult = ValidationResultEnum.Unused });
        }
    }
}
