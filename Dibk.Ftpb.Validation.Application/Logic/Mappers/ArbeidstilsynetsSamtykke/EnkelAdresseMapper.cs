using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class EnkelAdresseMapper : ModelToValidationEntityMapper<EnkelAdresseType, EnkelAdresseValidationEntity>
    {
        public override EnkelAdresseValidationEntity Map(EnkelAdresseType mapFrom, string parentElementXpath = null)
        {
            EnkelAdresse enkelAdresse = null;
            if (mapFrom != null)
                enkelAdresse = new EnkelAdresse()
                {
                    Adresselinje1 = mapFrom.adresselinje1,
                    Adresselinje2 = mapFrom.adresselinje2,
                    Adresselinje3 = mapFrom.adresselinje3,
                    Landkode = mapFrom.landkode,
                    Postnr = mapFrom.postnr,
                    Poststed = mapFrom.poststed
                };

            return new EnkelAdresseValidationEntity(enkelAdresse, "Adresse", parentElementXpath);
        }
    }
}