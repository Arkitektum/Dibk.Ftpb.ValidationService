using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Matrikkel
    {
        [XmlElement("kommunenummer")]
        public string Kommunenummer { get; set; }
        [XmlElement("gaardsnummer")]
        public string Gaardsnummer { get; set; }
        [XmlElement("bruksnummer")]
        public string Bruksnummer { get; set; }
        [XmlElement("festenummer")]
        public string Festenummer { get; set; }
        [XmlElement("seksjonsnummer")]
        public string Seksjonsnummer { get; set; }
    }
}