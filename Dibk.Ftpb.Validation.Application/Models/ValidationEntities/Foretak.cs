using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Foretak : Aktoer
    {
        [XmlElement("harSentralGodkjenning")]
        public bool? HarSentralGodkjenning { get; set; }
    }
}
