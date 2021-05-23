using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities
{
    public class ArbeidstilsynetsSamtykke2Form_45957 : ValidationEntityBase
    {
        public ArbeidstilsynetsSamtykke2Form_45957(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

        public IEnumerable<Eiendom> Eiendommer { get; set; }
        public Arbeidsplasser Arbeidsplasser { get; set; }
        public Aktoer Tiltakshaver { get; set; }
        public Fakturamottaker Fakturamottaker { get; set; }
    }
}