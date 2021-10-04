using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class KontaktpersonValidationEntity
    {
        [XmlElement("navn")]
        public string Navn { get; set; }
        [XmlElement("telefonnummer")]
        public string Telefonnummer { get; set; }
        [XmlElement("mobilnummer")]
        public string Mobilnummer { get; set; }
        [XmlElement("epost")]
        public string Epost { get; set; }
    }
}
