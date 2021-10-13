using System;
using System.Xml.Serialization;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako
{
    [XmlRoot("ErklaeringAnsvarsrett"), XmlType("ErklaeringAnsvarsrett")]
    public class AnsvarsrettAnsako_ANSAKO_10000_Form
    {
        [XmlElement("ansvarligSoeker")]
        public Aktoer AnsvarligSoeker { get; set; }

        [XmlElement("ansvarsrett")]
        public Ansvarsrett Ansvarsretts { get; set; }
        
        [XmlElement("fraSluttbrukersystem")]
        public string FraSluttbrukersystem { get; set; }


        [XmlArray("eiendomByggested", IsNullable = false)]
        [XmlArrayItem("eiendom", IsNullable = true)]
        public Eiendom[] eiendomByggested { get; set; }

    }
}
