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
        public string _entityXPath;
        //public abstract string ruleXmlElement { get; set; }
        protected ValidationResult _validationResult;
        private EntityValidatorNode _entity;
        ValidationResult IEntityValidator.ValidationResult { get => _validationResult; set => _validationResult = value; }

        public EntityValidatorBase(IList<EntityValidatorNode> entityValidatorTree, int? nodeId = null)
        {
            _validationResult = new ValidationResult();
            _validationResult.ValidationRules = new List<ValidationRule>();
            _validationResult.ValidationMessages = new List<ValidationMessage>();

            var validatorName = this.GetType().Name;

            var node = GetEntityValidatorNode(entityValidatorTree, nodeId, validatorName);
            _entity = node;
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
        protected void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult, int? index = null)
        {
            _validationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            if (newValudationResult.ValidationMessages != null && index.HasValue)
            {
                foreach (var message in newValudationResult.ValidationMessages)
                {
                    var newXpath = Helpers.ReplaceCurlyBracketInXPath(index.Value, message.XpathField);
                    message.XpathField = newXpath;
                    _validationResult.ValidationMessages.Add(message);
                }
            }
            else
            {
                _validationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
            }
        }

        protected void AddValidationRule(ValidationRuleEnum rule, FieldNameEnum? xmlElement = null, string overrideXpath = null)
        {
            var separator = "";
            string elementRuleId = null;
            ValidationRuleEnum validationRule = (ValidationRuleEnum)rule;
            var validationRuleTypeId = Helpers.GetEnumValidationRuleType(validationRule);
            var fieldNumberString = String.Empty;

            if (xmlElement != null)
            {
                var fieldNameNumber = Helpers.GetEnumFieldNameNumber(xmlElement);  //GetEnumEntityValidatorNumber
                fieldNumberString = $".{fieldNameNumber}";

            }

            elementRuleId = $"{_ruleIdPath}{fieldNumberString}.{validationRuleTypeId}";

            var ruleXpath = overrideXpath?? _entityXPath;

            //TODO Is this relevant now?
            //**
            var xmlElementString = xmlElement?.ToString();
            if (!string.IsNullOrEmpty(xmlElementString))
            {
                separator = "/";
            }

            string xPath = $"{ruleXpath}{separator}{xmlElementString}";

            if (xPath.Contains("sjekklistepunkt/kodebeskrivelse"))
            {
                var xx = "";
            }
            //**
            _validationResult.ValidationRules.Add(new ValidationRule() { Rule = rule.ToString(), Xpath = xPath, XmlElement = xmlElementString, Id = elementRuleId ?? _ruleIdPath });
        }

        protected void AddMessageFromRule(ValidationRuleEnum id, FieldNameEnum? fieldName = null, string[] messageParameters = null, string preConditionField = null)
        {
            var xpathNew = fieldName.HasValue ? $"{_entity?.EntityXPath}/{fieldName.ToString()}" : null;
            AddMessageFromRule(id, xpathNew, messageParameters, preConditionField);
        }

        //Add this method to override xPath if the validation value is not from the same 'entity'
        public void AddMessageFromRule(ValidationRuleEnum id, string overrideXpath, string[] messageParameters = null, string preConditionField = null)
        {
            var idSt = id.ToString();
            var xpathNew = overrideXpath ?? _entity?.EntityXPath;

            var rule = RuleToValidate(idSt, xpathNew);

            var validationMessage = new ValidationMessage()
            {
                Rule = idSt,
                Reference = rule.Id ?? _ruleIdPath,
                XpathField = xpathNew,
                PreCondition = preConditionField,
                MessageParameters = messageParameters
            };

            _validationResult.ValidationMessages.Add(validationMessage);
        }


        //**
        public bool IsAnyValidationMessagesWithXpath(string xpath, int? index = null)
        {
            var newXpath = index.HasValue ? Helpers.ReplaceCurlyBracketInXPath(index.Value, xpath) : xpath;

            var ruleFounded = _validationResult.ValidationMessages.Any(r => !string.IsNullOrEmpty(r.XpathField) && r.XpathField.Equals(newXpath, StringComparison.OrdinalIgnoreCase));
            return ruleFounded;
        }
        public bool RuleIsValid(ValidationRuleEnum ruleEnum, string xPath = null)
        {
            var rule = ruleEnum.ToString();
            xPath ??= _entityXPath;

            var validationMessage = _validationResult.ValidationMessages.Where(r => !string.IsNullOrEmpty(r.Rule))
                .FirstOrDefault(r => r.Rule.Equals(rule) && (r.XpathField == xPath));

            return validationMessage == null;
        }

        public static int GetArrayIndex(object[] objectArray)
        {
            var index = !(objectArray ?? Array.Empty<object>()).Any() ? 1 : objectArray.Count();
            return index;
        }

        //**Add rules with dynamic enum 
        public ValidationRule RuleToValidate(string rule, string xPath)
        {
            xPath = xPath?.Replace("[", "{").Replace("]", "}");

            ValidationRule validationRule = _validationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.Rule))
                .FirstOrDefault(r => r.Rule.Equals(rule) && (r.Xpath == xPath)) ?? new ValidationRule()
                {
                    Rule = rule,
                    Message = $"Can't find rule:'{rule}'.-"
                };
            return validationRule;
        }

    }
}
