using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Models.Standard;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class RuleDocumentationComposer
    {

        public static List<RuleDocumentationModel> GetRuleDocumentationModel(List<ValidationRule> validationRules)
        {
            var formRules = new List<RuleDocumentationModel>();

            if (validationRules != null)
            {

                foreach (var rule in validationRules)
                {
                    formRules.Add(new RuleDocumentationModel()
                    {
                        RuleId = rule.Id,
                        CheckListPt = rule.ChecklistReference,
                        Description = rule.Message,
                        XpathCondition = rule.XpathField,
                        XpathPrecondition = rule.PreCondition,
                        RuleType = NorskStandardValidator.NorskFeilmeldingType(rule.Messagetype)
                    });
                }
            }
            return formRules;
        }
    }
}
