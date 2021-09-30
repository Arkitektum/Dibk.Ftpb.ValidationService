using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class BeskrivelseAvTiltakMapper
    {

        public BeskrivelseAvTiltakValidationEntity Map(TiltakType mapFrom, string parentElementXpath = null)
        {
            BeskrivelseAvTiltakValidationEntity beskrivelseAvTiltak = null;
            if (mapFrom != null)
            {
                beskrivelseAvTiltak = new BeskrivelseAvTiltakValidationEntity()
                {
                     BRA = mapFrom.BRA
                };

                beskrivelseAvTiltak.Formaaltype = FormaaltypeMapper.Map(mapFrom.bruk);
                beskrivelseAvTiltak.Tiltakstype = KodelisteValidationEntitiesMapper.Map(mapFrom.type);
            }

            return beskrivelseAvTiltak;
        }


    }
}
