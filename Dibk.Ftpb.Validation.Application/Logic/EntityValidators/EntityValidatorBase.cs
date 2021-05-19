using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class EntityValidatorBase : IEntityValidator
    {
        public ValidationResult ValidationResult;
        protected string EntityName;
        protected string EntityXPath;

        public EntityValidatorBase(string xPath)
        {
            EntityXPath = xPath;
            ValidationResult = new();
            ValidationResult.ValidationRules = new List<ValidationRule>();
            ValidationResult.ValidationMessages = new List<ValidationMessage>();
        }

        public EntityValidatorBase(string xPath, string enityName) : this($"{xPath}/{enityName}")
        {}
        
        public abstract void InitializeValidationRules();

        public ValidationResult ResetValidationMessages()
        {
            ValidationResult.ValidationMessages = new();
            return ValidationResult;
        }
        protected void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult)
        {
            ValidationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            ValidationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }

        protected void AddValidationRule(string id, string xPath)
        {
            ValidationResult.ValidationRules.Add(new ValidationRule()
            {
                Id = id,
                Xpath = xPath
            });
        }

        public ValidationRule RuleToValidate(string id)
        {
            var validationRule = ValidationResult.ValidationRules.FirstOrDefault(r => r.Id.Equals(id)) ?? new ValidationRule()
            {
                Id = id,
                Message = $"Can't find rule with id:'{id}'.-"
            };

            return validationRule;
        }

        protected void AddValidationRule(string id, string xPath, string xmlElement)
        {
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = id, Xpath = $"{xPath}/{xmlElement}", XmlElement = xmlElement });
        }

        protected void AddMessageFromRule(string id, string xPath, List<string> messageParameters)
        {
            var rule = RuleToValidate(id);
            var XmlElement = String.IsNullOrEmpty(rule.XmlElement) ? null : $"/{rule.XmlElement}";

            string newXPath;
            newXPath = string.IsNullOrEmpty(xPath) ? rule.Xpath : $"{xPath}{XmlElement}"; // debug XmlElement in rule.Xpath!?

            var validationMessage = new ValidationMessage()
            {
                Reference = id,
                Xpath = newXPath,
                MessageParameters = messageParameters
            };

            ValidationResult.ValidationMessages.Add(validationMessage);
        }

        public void AddMessageFromRule(string id, List<string> messageParameters)
        {
            AddMessageFromRule(id, null, messageParameters);
        }
        public void AddMessageFromRule(string id, string xPath)
        {
            AddMessageFromRule(id, xPath, null);
        }
        public void AddMessageFromRule(string id)
        {
            AddMessageFromRule(id, null, null);
        }

        internal string GetXpathFromValidationRule(string id)
        {
            var validationRule = ValidationResult.ValidationRules.FirstOrDefault(r => r.Id == id);
            var xPath = String.Empty;

            if (validationRule != null)
                xPath = validationRule.Xpath;

            return xPath;
        }
    }
}
