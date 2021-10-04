using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class ArbeidsplasserValidationEntity 
    {
        [XmlElement("framtidige")]
        public bool? Framtidige { get; set; }
        [XmlElement("faste")]
        public bool? Faste { get; set; }
        [XmlElement("midlertidige")]
        public bool? Midlertidige { get; set; }
        [XmlElement("antallAnsatte")]
        public string AntallAnsatte { get; set; }
        [XmlElement("eksisterende")]
        public bool? Eksisterende { get; set; }
        [XmlElement("utleieBygg")]
        public bool? UtleieBygg { get; set; }
        [XmlElement("antallVirksomheter")]
        public string AntallVirksomheter { get; set; }
        [XmlElement("beskrivelse")]
        public string Beskrivelse { get; set; }
        [XmlElement("veiledning")]
        public bool? Veiledning { get; set; }

    }
}