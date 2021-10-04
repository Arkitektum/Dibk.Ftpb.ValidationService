using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.Common
{
    
    [XmlRoot("ErklaeringAnsvarsrett"), XmlType("ErklaeringAnsvarsrett")]
    public class AnsvarsrettAnsako_ANSAKO_10000_Common
    {
        [XmlElement("tiltakshaver")]
        public AktoerValidationEntity Tiltakshaver { get; set; }
    }
}
