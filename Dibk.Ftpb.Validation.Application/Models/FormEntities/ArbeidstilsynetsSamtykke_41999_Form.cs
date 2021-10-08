using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities
{
    [XmlRoot("ArbeidstilsynetsSamtykke"), XmlType("ArbeidstilsynetsSamtykke")]
    public class ArbeidstilsynetsSamtykke_41999_Form
    {
        [XmlArray("eiendomByggested", IsNullable = false)]
        [XmlArrayItem("eiendom")]
        public EiendomValidationEntity[] EiendomByggested { get; set; }
       
        [XmlElement("tiltakshaver")]
        public Aktoer Tiltakshaver { get; set; }
        
        [XmlElement("arbeidsplasser")]
        public ArbeidsplasserValidationEntity ArbeidsplasserValidationEntity { get; set; }

        [XmlElement("fakturamottaker")]
        public FakturamottakerValidationEntity FakturamottakerValidationEntity { get; set; }

        [XmlElement("ansvarligSoeker")]
        public Aktoer AnsvarligSoeker { get; set; }

    }
}
