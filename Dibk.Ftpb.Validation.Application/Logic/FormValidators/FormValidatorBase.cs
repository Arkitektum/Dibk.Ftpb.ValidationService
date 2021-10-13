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

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public abstract class FormValidatorBase
    {
        protected ValidationResult _validationResult;
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
            _validationResult = new ValidationResult
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
            _validationResult = _validationMessageComposer.ComposeValidationResult(XPathRoot, dataFormatVersion, _validationResult, "NO");

            if (!_validationResult.ValidationMessages.Any(x => x.Messagetype.Equals(ValidationResultSeverityEnum.CRITICAL)))
            {
                FilterValidationMessagesOnTiltakstyper(dataFormatVersion);
            }

            return _validationResult;
        }

        protected void AccumulateValidationRules(List<ValidationRule> validationRules)
        {
            var whereNotAlreadyExists = validationRules.Where(x => !_validationResult.ValidationRules.Any(y => y.Xpath == x.Xpath && y.Rule == x.Rule));
            _validationResult.ValidationRules.AddRange(whereNotAlreadyExists);
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

            _validationResult.ValidationRules.Add(new ValidationRule() { Rule = rule.ToString(), Xpath = xPath, XmlElement = xmlElementName?.ToString(), Id = elementRuleId });
        }
        protected void AddMessageFromRule(ValidationRuleEnum id, FieldNameEnum? xmlElementName = null, string[] messageParameters = null, string preConditionField = null)
        {
            var idSt = id.ToString();
            
            string xmlElementXpath = xmlElementName.HasValue ? $"/{xmlElementName}" : "/";

            var xpathNew =$"{xmlElementXpath}";

            var rule = RuleToValidate(idSt, xpathNew);

            var validationMessage = new ValidationMessage()
            {
                Rule = idSt,
                Reference = rule.Id,
                XpathField = xpathNew,
                PreCondition = preConditionField,
                MessageParameters = messageParameters
            };

            _validationResult.ValidationMessages.Add(validationMessage);
        }
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
        protected void AccumulateValidationMessages(List<ValidationMessage> validationMessages, int? index = null)
        {
            if (validationMessages != null && index.HasValue)
            {
                foreach (var message in validationMessages)
                {
                    var newXpath = Helpers.ReplaceCurlyBracketInXPath(index.Value, message.XpathField);
                    message.XpathField = newXpath;
                    _validationResult.ValidationMessages.AddRange(validationMessages);
                }
            }
            else
            {
                _validationResult.ValidationMessages.AddRange(validationMessages);
            }
        }

        private void FilterValidationMessagesOnTiltakstyper(string dataFormatVersion)
        {
            var tiltakstyper = GetFormTiltakstyper();
            if (tiltakstyper.Count() > 0)
            {
                var result = _checklistService.FilterValidationResult(dataFormatVersion, _validationResult.ValidationMessages, tiltakstyper);
                _validationResult.ValidationMessages = result.ToList();
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
    }
}
