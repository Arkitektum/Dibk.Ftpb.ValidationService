using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Metadata
    {
        [XmlElement("fraSluttbrukersystem")]
        public string FraSluttbrukersystem { get; set; }

        [XmlElement("ftbId")]
        public string FtbId { get; set; }

        [XmlElement("prosjektnavn")]
        public string Prosjektnavn { get; set; }
        
        [XmlElement("sluttbrukersystemUrl")]
        public string SluttbrukersystemUrl { get; set; }
        
        [XmlElement("hovedinnsendingsnummer")]
        public string Hovedinnsendingsnummer { get; set; }
        
        [XmlElement("erNorskSvenskDansk")]
        public bool? ErNorskSvenskDansk { get; set; }
        
        [XmlElement("klartForSigneringFraSluttbrukersystem")]
        public bool? KlartForSigneringFraSluttbrukersystem { get; set; }
        
        [XmlElement("unntattOffentlighet")]
        public bool? UnntattOffentlighet { get; set; }
    }
}
