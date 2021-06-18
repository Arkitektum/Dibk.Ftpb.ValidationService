using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class EntityValidatorBase : IEntityValidator
    {
        protected string EntityName;
        public string RulePath;
        private string EntityXPath;
        public abstract string ruleXmlElement { get; set; }
        protected ValidationResult _validationResult;

        ValidationResult IEntityValidator.ValidationResult { get => _validationResult; set => _validationResult = value; }

        public EntityValidatorBase(FormValidatorConfiguration formValidatorConfiguration)
            : this(formValidatorConfiguration, null, null, null) { }

        public EntityValidatorBase(FormValidatorConfiguration formValidatorConfiguration, string xmlElement)
            : this(formValidatorConfiguration, null, null, xmlElement) { }

        public EntityValidatorBase(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum? parentValidator)
            : this(formValidatorConfiguration, parentValidator, null, null) { }

        public EntityValidatorBase(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum? parentValidator, EntityValidatorEnum? grandParentValidator)
            : this(formValidatorConfiguration, parentValidator, grandParentValidator, null) { }

        private EntityValidatorBase(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum? parentValidator, EntityValidatorEnum? grandParentValidator, string xmlElement)
        {
            _validationResult = new ValidationResult();
            _validationResult.ValidationRules = new List<ValidationRule>();
            _validationResult.ValidationMessages = new List<ValidationMessage>();

            string endXPathElement;
            if (xmlElement != null)
            {
                endXPathElement = xmlElement;
            }
            else
            {
                endXPathElement = ruleXmlElement;
            }

            string xPathBetweenRootAndEndElement = null;

            if (grandParentValidator != null)
            {
                xPathBetweenRootAndEndElement = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator) && x.GrandparentValidator.Equals(grandParentValidator)).XPathBetweenRootAndEndElement;
                RulePath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator) && x.GrandparentValidator.Equals(grandParentValidator)).RulePath;
            }
            else if (parentValidator != null)
            {
                xPathBetweenRootAndEndElement = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).XPathBetweenRootAndEndElement;
                RulePath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).RulePath;
            }

            if (xPathBetweenRootAndEndElement != null)
            {
                xPathBetweenRootAndEndElement = $"/{xPathBetweenRootAndEndElement}";
                RulePath = $"{RulePath}";
            }
            else
            {
                RulePath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator == null).RulePath;
                RulePath = $"{RulePath}";
            }

            EntityXPath = $"{formValidatorConfiguration.FormXPathRoot}{xPathBetweenRootAndEndElement}/{endXPathElement}";
            RulePath = $"{formValidatorConfiguration.FormXPathRoot}{RulePath}.{endXPathElement}";
            
            InitializeValidationRules();
        }


        protected abstract void InitializeValidationRules();

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
        protected void AddValidationRule(ValidationRuleEnum id)
        {
            AddValidationRule(id, null);
        }

        protected void AddValidationRule(ValidationRuleEnum id, string xmlElement)
        {
            var separator = "";
            if (!string.IsNullOrEmpty(xmlElement))
            {
                separator = "/";
            }
            string xPath = $"{EntityXPath}{separator}{xmlElement}";
            //xPath = Regex.Replace(xPath, @"\[([0-9]*)\]", "{0}");
            _validationResult.ValidationRules.Add(new ValidationRule() { Id = id, Xpath = xPath, XmlElement = xmlElement, RulePath = RulePath });
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

        public void AddMessageFromRule(ValidationRuleEnum id, string xPath)
        {
            AddMessageFromRule(id, xPath, null);
        }
        /// <summary>
        /// Use when validating if collection is empty (because modelEntity.DataModelXpath is not present)
        /// </summary>
        /// <param name="id"></param>
        public void AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum id)
        {
            AddMessageFromRule(id, null, null);
        }
        //TODO Test this
        public ValidationRule RuleToValidate(string id)
        {
            var validationRule = _validationResult.ValidationRules.FirstOrDefault(r => r.IdSt.Equals(id)) ?? new ValidationRule()
            {
                IdSt = id,
                Message = $"Can't find rule with id:'{id}'.-"
            };

            return validationRule;
        }
        protected void AddValidationRule(object id, string xmlElement)
        {
            var separator = "";
            if (!string.IsNullOrEmpty(xmlElement))
            {
                separator = "/";
            }
            string xPath = $"{EntityXPath}{separator}{xmlElement}";
            //xPath = Regex.Replace(xPath, @"\[([0-9]*)\]", "{0}");
            _validationResult.ValidationRules.Add(new ValidationRule() { IdSt = id.ToString(), Xpath = xPath, XmlElement = xmlElement });
        }
        protected void AddMessageFromRule(string id, string xPath, List<string> messageParameters)
        {
            var rule = RuleToValidate(id);
            var XmlElement = String.IsNullOrEmpty(rule.XmlElement) ? null : $"/{rule.XmlElement}";

            string newXPath;
            newXPath = string.IsNullOrEmpty(xPath) ? rule.Xpath : $"{xPath}{XmlElement}"; // debug XmlElement in rule.Xpath!?

            var validationMessage = new ValidationMessage()
            {
                ReferenceSt = id,
                XpathField = newXPath,
                MessageParameters = messageParameters
            };

            _validationResult.ValidationMessages.Add(validationMessage);
        }
        public void AddMessageFromRule(object id)
        {
            var idSt = id.ToString();
            AddMessageFromRule(idSt, null, null);
        }

        //**

        public bool? IsAnyValidationMessagesWithXpath(string xpath, string elemetNode = null)
        {
            if (string.IsNullOrEmpty(xpath))
                return null;

            if (Helpers.ObjectIsNullOrEmpty(_validationResult.ValidationRules))
                return null;

            var xpathToFind = string.IsNullOrEmpty(elemetNode) ? xpath : $"{xpath}/{elemetNode}";
            var ruleFounded = _validationResult.ValidationMessages.Any(r => r.XpathField.Equals(xpathToFind, StringComparison.OrdinalIgnoreCase));

            return ruleFounded;
        }

    }
}
