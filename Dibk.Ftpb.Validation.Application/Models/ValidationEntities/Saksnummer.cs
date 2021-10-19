using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Saksnummer 
    {
        [XmlElement("saksaar")]
        public int? Saksaar { get; set; }
        [XmlElement("sakssekvensnummer")]
        public int? Sakssekvensnummer { get; set; }
    }
}