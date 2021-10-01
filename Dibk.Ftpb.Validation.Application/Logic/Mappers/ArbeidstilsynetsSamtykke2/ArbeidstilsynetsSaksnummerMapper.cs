using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class ArbeidstilsynetsSaksnummerMapper
    {
        public SaksnummerValidationEntity Map(SaksnummerType mapFrom)
        {
            SaksnummerValidationEntity saksnummer = null;
            if (mapFrom != null)
                saksnummer = new SaksnummerValidationEntity()
                {
                     Saksaar = mapFrom.saksaar,
                     Sakssekvensnummer = mapFrom.sakssekvensnummer
                };

            return saksnummer;
        }
    }
}
