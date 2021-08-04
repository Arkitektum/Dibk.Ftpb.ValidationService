using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ChecklistAnswer
    {
        public string ChecklistReference { get; set; }
        public string ChecklistQuestion { get; set; }
        public bool yesNo { get; set; }
        public string SupportingDataXpathField { get; set; }
        public string Documentation { get; set; }

    }
}
