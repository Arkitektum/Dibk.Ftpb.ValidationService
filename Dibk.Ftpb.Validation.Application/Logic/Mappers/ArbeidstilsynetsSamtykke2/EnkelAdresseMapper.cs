using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class EnkelAdresseMapper
    {
        public static EnkelAdresseValidationEntity Map(EnkelAdresseType mapFrom)
        {
            EnkelAdresseValidationEntity enkelAdresse = null;
            if (mapFrom != null)
                enkelAdresse = new EnkelAdresseValidationEntity()
                {
                    Adresselinje1 = mapFrom.adresselinje1,
                    Adresselinje2 = mapFrom.adresselinje2,
                    Adresselinje3 = mapFrom.adresselinje3,
                    Landkode = mapFrom.landkode,
                    Postnr = mapFrom.postnr,
                    Poststed = mapFrom.poststed
                };

            return enkelAdresse;
        }
    }
}