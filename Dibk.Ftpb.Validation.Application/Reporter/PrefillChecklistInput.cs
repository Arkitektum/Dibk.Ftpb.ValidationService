using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class PrefillChecklistInput
    {
        //public string ProcessCategory { get; set; }
        public string DataFormatVersion { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public IEnumerable<string> Warnings { get; set; }
        public IEnumerable<string> ExecutedValidations { get; set; }


    }
}
