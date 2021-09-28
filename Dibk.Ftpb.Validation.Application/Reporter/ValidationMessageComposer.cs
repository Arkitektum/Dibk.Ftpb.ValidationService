using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageComposer : IValidationMessageComposer
    {
        public ValidationMessageComposer()
        {
        }

        public ValidationResult ComposeValidationResult(string xPathRoot, string dataFormatVersion, ValidationResult validationResult, string languageCode)
        {
            ValidationMessageRepository repo = new ValidationMessageRepository();
            foreach (var validationMessage in validationResult.ValidationMessages)
            {
                var composedValidationMessage = repo.GetComposedValidationMessage(dataFormatVersion, validationMessage, languageCode);
                validationMessage.Message = composedValidationMessage.Message;
                validationMessage.ChecklistReference = composedValidationMessage.ChecklistReference;
                validationMessage.Messagetype = composedValidationMessage.Messagetype;
            }

            foreach (var validationRule in validationResult.ValidationRules)
            {
                var validationRuleFromRepo = repo.GetValidationRuleMessage(validationRule, languageCode, dataFormatVersion);
                validationRule.Message = validationRuleFromRepo.Message;
                //validationRule.ChecklistReference = validationRuleFromRepo.ChecklistReference;
                validationRule.Messagetype = validationRuleFromRepo.Messagetype;
            }

            foreach (var rule in validationResult.ValidationRules)
            {
                rule.Xpath = $"{xPathRoot}{rule.Xpath}";
                rule.Id = $"{dataFormatVersion}{rule.Id}";
            }
            foreach (var message in validationResult.ValidationMessages)
            {
                message.XpathField = $"{xPathRoot}{message.XpathField}";
                message.Reference = $"{dataFormatVersion}{message.Reference}";
            }

            return validationResult;
        }
    }
}
