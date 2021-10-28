using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageComposer : IValidationMessageComposer
    {
        private readonly ValidationMessageRepository _repo;

        public ValidationMessageComposer()
        {
            _repo = new ValidationMessageRepository();
        }

        public ValidationRule[] ComposeValidationRules(string xPathRoot, string dataFormatId, string dataFormatVersion, List<ValidationRule> validationRules, string languageCode)
        {
            var newValidationRules = new List<ValidationRule>();

            if (validationRules != null)
                foreach (var validationRule in validationRules)
                {
                    var validationRuleFromRepo = _repo.GetValidationRuleMessage(validationRule, languageCode, dataFormatId, dataFormatVersion);
                    validationRule.Message = validationRuleFromRepo.Message;
                    validationRule.Messagetype = validationRuleFromRepo.Messagetype;
                    validationRule.XpathField = $"{xPathRoot}{validationRule.XpathField}";
                    validationRule.Id = $"{dataFormatId}.{dataFormatVersion}{validationRule.Id}";
                    validationRule.ChecklistReference = validationRuleFromRepo.ChecklistReference;
                    newValidationRules.Add(validationRule);
                }

            return newValidationRules.ToArray();
        }
    }
}
