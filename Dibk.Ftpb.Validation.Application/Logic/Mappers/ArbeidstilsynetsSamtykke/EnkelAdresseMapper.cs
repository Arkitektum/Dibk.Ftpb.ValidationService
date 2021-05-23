using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class EnkelAdresseMapper : ModelToValidationEntityMapper<EnkelAdresseType, EnkelAdresse>
    {
        public override EnkelAdresse Map(EnkelAdresseType mapFrom, ValidationEntityBase parentEntity = null)
        {
            if (mapFrom == null) return null;
            return new EnkelAdresse("Adresse", parentEntity)
            {
                Adresselinje1 = mapFrom.adresselinje1,
                Adresselinje2 = mapFrom.adresselinje2,
                Adresselinje3 = mapFrom.adresselinje3,
                Landkode = mapFrom.landkode,
                Postnr = mapFrom.postnr,
                Poststed = mapFrom.poststed
            };
        }
    }
}