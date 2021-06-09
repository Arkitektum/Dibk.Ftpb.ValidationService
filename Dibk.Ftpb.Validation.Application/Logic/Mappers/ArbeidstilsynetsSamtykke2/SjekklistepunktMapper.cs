using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class SjekklistepunktMapper : ModelToValidationEntityMapper<KodeType, SjekklistepunktValidationEntity>
    {
        public override SjekklistepunktValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
        {
            Sjekklistepunkt sjekklistepunkt = null;
            if (mapFrom != null)
                sjekklistepunkt = new Sjekklistepunkt()
                {
                    Kodeverdi = mapFrom.kodeverdi,
                    Kodebeskrivelse = mapFrom.kodebeskrivelse
                };

            return new SjekklistepunktValidationEntity(sjekklistepunkt, "sjekklistepunkt", parentElementXpath);
        }
    }
}