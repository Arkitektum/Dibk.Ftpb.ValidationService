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
                valMessage.Message = repo.GetValidationMessageStorageEntry(valMessage, languageCode);
            }

            return validationResult;
        }
    }

    public interface IValidationMessageComposer
    {
        ValidationResult ComposeValidationReport(ValidationResult validationResult, string languageCode);
    }
}
