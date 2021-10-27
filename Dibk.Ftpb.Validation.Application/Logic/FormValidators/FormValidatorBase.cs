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
using System.Xml.Serialization;

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
        protected string XPathRoot;
        protected abstract void InitializeValidatorConfig();
        protected abstract IEnumerable<string> GetFormTiltakstyper();
        protected abstract void InstantiateValidators();
        protected abstract void Validate(ValidationInput validationInput);
        protected abstract void DefineValidationRules();

        public ValidationRule[] FormValidationRules;

        public FormValidatorBase(IValidationMessageComposer validationMessageComposer, IChecklistService checklistService = null)
        {
            _validationMessageComposer = validationMessageComposer;
            _checklistService = checklistService;
            ValidationResult = new ValidationResult
            {
                ValidationMessages = new List<ValidationMessage>(),
                ValidationRules = new List<ValidationRule>()
            };
            ValidationResult = new ValidationResult
            {
                ValidationMessages = new List<ValidationMessage>(),
                ValidationRules = new List<ValidationRule>()
            };

        }

        protected virtual void InitializeFormValidator<T>()
        {

            //Get 'DataFormatId' and 'DataFormatVersion' info from validator
            var customAttributes = this.GetType().GetCustomAttributes(typeof(FormDataAttribute), true).FirstOrDefault() as FormDataAttribute;
            DataFormatId = customAttributes?.DataFormatId;
            DataFormatVersion = customAttributes?.DataFormatVersion;

            //GetRootXmlNode name
            var xmlRootElelement = typeof(T).GetCustomAttributes(typeof(XmlRootAttribute), true)?.SingleOrDefault() as XmlRootAttribute;
            XPathRoot = xmlRootElelement?.ElementName;

            InitializeValidatorConfig();
            InstantiateValidators();
            DefineValidationRules();

            FormValidationRules = _validationMessageComposer.ComposeValidationRules(XPathRoot, DataFormatId, DataFormatVersion, ValidationResult?.ValidationRules, "NO");

        }
        public virtual ValidationResult StartValidation(ValidationInput validationInput)
        {
            Validate(validationInput);
            ValidationResult = _validationMessageComposer.ComposeValidationMessages(XPathRoot, DataFormatId, DataFormatVersion, ValidationResult, "NO");

            if (!ValidationResult.ValidationMessages.Any(x => x.Messagetype.Equals(ValidationResultSeverityEnum.CRITICAL)))
            {
                FilterValidationMessagesOnTiltakstyper();
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
        protected void AddMessageFromRule(ValidationRuleEnum id, FieldNameEnum? fieldName = null, string[] messageParameters = null, string preConditionField = null)
        {
            var xpathNew = fieldName.HasValue ? $"/{fieldName}" : null;
            AddMessageFromRule(id, xpathNew, messageParameters, preConditionField);
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
            var formxPath = $"{XPathRoot}{xPath}";
            ValidationRule validationRule = ValidationResult.ValidationRules.Where(r => !string.IsNullOrEmpty(r.Rule))
                .FirstOrDefault(r => r.Rule.Equals(rule) && (r.XpathField == formxPath));
            if (validationRule == null)
            {
                validationRule = new ValidationRule()
                {
                    Rule = rule,
                    Message = $"Can't find rule:'{rule}'.-"
                };
            }
            return validationRule;
        }


    }

    public class FormDataAttribute : Attribute
    {
        public string DataFormatVersion { get; set; }
        public string DataFormatId { get; set; }
    }
}
