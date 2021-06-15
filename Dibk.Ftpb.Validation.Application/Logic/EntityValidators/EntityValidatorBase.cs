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

            if (parentValidator == EntityValidatorEnum.FormaaltypeValidator)
            {

            }

            string xPath = null;
            //if (grandParentValidator != null)
            //{
            //    xPath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).ParentValidatorXPathElement;

            //    if (parentValidator != null)
            //    {

            //        var xx = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(parentValidator) && x.ParentValidator.Equals(grandParentValidator)).ParentValidatorXPathElement;
            //        xPath = $"{xx}/{xPath}";
            //    }
            //    else
            //    {
            //        throw new ArgumentNullException($"'ParentValidator' er påkrevd når 'GrandParentValidator' er angitt ({grandParentValidator}).");
            //    }
            //    xPath = $"{formValidatorConfiguration.FormXPathRoot}/{xPath}";
            //}
            //else if (parentValidator != null)
            //{
            //    xPath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).ParentValidatorXPathElement;
            //    xPath = $"{formValidatorConfiguration.FormXPathRoot}/{xPath}";
            //}
            //else
            //{


            //    var XPathAfterParentForm = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).ParentValidatorXPathElement;
            //    xPath = $"{formValidatorConfiguration.FormXPathRoot}/{XPathAfterParentForm}";
            //}

            if (grandParentValidator != null)
            {
                //var grandparentXPath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(parentValidator) && x.ParentValidator.Equals(grandParentValidator)).ParentValidatorXPathElement;
                xPath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator) && x.GrandparentValidator.Equals(grandParentValidator)).ParentValidatorXPathElement;
                //xPath = $"{grandparentXPath}/{xPath}";
            }
            else if (parentValidator != null)
            {
                xPath = formValidatorConfiguration.Validators.FirstOrDefault(x => Enum.GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).ParentValidatorXPathElement;
            }
            else
            {

            }
            xPath = $"{formValidatorConfiguration.FormXPathRoot}/{xPath}";

            
            EntityXPath = $"{xPath}/{endXPathElement}";

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
            _validationResult.ValidationRules.Add(new ValidationRule() { Id = id, Xpath = xPath, XmlElement = xmlElement });
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

        //public void AddMessageFromRule(ValidationRuleEnum id, List<string> messageParameters)
        //{
        //    AddMessageFromRule(id, null, messageParameters);
        //}
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
