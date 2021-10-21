namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public interface IValidationMessageComposer
    {
        ValidationResult ComposeValidationResult(string xPathRoot, string dataFormatVersion, ValidationResult validationResult, string languageCode);
    }
}
