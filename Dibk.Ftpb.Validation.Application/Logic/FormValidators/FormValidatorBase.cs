﻿using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public abstract class FormValidatorBase
    {
        protected ValidationResult ValidationResult;
        protected IList<EntityValidatorNode> EntityValidatorTree;
        private readonly IValidationMessageComposer _validationMessageComposer;
        private readonly IChecklistService _checklistService;

        public string DataFormatId;
        public string DataFormatVersion;
        protected abstract string XPathRoot { get; }
        protected abstract void InitializeValidatorConfig();
        protected abstract IEnumerable<string> GetFormTiltakstyper();
        protected abstract void InstantiateValidators();
        protected abstract void Validate(ValidationInput validationInput);
        protected abstract void DefineValidationRules();

        public FormValidatorBase(IValidationMessageComposer validationMessageComposer, IChecklistService checklistService = null)
        {
            _validationMessageComposer = validationMessageComposer;
            _checklistService = checklistService;
            ValidationResult = new ValidationResult
            {
                ValidationMessages = new List<ValidationMessage>(),
                ValidationRules = new List<ValidationRule>()
            };

        }

        protected string GetDataFormatVersion(Type t)
        {
            FormDataAttribute myAtt = (FormDataAttribute)Attribute.GetCustomAttribute(t, typeof(FormDataAttribute));
            return myAtt.DataFormatVersion;
        }
        public virtual ValidationResult StartValidation(ValidationInput validationInput)
        {

            var customAttributes = this.GetType().GetCustomAttributes(typeof(FormDataAttribute), true).FirstOrDefault() as FormDataAttribute;

            DataFormatId = customAttributes?.DataFormatId;
            DataFormatVersion = customAttributes?.DataFormatVersion;

            InitializeValidatorConfig();
            InstantiateValidators();
            DefineValidationRules();
            Validate(validationInput);
            ValidationResult = _validationMessageComposer.ComposeValidationResult(XPathRoot, DataFormatVersion, ValidationResult, "NO");

            if (!ValidationResult.ValidationMessages.Any(x => x.Messagetype.Equals(ValidationResultSeverityEnum.CRITICAL)))
            {
                FilterValidationMessagesOnTiltakstyper();
            }

            return ValidationResult;
        }

        protected void AccumulateValidationRules(List<ValidationRule> validationRules)
        {
            var whereNotAlreadyExists = validationRules.Where(x => !ValidationResult.ValidationRules.Any(y => y.Xpath == x.Xpath && y.Rule == x.Rule));
            ValidationResult.ValidationRules.AddRange(whereNotAlreadyExists);
        }
        protected void AddValidationRule(ValidationRuleEnum rule, FieldNameEnum? xmlElementName = null, string overrideXpath = null)
        {
            var validationRuleTypeId = Helpers.GetEnumValidationRuleType(rule);
            var fieldNumberString = String.Empty;

            if (xmlElementName != null)
            {
                var fieldNameNumber = Helpers.GetEnumFieldNameNumber(xmlElementName);  //GetEnumEntityValidatorNumber
                fieldNumberString = $".{fieldNameNumber}";
            }

            var elementRuleId = $"{fieldNumberString}.{validationRuleTypeId}";

            var separator = xmlElementName.HasValue ? "/" : "";
            string xPath = $"{overrideXpath}{separator}{xmlElementName?.ToString()}";

            ValidationResult.ValidationRules.Add(new ValidationRule() { Rule = rule.ToString(), Xpath = xPath, XmlElement = xmlElementName?.ToString(), Id = elementRuleId });
        }
        protected void AddMessageFromRule(ValidationRuleEnum id, FieldNameEnum? xmlElementName = null, string[] messageParameters = null, string preConditionField = null)
        {
            var idSt = id.ToString();

            string xmlElementXpath = xmlElementName.HasValue ? $"/{xmlElementName}" : "/";

            var xpathNew = $"{xmlElementXpath}";

            var rule = RuleToValidate(idSt, xpathNew);

            var validationMessage = new ValidationMessage()
            {
                Rule = idSt,
                Reference = rule.Id,
                XpathField = xpathNew,
                PreCondition = preConditionField,
                MessageParameters = messageParameters
            };

            ValidationResult.ValidationMessages.Add(validationMessage);
        }
        public ValidationRule RuleToValidate(string rule, string xPath)
        {
            xPath = xPath?.Replace("[", "{").Replace("]", "}");

            ValidationRule validationRule = ValidationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.Rule))
                .FirstOrDefault(r => r.Rule.Equals(rule) && (r.Xpath == xPath)) ?? new ValidationRule()
                {
                    Rule = rule,
                    Message = $"Can't find rule:'{rule}'.-"
                };
            return validationRule;
        }
        protected void AccumulateValidationMessages(List<ValidationMessage> validationMessages, int? index = null)
        {
            if (validationMessages != null && index.HasValue)
            {
                foreach (var message in validationMessages)
                {
                    var newXpath = Helpers.ReplaceCurlyBracketInXPath(index.Value, message.XpathField);
                    message.XpathField = newXpath;
                    ValidationResult.ValidationMessages.AddRange(validationMessages);
                }
            }
            else
            {
                ValidationResult.ValidationMessages.AddRange(validationMessages);
            }
        }

        private void FilterValidationMessagesOnTiltakstyper()
        {
            var tiltakstyper = GetFormTiltakstyper();
            if (tiltakstyper.Count() > 0)
            {
                var result = _checklistService.FilterValidationResult(DataFormatId, DataFormatVersion, ValidationResult.ValidationMessages, tiltakstyper);
                ValidationResult.ValidationMessages = result.ToList();
            }
        }
        public static int GetArrayIndex(object[] objectArray)
        {
            var index = !(objectArray ?? Array.Empty<object>()).Any() ? 1 : objectArray.Count();
            return index;
        }
    }

    public class FormDataAttribute : Attribute
    {
        public string DataFormatVersion { get; set; }
        public string DataFormatId { get; set; }
    }
}
