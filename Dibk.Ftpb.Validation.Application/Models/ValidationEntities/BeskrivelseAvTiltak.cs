using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class BeskrivelseAvTiltakValidationEntity 
    {
        [XmlElement("bruk")]
        public FormaaltypeValidationEntity Formaaltype { get; set; }
        [XmlElement("BRA")]
        public string BRA { get; set; }
        [XmlElement("type")]
        public Kodeliste[] Tiltakstype { get; set; }

        [XmlElement("foelgebrev", IsNullable = true)]
        public string Foelgebrev { get; set; }
    }
}
