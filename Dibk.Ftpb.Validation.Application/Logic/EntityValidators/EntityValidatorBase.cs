using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class EntityValidatorBase : IEntityValidator
    {
        protected ValidationResult _validationResult;
        protected string EntityName;        

        public EntityValidatorBase()
        {
            //EntityXPath = xPath;
            _validationResult = new();
            _validationResult.ValidationRules = new List<ValidationRule>();
            _validationResult.ValidationMessages = new List<ValidationMessage>();
        }

        //public EntityValidatorBase(string xPath, string enityName) : this($"{xPath}/{enityName}")
        //{}
        
        protected abstract void InitializeValidationRules(string xPathForEntity);

        public ValidationResult ResetValidationMessages()
        {
            _validationResult.ValidationMessages = new();
            return _validationResult;
        }
        protected void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult)
        {
            _validationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            _validationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }

        public ValidationRule RuleToValidate(ValidationRuleEnum id)
        {
            var validationRule = _validationResult.ValidationRules.FirstOrDefault(r => r.Id.Equals(id)) ?? new ValidationRule()
            {
                Id = id,
                Message = $"Can't find rule with id:'{id}'.-"
            };

            return validationRule;
        }
        //string xPath = Regex.Replace(validationMessage.XpathField, @"\[([0-9]*)\]", "{0}");
        protected void AddValidationRule(ValidationRuleEnum id, string xPath)
        {
            //_validationResult.ValidationRules.Add(new ValidationRule() { Id = id, Xpath = xPath });
            AddValidationRule(id, xPath, null);
        }

        protected void AddValidationRule(ValidationRuleEnum id, string xPath, string xmlElement)
        {
            var separator = "";
            if (!string.IsNullOrEmpty(xmlElement))
            {
                separator = "/";
            }
            string bartePath = $"{xPath}{separator}{xmlElement}";
            bartePath = Regex.Replace(bartePath, @"\[([0-9]*)\]", "{0}");
            _validationResult.ValidationRules.Add(new ValidationRule() { Id = id, Xpath = bartePath, XmlElement = xmlElement });
        }


        protected void AddMessageFromRule(ValidationRuleEnum id, string xPath, List<string> messageParameters)
        {
            var rule = RuleToValidate(id);
            var XmlElement = String.IsNullOrEmpty(rule.XmlElement) ? null : $"/{rule.XmlElement}";

            string newXPath;
            newXPath = string.IsNullOrEmpty(xPath) ? rule.Xpath : $"{xPath}{XmlElement}"; // debug XmlElement in rule.Xpath!?

            var validationMessage = new ValidationMessage()
            {
                Reference = id,
                XpathField = newXPath,
                MessageParameters = messageParameters
            };

            _validationResult.ValidationMessages.Add(validationMessage);
        }

        public void AddMessageFromRule(ValidationRuleEnum id, List<string> messageParameters)
        {
            AddMessageFromRule(id, null, messageParameters);
        }
        public void AddMessageFromRule(ValidationRuleEnum id, string xPath)
        {
            AddMessageFromRule(id, xPath, null);
        }
        public void AddMessageFromRule(ValidationRuleEnum id)
        {
            AddMessageFromRule(id, null, null);
        }

        internal string GetXpathFromValidationRule(ValidationRuleEnum id)
        {
            var validationRule = _validationResult.ValidationRules.FirstOrDefault(r => r.Id == id);
            var xPath = String.Empty;

            if (validationRule != null)
                xPath = validationRule.Xpath;

            return xPath;
        }
    }
}
