using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.Models
{
    public class CodelistFormat
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public CodelistFormat(string name, string description, string status)
        {
            Name = name;
            Description = description;
            Status = status;
        }
    }
}
