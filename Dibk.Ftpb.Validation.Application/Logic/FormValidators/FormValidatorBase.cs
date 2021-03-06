using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Text.RegularExpressions;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public abstract class FormValidatorBase
    {
        protected ValidationResult ValidationResult;
        protected IList<EntityValidatorNode> EntityValidatorTree;
        private readonly IValidationMessageComposer _validationMessageComposer;
        private readonly IChecklistService _checklistService;
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

        public virtual ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            InitializeValidatorConfig();
            InstantiateValidators();
            DefineValidationRules();
            Validate(validationInput);
            ValidationResult = _validationMessageComposer.ComposeValidationResult(XPathRoot, dataFormatVersion, ValidationResult, "NO");

            if (!ValidationResult.ValidationMessages.Any(x => x.Messagetype.Equals(ValidationResultSeverityEnum.CRITICAL)))
            {
                FilterValidationMessagesOnTiltakstyper(dataFormatVersion);
            }

            ValidationResult.TiltakstyperISoeknad = GetFormTiltakstyper().ToList();

            return ValidationResult;
        }

        protected void AccumulateValidationRules(List<ValidationRule> validationRules)
        {
            var whereNotAlreadyExists = validationRules.Where(x => !ValidationResult.ValidationRules.Any(y => y.XpathField == x.XpathField && y.Rule == x.Rule));
            ValidationResult.ValidationRules.AddRange(whereNotAlreadyExists);
        }


        protected void AccumulateValidationMessages(List<ValidationMessage> validationMessages, int? index = null)
        {
            if (validationMessages != null && index.HasValue)
            {
                foreach (var message in validationMessages)
                {
                    var newXpath = Helpers.ReplaceCurlyBracketInXPath(index.Value, message.XpathField);
                    message.XpathField = newXpath;
                }
                ValidationResult.ValidationMessages.AddRange(validationMessages);
            }
            else
            {
                ValidationResult.ValidationMessages.AddRange(validationMessages);
            }
        }

        private void FilterValidationMessagesOnTiltakstyper(string dataFormatVersion)
        {
            var tiltakstyper = GetFormTiltakstyper();
            if (tiltakstyper.Count() > 0)
            {
                var result = _checklistService.FilterValidationResult(dataFormatVersion, ValidationResult.ValidationMessages, tiltakstyper);
                ValidationResult.ValidationMessages = result.ToList();
            }
        }
        public static int GetArrayIndex(object[] objectArray)
        {
            var index = !(objectArray ?? Array.Empty<object>()).Any() ? 1 : objectArray.Count();
            return index;
        }

        protected void AddValidationRule(ValidationRuleEnum rule, FieldNameEnum? xmlElement = null, string overrideXpath = null)
        {
            //Note: different implementationthan than in the EntityValidatorBase
            var validationRuleTypeId = Helpers.GetEnumValidationRuleType(rule);
            var fieldNumberString = String.Empty;

            if (xmlElement != null)
            {
                var fieldNameNumber = Helpers.GetEnumFieldNameNumber(xmlElement);  //GetEnumEntityValidatorNumber
                fieldNumberString = $".{fieldNameNumber}";

            }

            var elementRuleId = $"{fieldNumberString}.{validationRuleTypeId}";


            //TODO Is this relevant now?
            var separator = xmlElement.HasValue ? "/" : "";

            string xPath = $"{overrideXpath}{separator}{xmlElement?.ToString()}";
            //**
            ValidationResult.ValidationRules.Add(new ValidationRule() { Rule = rule.ToString(), XpathField = xPath, XmlElement = xmlElement?.ToString(), Id = elementRuleId });
        }

        protected void AddMessageFromRule(ValidationRuleEnum id, string xmlElementPathName, string[] messageParameters = null, string preConditionField = null)
        {
            var idSt = id.ToString();
            var rule = RuleToValidate(idSt, xmlElementPathName);
            var validationMessage = new ValidationMessage()
            {
                Rule = idSt,
                Reference = rule.Id,
                XpathField = xmlElementPathName,
                PreCondition = preConditionField,
                MessageParameters = messageParameters
            };
            ValidationResult.ValidationMessages.Add(validationMessage);
        }


        public ValidationRule RuleToValidate(string rule, string xPath)
        {
            //Note: different regex implementationthan than in the EntityValidatorBase
            xPath = Regex.Replace(xPath, @"\[\d{1,}\]", "{0}");
            ValidationRule validationRule = ValidationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.Rule))
                .FirstOrDefault(r => r.Rule.Equals(rule) && (r.XpathField == xPath)) ?? new ValidationRule()
                {
                    Rule = rule,
                    Message = $"Can't find rule:'{rule}'.-"
                };
            return validationRule;
        }


    }

    public class FormDataAttribute : Attribute
    {
        public string DataFormatVersion { get; set; }
    }
}
