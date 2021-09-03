using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class ArbeidstilsynetsSaksnummerMapper : ModelToValidationEntityMapper<SaksnummerType, SaksnummerValidationEntity>
    {
        public override SaksnummerValidationEntity Map(SaksnummerType mapFrom, string parentElementXpath = null)
        {
            Saksnummer saksnummer = null;
            if (mapFrom != null)
                saksnummer = new Saksnummer()
                {
                     Saksaar = mapFrom.saksaar,
                     Sakssekvensnummer = mapFrom.sakssekvensnummer
                };

            return new SaksnummerValidationEntity(saksnummer, "arbeidstilsynetsSaksnummer", parentElementXpath);
        }
    }
}
