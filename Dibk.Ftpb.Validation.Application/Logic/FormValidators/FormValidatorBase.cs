﻿using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public abstract class FormValidatorBase
    {
        protected abstract string XPathRoot { get; }
        protected abstract void InitializeValidatorConfig();
        public virtual ValidationResult StartValidation(ValidationInput validationInput)
        {
            InitializeValidatorConfig();
            InstantiateValidators();
            DefineValidationRules();
            Validate(validationInput);

            return ValidationResult;
        }
        protected abstract void InstantiateValidators();
        protected abstract void Validate(ValidationInput validationInput);
        protected abstract void DefineValidationRules();

        protected ValidationResult ValidationResult;

        protected void AccumulateValidationRules(List<ValidationRule> validationRules)
        {
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
