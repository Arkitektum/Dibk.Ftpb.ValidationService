using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Eiendom 
    {
        [XmlElement("adresse")]
        public EiendomsAdresseValidationEntity Adresse { get; set; }
        [XmlElement("eiendomsidentifikasjon")]
        public MatrikkelValidationEntity Matrikkel { get; set; }
        [XmlElement("bygningsnummer")]
        public string Bygningsnummer { get; set; }
        [XmlElement("bolignummer")]
        public string Bolignummer { get; set; }
        [XmlElement("kommunenavn")]
        public string Kommunenavn { get; set; }
    }
}
