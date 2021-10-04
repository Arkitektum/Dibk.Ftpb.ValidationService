using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class EiendomsAdresseValidationEntity
    {
        [XmlElement("adresselinje1")]
        public string Adresselinje1 { get; set; }
        [XmlElement("adresselinje2")]
        public string Adresselinje2 { get; set; }
        [XmlElement("adresselinje3")]
        public string Adresselinje3 { get; set; }
        [XmlElement("postnr")]
        public string Postnr { get; set; }
        [XmlElement("poststed")]
        public string Poststed { get; set; }
        [XmlElement("landkode")]
        public string Landkode { get; set; }
        [XmlElement("gatenavn")]
        public string Gatenavn { get; set; }
        [XmlElement("husnr")]
        public string Husnr { get; set; }
        [XmlElement("bokstav")]
        public string Bokstav { get; set; }
    }
}