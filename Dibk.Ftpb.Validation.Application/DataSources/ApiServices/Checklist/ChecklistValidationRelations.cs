using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class ChecklistValidationRelations
    {
        public string ChecklistReference { get; set; }
        public string ProcessCategory { get; set; }
        public List<string> EnterpriseTerms { get; set; }
        public List<string> SupportingDataValidationRuleId { get; set; }
    }
}
