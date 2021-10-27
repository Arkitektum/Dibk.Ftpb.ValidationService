using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public interface IValidationMessageComposer
    {
        ValidationResult ComposeValidationMessages(string xPathRoot, string dataFormatId,string dataFormatVersion, ValidationResult validationResult, string languageCode);
        ValidationRule[] ComposeValidationRules(string xPathRoot, string dataFormatId,string dataFormatVersion, List<ValidationRule> validationRules, string languageCode);
    }
}
