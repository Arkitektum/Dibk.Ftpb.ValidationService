using System.Xml.Serialization;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.Common
{
    [XmlRoot("ArbeidstilsynetsSamtykke"), XmlType("ArbeidstilsynetsSamtykke")]
    public class ArbeidstilsynetsSamtykke_45957_Common
    {
        [XmlArray("eiendomByggested", IsNullable = false)]
        [XmlArrayItem("eiendom")]
        public EiendomValidationEntity[] EiendomByggested { get; set; }
        
        [XmlElement("tiltakshaver")]
        public AktoerValidationEntity Tiltakshaver { get; set; }

        [XmlElement("arbeidsplasser")]
        public ArbeidsplasserValidationEntity ArbeidsplasserValidationEntity { get; set; }

        [XmlElement("fakturamottaker")]
        public FakturamottakerValidationEntity FakturamottakerValidationEntity { get; set; }

        [XmlElement("ansvarligSoeker")]
        public AktoerValidationEntity AnsvarligSoekerValidationEntity { get; set; }

        [XmlElement("betaling")]
        public BetalingValidationEntity BetalingValidationEntity { get; set; }

        [XmlArray("krav", IsNullable = false)]
        [XmlArrayItem("sjekklistekrav")]
        public SjekklistekravValidationEntity[] SjekklistekravValidationEntities { get; set; }

        [XmlElement("beskrivelseAvTiltak")]
        public BeskrivelseAvTiltakValidationEntity BeskrivelseAvTiltak { get; set; }

        [XmlElement("kommunensSaksnummer")]
        public SaksnummerValidationEntity KommunensSaksnummerValidationEntity { get; set; }

        [XmlElement("arbeidstilsynetsSaksnummer")]
        public SaksnummerValidationEntity ArbeidstilsynetsSaksnummerValidationEntity { get; set; }

        [XmlElement("metadata")]
        public MetadataValidationEntity MetadataValidationEntity { get; set; }

    }
}