using System;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using static System.Enum;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common
{
    public abstract class EntityValidatorBase : IEntityValidator
    {
        //public event EventHandler<ValidationResult> RulesAdded;

        //protected virtual void OnRulesAdded(ValidationResult validationResult)
        //{
        //    RulesAdded?.Invoke(this, validationResult);
        //}

        protected string EntityName;
        private string _ruleIdPath;
        private string _entityXPath;
        //public abstract string ruleXmlElement { get; set; }
        protected ValidationResult _validationResult;
        ValidationResult IEntityValidator.ValidationResult { get => _validationResult; set => _validationResult = value; }

        public EntityValidatorBase(IList<EntityValidatorNode> entityValidatorTree, int? nodeId = null)
        {
            _validationResult = new ValidationResult();
            _validationResult.ValidationRules = new List<ValidationRule>();
            _validationResult.ValidationMessages = new List<ValidationMessage>();

            var validatorName = this.GetType().Name;

            var node = GetEntityValidatorNode(entityValidatorTree, nodeId, validatorName);
            if (node != null)
            {
                _ruleIdPath = node.IdPath;
                _entityXPath = node.EntityXPath;
            }
            else
            {
                _ruleIdPath = "Can't find the node";
                _entityXPath = nodeId.HasValue ? $"Can't find Entity validator enum:'{validatorName}' with nodeId:'{nodeId}'" : $"Can't find Entity validator enum:'{validatorName}'.";
            }
            InitializeValidationRules();
            //OnRulesAdded(_validationResult);
        }

        private static EntityValidatorNode GetEntityValidatorNode(IList<EntityValidatorNode> entityValidationTree, int? treeNodeId, string validatorName)
        {
            EntityValidatorNode entityValidationNode;
            if (treeNodeId.HasValue)
                entityValidationNode = entityValidationTree.FirstOrDefault(e => e.NodeId.Equals(treeNodeId.Value));
            else
                entityValidationNode = entityValidationTree.FirstOrDefault(e => GetName(typeof(EntityValidatorEnum), e.EnumId).Equals(validatorName));

            if (entityValidationNode == null)
            {
                foreach (EntityValidatorNode validationGroup in entityValidationTree)
                {
                    entityValidationNode = GetEntityValidatorNode(validationGroup.Children, treeNodeId, validatorName);
                    if (entityValidationNode != null)
                        break;
                }
            }
            return entityValidationNode;
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

        protected void AddValidationRule(object rule)
        {
            AddValidationRule(rule, null, null);
        }

        protected void AddValidationRuleOverideXpath(object rule, string overrideXpath)
        {
            AddValidationRule(rule, null, overrideXpath);
        }


        protected void AddValidationRule(object rule, string xmlElement)
        {
            AddValidationRule(rule, xmlElement, null);
        }

        protected void AddValidationRule(object rule, FieldNameEnum xmlElement)
        {
            AddValidationRule(rule, xmlElement.ToString(), null);
        }

        protected void AddValidationRule(object rule, string xmlElement, string overrideXpath)
        {
            var separator = "";
            string elementRuleId = null;
            if (rule is Enum)
            {
                ValidationRuleEnum validationRule = (ValidationRuleEnum)rule;
                var validationRuleTypeId = Helpers.GetEnumValidationRuleType(validationRule);
                var fieldNumberString = String.Empty;

                if (xmlElement != null)
                {
                    if (TryParse(xmlElement, out FieldNameEnum fieldNameEnum))
                    {
                        fieldNameEnum = (FieldNameEnum)System.Enum.Parse(typeof(FieldNameEnum), xmlElement);
                        var fieldNameNumber = Helpers.GetEnumFieldNameNumber(fieldNameEnum);  //GetEnumEntityValidatorNumber
                        fieldNumberString = $".{fieldNameNumber}";
                    }
                }

                elementRuleId = $"{_ruleIdPath}{fieldNumberString}.{validationRuleTypeId}";
            }

            if (!string.IsNullOrEmpty(overrideXpath))
            {
                _entityXPath = overrideXpath;
            }

            if (!string.IsNullOrEmpty(xmlElement))
            {
                separator = "/";
            }

            string xPath = $"{_entityXPath}{separator}{xmlElement}";

            if (xPath.Contains("sjekklistepunkt/kodebeskrivelse"))
            {
                var xx = "";
            }

            _validationResult.ValidationRules.Add(new ValidationRule() { Rule = rule.ToString(), Xpath = xPath, XmlElement = xmlElement, Id = elementRuleId ?? _ruleIdPath });
        }


        protected void AddMessageFromRule(ValidationRuleEnum id, string xPath = null, string[] messageParameters = null)
        {

            var idSt = id.ToString();

            var rule = RuleToValidate(idSt, xPath);

            var validationMessage = new ValidationMessage()
            {
                Rule = idSt,
                Reference = rule.Id ?? _ruleIdPath,
                XpathField = xPath,
                MessageParameters = messageParameters
            };

            _validationResult.ValidationMessages.Add(validationMessage);
        }

        //**
        public bool? IsAnyValidationMessagesWithXpath(string xpath)
        {
            var ruleFounded = _validationResult.ValidationMessages.Any(r => r.XpathField.Equals(xpath, StringComparison.OrdinalIgnoreCase));
            return ruleFounded;
        }

        //**Add rules with dynamic enum 
        public ValidationRule RuleToValidate(string rule, string xPath)
        {
            xPath = xPath?.Replace("[", "{").Replace("]", "}");

            ValidationRule validationRule = _validationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.Rule))
                .FirstOrDefault(r => r.Rule.Equals(rule) && (r.Xpath == xPath));

            if (validationRule == null)
            {
                var cleanXpath = xPath?.Substring(xPath.IndexOf("/", StringComparison.Ordinal));

                var validationRules = _validationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.Rule))
                    .Where(r => r.Rule.Equals(rule) && (r.Xpath == cleanXpath)).ToArray();

                if (validationRules.Any())
                {
                    if (validationRules.Count() == 1)
                    {
                        validationRule = validationRules.FirstOrDefault();
                    }
                    else
                    {
                        validationRule = new ValidationRule()
                        {
                            Rule = rule,
                            Message = $"More than 1 rule found with xpath:'{cleanXpath}'.-"
                        };
                    }
                }
                else
                {
                    validationRule = new ValidationRule()
                    {
                        Rule = rule,
                        Message = $"Can't find rule:'{rule}'.-"
                    };
                }
            }

            return validationRule;
        }


    }
}
