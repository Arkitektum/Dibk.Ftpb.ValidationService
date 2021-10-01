using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class KodelisteValidationEntityMapper
    {
        public static KodelisteValidationEntity Map(KodeType mapFrom)
        {
            KodelisteValidationEntity kodeliste = null;
            if (mapFrom != null)
                kodeliste = new KodelisteValidationEntity()
                {
                    Kodebeskrivelse = mapFrom.kodebeskrivelse,
                    Kodeverdi = mapFrom.kodeverdi
                };

            return kodeliste;
        }
    }
}
