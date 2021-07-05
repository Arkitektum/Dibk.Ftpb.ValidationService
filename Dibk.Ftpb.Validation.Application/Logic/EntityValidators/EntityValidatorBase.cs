using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Utils;
using Elasticsearch.Net;
using static System.Enum;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class EntityValidatorBase : IEntityValidator
    {
        protected string EntityName;
        public string _rulePath;
        private string _entityXPath;
        public abstract string ruleXmlElement { get; set; }
        protected ValidationResult _validationResult;

        ValidationResult IEntityValidator.ValidationResult { get => _validationResult; set => _validationResult = value; }

        public EntityValidatorBase(IList<EntityValidatorNode> entityValidationGroup, int? nodeId = null, string xmlElement = null)
        {
            _validationResult = new ValidationResult();
            _validationResult = new ValidationResult();
            _validationResult.ValidationRules = new List<ValidationRule>();
            _validationResult.ValidationMessages = new List<ValidationMessage>();
            var validatorName = xmlElement ?? this.GetType().Name;
            var rule = GetEntityValidationGroup(entityValidationGroup, nodeId, validatorName);

            _rulePath = rule.RulePath;
            _entityXPath = rule.EntityXPath;
            InitializeValidationRules();
        }

        private static EntityValidatorNode GetEntityValidationGroup(IList<EntityValidatorNode> entityValidationGroup, int? treeNodeId, string validatorName)
        {
            EntityValidatorNode entityValidationInfo;
            if (treeNodeId.HasValue)
                entityValidationInfo = entityValidationGroup.FirstOrDefault(e => e.Id.Equals(treeNodeId.Value));
            else
                entityValidationInfo = entityValidationGroup.FirstOrDefault(e => GetName(typeof(EntityValidatorEnum), e.EnumId).Equals(validatorName));

            if (entityValidationInfo == null)
            {
                foreach (EntityValidatorNode validationGroup in entityValidationGroup)
                {
                    entityValidationInfo = GetEntityValidationGroup(validationGroup.Children, treeNodeId, validatorName);
                    if (entityValidationInfo != null)
                        break;
                }
            }
            return entityValidationInfo;
        }


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
                xPathBetweenRootAndEndElement = formValidatorConfiguration.Validators.FirstOrDefault(x => GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator) && x.GrandparentValidator.Equals(grandParentValidator)).XPathBetweenRootAndEndElement;
                _rulePath = formValidatorConfiguration.Validators.FirstOrDefault(x => GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator) && x.GrandparentValidator.Equals(grandParentValidator)).RulePath;
            }
            else if (parentValidator != null)
            {
                xPathBetweenRootAndEndElement = formValidatorConfiguration.Validators.FirstOrDefault(x => GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).XPathBetweenRootAndEndElement;
                _rulePath = formValidatorConfiguration.Validators.FirstOrDefault(x => GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator.Equals(parentValidator)).RulePath;
            }

            if (xPathBetweenRootAndEndElement != null)
            {
                xPathBetweenRootAndEndElement = $"/{xPathBetweenRootAndEndElement}";
                _rulePath = $"{_rulePath}";
            }
            else
            {
                _rulePath = formValidatorConfiguration.Validators.FirstOrDefault(x => GetName(typeof(EntityValidatorEnum), x.EntityValidator).Equals(this.GetType().Name) && x.ParentValidator == null).RulePath;
                _rulePath = $"{_rulePath}";
            }

            _entityXPath = $"{formValidatorConfiguration.FormXPathRoot}{xPathBetweenRootAndEndElement}/{endXPathElement}";
            _rulePath = $"{formValidatorConfiguration.FormXPathRoot}{_rulePath}.{endXPathElement}";

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
            string xPath = $"{_entityXPath}{separator}{xmlElement}";
            //xPath = Regex.Replace(xPath, @"\[([0-9]*)\]", "{0}");
            _validationResult.ValidationRules.Add(new ValidationRule() { Id = id, Xpath = xPath, XmlElement = xmlElement, RulePath = _rulePath });
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
            var validationRules = _validationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.IdSt))
                .Where(r => r.IdSt.Equals(id)).ToArray();
            var validationRule = _validationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.IdSt)).FirstOrDefault(r => r.IdSt.Equals(id)) ?? new ValidationRule()
            {
                IdSt = id,
                Message = $"Can't find rule with id:'{id}'.-"
            };

            return validationRule;
        }
        protected void AddValidationRule(object id, string xmlElement)
        {
            var separator = "";
            int? enumHashCode;
            string elementRuleId = null;
            if (id is Enum)
            {
                enumHashCode = id.GetHashCode();
                elementRuleId = $"{_rulePath}.{enumHashCode}";
            }

            if (!string.IsNullOrEmpty(xmlElement))
            {
                separator = "/";
            }
            string xPath = $"{_entityXPath}{separator}{xmlElement}";
            //xPath = Regex.Replace(xPath, @"\[([0-9]*)\]", "{0}");

            _validationResult.ValidationRules.Add(new ValidationRule() { IdSt = id.ToString(), Xpath = xPath, XmlElement = xmlElement, RulePath = elementRuleId ?? _rulePath });

        }
        protected void AddMessageFromRule(string id, string xPath, List<string> messageParameters)
        {
            var rule = RuleToValidate(id);

            string newXPath;
            if (!string.IsNullOrEmpty(xPath))
            {
                newXPath = string.IsNullOrEmpty(rule.XmlElement) ? xPath : $"{xPath}/{rule.XmlElement}";
            }
            else
            {
                newXPath = rule.Xpath;
            }

            var validationMessage = new ValidationMessage()
            {
                ReferenceSt = id,
                RulePath = rule.RulePath ?? _rulePath,
                XpathField = newXPath,
                MessageParameters = messageParameters
            };

            _validationResult.ValidationMessages.Add(validationMessage);
        }
        public void AddMessageFromRule(object id, string xPath = null)
        {
            var idSt = id.ToString();
            AddMessageFromRule(idSt, xPath, null);
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
