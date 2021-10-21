using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class FormaaltypeValidationEntity 
    {
        [XmlElement("anleggstype")]
        public Kodeliste Anleggstype { get; set; }
        [XmlElement("naeringsgruppe")]
        public Kodeliste Naeringsgruppe { get; set; }
        [XmlElement("bygningstype")]
        public Kodeliste Bygningstype { get; set; }
        [XmlElement("tiltaksformaal")]
        public Kodeliste[] Tiltaksformaal { get; set; }
        [XmlElement("beskrivPlanlagtFormaal")]
        public string BeskrivPlanlagtFormaal {  get; set; }
    }
}
