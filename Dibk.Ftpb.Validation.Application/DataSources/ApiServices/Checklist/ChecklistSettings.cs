using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class ChecklistSettings
    {
        public string BaseAddress { get; set; }
        public string ChecklistUrl { get; set; }
    }
    public interface IChecklistSettings
    {
        string BaseAddress { get; set; }
        string ChecklistUrl { get; set; }
    }
}
