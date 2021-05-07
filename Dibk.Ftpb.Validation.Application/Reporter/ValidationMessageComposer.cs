using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageComposer : IValidationMessageComposer
    {
        public ValidationMessageComposer()
        {
        }

        public List<ValidationRule> ComposeValidationReport(List<ValidationRule> validationRules, string languageCode)
        {
            ValidationMessageRepository repo = new ValidationMessageRepository();
            foreach (var valRule in validationRules)
            {
                valRule.Message = repo.GetValidationMessageStorageEntry(valRule, languageCode);
            }

            return validationRules;
        }
    }

    public interface IValidationMessageComposer
    {
        List<ValidationRule> ComposeValidationReport(List<ValidationRule> validationRules, string languageCode);
    }
}
