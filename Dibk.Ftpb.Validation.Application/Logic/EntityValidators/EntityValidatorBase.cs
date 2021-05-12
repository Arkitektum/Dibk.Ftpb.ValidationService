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
        public EntityValidatorBase()
        {
            ValidationResult = new();
            ValidationResult.ValidationRules = new List<ValidationRule>();
            ValidationResult.ValidationMessages = new List<ValidationMessage>();
        }

        public abstract void InitializeValidationRules(string context);

        public void AddValidationRule(string id, string xPath)
        {
            ValidationResult.ValidationRules.Add(new ValidationRule()
            {
                Id = id,
                Xpath = xPath
            });
        }

        protected ValidationResult ReturnValidationResult(ValidationResult validationResult)
        {
            ValidationResult returnInstance = new();
            returnInstance.ValidationRules = validationResult.ValidationRules;
            returnInstance.ValidationMessages = validationResult.ValidationMessages;

            //Empty messages to avoid duplicates when next call to Validate is done
            ValidationResult.ValidationMessages = new List<ValidationMessage>();

            return returnInstance;
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

        public void AddValidationRule(string id, string xPath, string xmlElement)
        {
            ValidationResult.ValidationRules.Add(new ValidationRule() { Id = id, Xpath = $"{xPath}/{xmlElement}", XmlElement = xmlElement });
        }

        public void AddMessageFromRule(string id, string xPath, List<string> messageParameters)
        {
            var rule = RuleToValidate(id);
            var XmlElement = String.IsNullOrEmpty(rule.XmlElement) ? null : $"/{rule.XmlElement}";
            var validationMessage = new ValidationMessage()
            {
                Reference = id,
                Xpath = $"{xPath}{XmlElement}", 
                MessageParameters = messageParameters
            };


            ValidationResult.ValidationMessages.Add(validationMessage);

        }

        public void AddMessageFromRule(string id, List<string> messageParameters)
        {
            string xPath = GetXpathFromValidationRule(id);
            AddMessageFromRule(id, null, messageParameters);
        }
        public void AddMessageFromRule(string id, string xPath)
        {
            AddMessageFromRule(id, xPath, null);
        }
        public void AddMessageFromRule(string id)
        {
            string xPath = GetXpathFromValidationRule(id);
            AddMessageFromRule(id, xPath, null);
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
