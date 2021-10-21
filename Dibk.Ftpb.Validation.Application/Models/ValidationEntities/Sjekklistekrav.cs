using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class SjekklistekravValidationEntity
    {
        [XmlElement("sjekklistepunktsvar")]
        public bool? Sjekklistepunktsvar { get; set; }
        
        [XmlElement("sjekklistepunkt")]
        public Kodeliste Sjekklistepunkt { get; set; }

        [XmlElement("dokumentasjon")]
        public string Dokumentasjon { get; set; }
        
    }
}
