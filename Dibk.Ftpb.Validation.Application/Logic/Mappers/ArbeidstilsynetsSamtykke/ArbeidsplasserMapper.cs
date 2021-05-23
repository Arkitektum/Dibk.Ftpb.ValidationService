using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class ArbeidsplasserMapper : ModelToValidationEntityMapper<ArbeidsplasserType, Arbeidsplasser>
    {
        public override Arbeidsplasser Map(ArbeidsplasserType mapFrom, ValidationEntityBase parentEntity = null)
        {
            if (mapFrom == null) return null;
            return new Arbeidsplasser("Arbeidsplasser", parentEntity)
            {
                AntallAnsatte = mapFrom.antallAnsatte,
                AntallVirksomheter = mapFrom.antallVirksomheter,
                Beskrivelse = mapFrom.beskrivelse,
                Eksisterende = mapFrom.eksisterende,
                Faste = mapFrom.faste,
                Framtidige = mapFrom.framtidige,
                Midlertidige = mapFrom.midlertidige,
                UtleieBygg = mapFrom.utleieBygg
            };
        }
    }
}