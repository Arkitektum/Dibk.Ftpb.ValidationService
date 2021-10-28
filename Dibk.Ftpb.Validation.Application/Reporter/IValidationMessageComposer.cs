using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public interface IValidationMessageComposer
    {
        ValidationRule[] ComposeValidationRules(string xPathRoot, string dataFormatId,string dataFormatVersion, List<ValidationRule> validationRules, string languageCode);
    }
}
