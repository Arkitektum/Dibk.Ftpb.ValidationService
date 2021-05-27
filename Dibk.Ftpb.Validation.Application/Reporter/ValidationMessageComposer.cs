using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageComposer : IValidationMessageComposer
    {
        public ValidationMessageComposer()
        {
        }

        public ValidationResult ComposeValidationReport(ValidationResult validationResult, string languageCode)
        {
            ValidationMessageRepository repo = new ValidationMessageRepository();
            foreach (var validationMessage in validationResult.ValidationMessages)
            {
                var composedValidationMessage = repo.GetComposedValidationMessage(validationMessage, languageCode);
                validationMessage.Message = composedValidationMessage.Message;
                validationMessage.ChecklistReference = composedValidationMessage.ChecklistReference;
                validationMessage.Messagetype = composedValidationMessage.Messagetype;
            }

            foreach (var validationRule in validationResult.ValidationRules)
            {
                var validationRuleFromRepo = repo.GetValidationRuleMessage(validationRule, languageCode);
                validationRule.Message = validationRuleFromRepo.Message;
                validationRule.ChecklistReference = validationRuleFromRepo.ChecklistReference;
                validationRule.Messagetype = validationRuleFromRepo.Messagetype;
            }

            return validationResult;
        }
    }

    public interface IValidationMessageComposer
    {
        ValidationResult ComposeValidationReport(ValidationResult validationResult, string languageCode);
    }
}
