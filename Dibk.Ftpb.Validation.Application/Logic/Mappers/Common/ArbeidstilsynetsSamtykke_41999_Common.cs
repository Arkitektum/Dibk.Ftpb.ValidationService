using System.Collections.Generic;
using System.Xml.Serialization;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.Common
{
    [XmlRoot("ArbeidstilsynetsSamtykke"), XmlType("ArbeidstilsynetsSamtykke")]
    public class ArbeidstilsynetsSamtykke_41999_Common
    {
        [XmlElement("tiltakshaver")]
        public AktoerValidationEntity Tiltakshaver { get; set; }
        [XmlElement("beskrivelseAvTiltak")]
        public BeskrivelseAvTiltakValidationEntity BeskrivelseAvTiltak { get; set; }
        
        [XmlArray("eiendomByggested", IsNullable = false)]
        [XmlArrayItem("eiendom")]
        public EiendomValidationEntity[] EiendomByggested { get; set; }

        [XmlElement("arbeidsplasser")]
        public ArbeidsplasserValidationEntity ArbeidsplasserValidationEntity { get; set; }


        //[XmlElement("fakturamottaker")]
        //public Fakturamottaker Fakturamottaker { get; set; }
    }
}