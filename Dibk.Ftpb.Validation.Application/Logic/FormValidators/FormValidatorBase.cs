using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public abstract class FormValidatorBase
    {
        protected ValidationResult ValidationResult;

        protected void AccumulateValidationRules(List<ValidationRule> validationRules)
        {
            //ValidationResult ??= new ValidationResult();
            //ValidationResult.ValidationRules ??= new List<ValidationRule>();
            //ValidationResult.ValidationMessages ??= new List<ValidationMessage>();

            //var whereNotAlreadyExists = validationResult.ValidationRules.Where(x => !ValidationResult.ValidationRules.Any(y => y.Xpath == x.Xpath && y.Id == x.Id));
            //ValidationResult.ValidationRules.AddRange(whereNotAlreadyExists);
            //ValidationResult.ValidationMessages.AddRange(validationResult.ValidationMessages);
            
            ValidationResult ??= new ValidationResult();
            ValidationResult.ValidationRules ??= new List<ValidationRule>();

            var whereNotAlreadyExists = validationRules.Where(x => !ValidationResult.ValidationRules.Any(y => y.Xpath == x.Xpath && y.Id == x.Id));
            ValidationResult.ValidationRules.AddRange(whereNotAlreadyExists);
        }

        protected void AccumulateValidationMessages(List<ValidationMessage> validationMessages)
        {
            ValidationResult ??= new ValidationResult();
            ValidationResult.ValidationMessages ??= new List<ValidationMessage>();
            ValidationResult.ValidationMessages.AddRange(validationMessages);
        }

    }
}
