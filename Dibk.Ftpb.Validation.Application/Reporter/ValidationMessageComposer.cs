using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageComposer : IValidationMessageComposer
    {
        private readonly ValidationMessageRepository _repo;

        public ValidationMessageComposer()
        {
            _repo = new ValidationMessageRepository();
        }

        public ValidationResult ComposeValidationMessages(string xPathRoot, string dataFormatId, string dataFormatVersion, ValidationResult validationResult, string languageCode)
        {
            ValidationMessageRepository repo = new ValidationMessageRepository();

            //TODO get validationMessages form ValidationRule
            foreach (var validationMessage in validationResult.ValidationMessages)
            {
                var composedValidationMessage = repo.GetComposedValidationMessage(dataFormatId, dataFormatVersion, validationMessage, languageCode);
                validationMessage.Message = composedValidationMessage.Message;
                validationMessage.ChecklistReference = composedValidationMessage.ChecklistReference;
                validationMessage.Messagetype = composedValidationMessage.Messagetype;

                if (!validationMessage.XpathField.StartsWith(xPathRoot))
                    validationMessage.XpathField = $"{xPathRoot}{validationMessage.XpathField}";

                if (!validationMessage.Reference.StartsWith($"{dataFormatId}.{dataFormatVersion}"))
                    validationMessage.Reference = $"{dataFormatId}.{dataFormatVersion}{validationMessage.Reference}";
            }

            return validationResult;
        }

        public ValidationRule[] ComposeValidationRules(string xPathRoot, string dataFormatId, string dataFormatVersion, List<ValidationRule> validationRules, string languageCode)
        {
            var newValidationRules = new List<ValidationRule>();

            if (validationRules != null)
                foreach (var validationRule in validationRules)
                {
                    var validationRuleFromRepo = _repo.GetValidationRuleMessage(validationRule, languageCode, dataFormatId, dataFormatVersion);
                    validationRule.Message = validationRuleFromRepo.Message;
                    validationRule.Messagetype = validationRuleFromRepo.Messagetype;
                    validationRule.XpathField = $"{xPathRoot}{validationRule.XpathField}";
                    validationRule.Id = $"{dataFormatId}.{dataFormatVersion}{validationRule.Id}";
                    newValidationRules.Add(validationRule);
                }

            return newValidationRules.ToArray();
        }
    }
}
