using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumerationAttribute : Attribute
    {
        public string XmlNode { get; set; }
        public string ValidatorId { get; set; }
    }

    public class SjekklistekravEnumerationAttribute : Attribute
    {
        public string SjekklistepunktVerdi { get; set; }
    }



}
