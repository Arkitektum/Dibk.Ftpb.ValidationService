using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities
{
    [XmlRoot("ArbeidstilsynetsSamtykke"), XmlType("ArbeidstilsynetsSamtykke")]
    public class ArbeidstilsynetsSamtykke2_45957_Form
    {
        [XmlArray("eiendomByggested", IsNullable = false)]
        [XmlArrayItem("eiendom")]
        public Eiendom[] EiendomByggested { get; set; }

        [XmlElement("tiltakshaver")]
        public Aktoer Tiltakshaver { get; set; }

        [XmlElement("arbeidsplasser")]
        public ArbeidsplasserValidationEntity Arbeidsplasser { get; set; }

        [XmlElement("fakturamottaker")]
        public FakturamottakerValidationEntity Fakturamottaker { get; set; }

        [XmlElement("ansvarligSoeker")]
        public Aktoer AnsvarligSoeker { get; set; }

        [XmlElement("betaling")]
        public BetalingValidationEntity Betaling
        { get; set; }

        [XmlArray("krav", IsNullable = false)]
        [XmlArrayItem("sjekklistekrav")]
        public SjekklistekravValidationEntity[] Sjekklistekrav { get; set; }

        [XmlElement("beskrivelseAvTiltak")]
        public BeskrivelseAvTiltakValidationEntity BeskrivelseAvTiltak { get; set; }

        [XmlElement("kommunensSaksnummer")]
        public Saksnummer KommunensSaksnummer { get; set; }

        [XmlElement("arbeidstilsynetsSaksnummer")]
        public Saksnummer ArbeidstilsynetsSaksnummer { get; set; }

        [XmlElement("metadata")]
        public Metadata Metadata { get; set; }
    }
}