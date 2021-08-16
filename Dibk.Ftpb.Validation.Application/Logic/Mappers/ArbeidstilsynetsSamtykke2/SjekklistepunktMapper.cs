using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class SjekklistepunktMapper : ModelToValidationEntityMapper<KodeType, KodelisteValidationEntity>
    {
        //public override SjekklistepunktValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
        //{
        //    Kodeliste sjekklistepunkt = null;
        //    if (mapFrom != null)
        //        sjekklistepunkt = new Kodeliste()
        //        {
        //            Kodeverdi = mapFrom.kodeverdi,
        //            Kodebeskrivelse = mapFrom.kodebeskrivelse
        //        };

        //    return new SjekklistepunktValidationEntity(sjekklistepunkt, "sjekklistepunkt", parentElementXpath);
        //}

        public override KodelisteValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
        {
            Kodeliste sjekklistepunkt = null;
            if (mapFrom != null)
                sjekklistepunkt = new Kodeliste()
                {
                    Kodebeskrivelse = mapFrom.kodebeskrivelse,
                    Kodeverdi = mapFrom.kodeverdi
                };

            return new KodelisteValidationEntity(sjekklistepunkt, "sjekklistepunkt", parentElementXpath);
        }
    }
}