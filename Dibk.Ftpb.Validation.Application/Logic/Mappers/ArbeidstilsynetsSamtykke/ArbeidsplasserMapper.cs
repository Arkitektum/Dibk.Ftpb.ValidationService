﻿using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class ArbeidsplasserMapper : ModelToValidationEntityMapper<ArbeidsplasserType, ArbeidsplasserValidationEntity>
    {
        public override ArbeidsplasserValidationEntity Map(ArbeidsplasserType mapFrom, string parentElementXpath = null)
        {
            Arbeidsplasser arbeidsplasser = null;
            if (mapFrom != null)
            arbeidsplasser =  new Arbeidsplasser()
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

            return new ArbeidsplasserValidationEntity(arbeidsplasser, "arbeidsplasser", parentElementXpath);
        }
    }
}