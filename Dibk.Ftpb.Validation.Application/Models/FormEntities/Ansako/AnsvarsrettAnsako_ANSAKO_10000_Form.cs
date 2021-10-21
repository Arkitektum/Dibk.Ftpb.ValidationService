using System.Xml.Serialization;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako
{
    [XmlRoot("ErklaeringAnsvarsrett"), XmlType("ErklaeringAnsvarsrett")]
    public class AnsvarsrettAnsako_ANSAKO_10000_Form
    {
        [XmlElement("ansvarligSoeker")]
        public AktoerV2 AnsvarligSoeker { get; set; }

        [XmlElement("ansvarsrett")]
        public Ansvarsrett Ansvarsretts { get; set; }

        [XmlElement("fraSluttbrukersystem")]
        public string FraSluttbrukersystem { get; set; }

        [XmlElement("prosjektnavn")]
        public string Prosjektnavn { get; set; }

        [XmlElement("prosjektnr")]
        public string Prosjektnr { get; set; }

        [XmlElement("kommunensSaksnummer")]
        public Saksnummer KommunensSaksnummer { get; set; }

        [XmlArray("eiendomByggested", IsNullable = false)]
        [XmlArrayItem("eiendom", IsNullable = true)]
        public Eiendom[] eiendomByggested { get; set; }

    }
}
