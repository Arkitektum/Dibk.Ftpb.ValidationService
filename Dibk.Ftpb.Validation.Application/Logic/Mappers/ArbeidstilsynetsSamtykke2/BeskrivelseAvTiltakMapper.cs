﻿using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class BeskrivelseAvTiltakMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.TiltakType, BeskrivelseAvTiltakValidationEntity>
    {

        public override BeskrivelseAvTiltakValidationEntity Map(TiltakType mapFrom, string parentElementXpath = null)
        {
            BeskrivelseAvTiltak beskrivelseAvTiltak = null;
            if (mapFrom != null)
            {
                beskrivelseAvTiltak = new BeskrivelseAvTiltak()
                {
                     BRA = mapFrom.BRA
                };

                beskrivelseAvTiltak.Formaaltype = new FormaaltypeMapper().Map(mapFrom.bruk, $"{parentElementXpath}/beskrivelseAvTiltak");
                beskrivelseAvTiltak.Tiltakstype = new TiltakstypeMapper().Map(mapFrom.type, $"{parentElementXpath}/beskrivelseAvTiltak");
            }

            return new BeskrivelseAvTiltakValidationEntity(beskrivelseAvTiltak, "beskrivelseAvTiltak", parentElementXpath);
        }


    }
}
