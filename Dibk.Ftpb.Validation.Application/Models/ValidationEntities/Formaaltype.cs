using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class FormaaltypeValidationEntity 
    {
        [XmlElement("anleggstype")]
        public KodelisteValidationEntity Anleggstype { get; set; }
        [XmlElement("naeringsgruppe")]
        public KodelisteValidationEntity Naeringsgruppe { get; set; }
        [XmlElement("bygningstype")]
        public KodelisteValidationEntity Bygningstype { get; set; }
        [XmlElement("tiltaksformaal")]
        public KodelisteValidationEntity[] Tiltaksformaal { get; set; }
        [XmlElement("beskrivPlanlagtFormaal")]
        public string BeskrivPlanlagtFormaal {  get; set; }
    }
}
