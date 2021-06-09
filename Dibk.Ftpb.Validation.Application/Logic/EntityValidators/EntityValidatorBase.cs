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
        protected string EntityName;
        protected string EntityXPath;
        public abstract string ruleXmlElement { get; }
        protected ValidationResult _validationResult;

        ValidationResult IEntityValidator.ValidationResult { get => _validationResult; set => _validationResult = value; }

        public EntityValidatorBase(EntityValidatorOrchestrator entityValidatorOrchestrator) 
            : this(entityValidatorOrchestrator, null) { }
        
        public EntityValidatorBase(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum? parentValidator)
        {
            string parentXPath = null;
            if (parentValidator == null)
            {
                parentXPath = entityValidatorOrchestrator.ValidatorFormXPath;
            }
            else
            {
                var XPathAfterParentForm = entityValidatorOrchestrator.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).XPathAfterParent;
                parentXPath = $"{entityValidatorOrchestrator.ValidatorFormXPath}/{XPathAfterParentForm}";
            }

            _validationResult = new ValidationResult();
            _validationResult.ValidationRules = new List<ValidationRule>();
            _validationResult.ValidationMessages = new List<ValidationMessage>();

            EntityXPath = $"{parentXPath}{ruleXmlElement}";
        }

        
        protected abstract void InitializeValidationRules(string xPathForEntity);

        public ValidationResult ResetValidationMessages()
        {
            _validationResult.ValidationMessages = new List<ValidationMessage>();
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
