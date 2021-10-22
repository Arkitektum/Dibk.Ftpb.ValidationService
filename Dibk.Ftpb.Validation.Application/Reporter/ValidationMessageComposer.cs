namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageComposer : IValidationMessageComposer
    {
        public ValidationMessageComposer()
        {
        }

        public ValidationResult ComposeValidationResult(string xPathRoot, string dataFormatId, string dataFormatVersion, ValidationResult validationResult, string languageCode)
        {
            ValidationMessageRepository repo = new ValidationMessageRepository();
            foreach (var validationRule in validationResult.ValidationRules)
            {
                var validationRuleFromRepo = repo.GetValidationRuleMessage(validationRule, languageCode, dataFormatId, dataFormatVersion);
                validationRule.Message = validationRuleFromRepo.Message;
                //validationRule.ChecklistReference = validationRuleFromRepo.ChecklistReference;
                validationRule.Messagetype = validationRuleFromRepo.Messagetype;
            }

            //TODO get validationMessages form ValidationRule
            foreach (var validationMessage in validationResult.ValidationMessages)
            {
                var composedValidationMessage = repo.GetComposedValidationMessage(dataFormatId, dataFormatVersion, validationMessage, languageCode);
                validationMessage.Message = composedValidationMessage.Message;
                validationMessage.ChecklistReference = composedValidationMessage.ChecklistReference;
                validationMessage.Messagetype = composedValidationMessage.Messagetype;
            }

            

            foreach (var rule in validationResult.ValidationRules)
            {
                rule.XpathField = $"{xPathRoot}{rule.XpathField}";
                rule.Id = $"{dataFormatId}.{dataFormatVersion}{rule.Id}";
            }
            foreach (var message in validationResult.ValidationMessages)
            {
                message.XpathField = $"{xPathRoot}{message.XpathField}";
                message.Reference = $"{dataFormatId}.{dataFormatVersion}{message.Reference}";
            }

            return validationResult;
        }
    }
}
