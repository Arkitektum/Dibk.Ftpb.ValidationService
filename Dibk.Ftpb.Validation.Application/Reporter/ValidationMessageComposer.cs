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
            foreach (var valMessage in validationResult.ValidationMessages)
            {
                var canReplacParameters = repo.GetValidationMessageStorageEntry(valMessage, languageCode, out string composedValidationMessage);
                //if (canReplacParameters)
                valMessage.Message = composedValidationMessage;
            }

            foreach (var valRule in validationResult.ValidationRules)
            {
                var canReplacParameters = repo.GetValidationRuleMessage(valRule, languageCode, out string validationRuleMessage);
                //if (canReplacParameters)
                valRule.Message = validationRuleMessage;
            }


            return validationResult;
        }
    }

    public interface IValidationMessageComposer
    {
        ValidationResult ComposeValidationReport(ValidationResult validationResult, string languageCode);
    }
}
