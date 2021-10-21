namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public interface IValidationMessageComposer
    {
        ValidationResult ComposeValidationResult(string xPathRoot, string dataFormatId,string dataFormatVersion, ValidationResult validationResult, string languageCode);
    }
}
