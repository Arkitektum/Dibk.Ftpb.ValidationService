using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Logic
{
    [AttributeUsage(AttributeTargets.Class)]
    public class FormDataAttribute : Attribute
    {
        //public string DataFormatId { get; set; }
        public string DataFormatVersion { get; set; }

        //public string ServiceCode { get; set; }
    }
}
