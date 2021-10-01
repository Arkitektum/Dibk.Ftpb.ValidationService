using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class SjekklistepunktMapper 
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

        public  KodelisteValidationEntity Map(KodeType mapFrom)
        {
            KodelisteValidationEntity sjekklistepunkt = null;
            if (mapFrom != null)
                sjekklistepunkt = new KodelisteValidationEntity()
                {
                    Kodebeskrivelse = mapFrom.kodebeskrivelse,
                    Kodeverdi = mapFrom.kodeverdi
                };

            return sjekklistepunkt;
        }
    }
}