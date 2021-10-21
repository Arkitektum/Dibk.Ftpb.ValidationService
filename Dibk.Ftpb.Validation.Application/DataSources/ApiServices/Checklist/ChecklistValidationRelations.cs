using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class ChecklistValidationRelations
    {
        public string ProcessCategory { get; set; }
        public string ChecklistReference { get; set; }
        public List<SupportingDataValidationRuleId> SupportingDataValidationRuleId { get; set; }
        public List<string> EnterpriseTerms { get; set; }
    }


    public class SupportingDataValidationRuleId
    {
        public List<string> ValidationRuleIds { get; set; }
    }
}
