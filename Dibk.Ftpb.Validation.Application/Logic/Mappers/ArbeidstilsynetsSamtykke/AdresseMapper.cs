using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class AdresseMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendommensAdresseType, EiendomsAdresse>
    {
        public override EiendomsAdresse Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendommensAdresseType mapFrom, ValidationEntityBase parentEntity = null)
        {
            if (mapFrom == null) return null;
            return new EiendomsAdresse("Adresse", parentEntity)
            {
                Adresselinje1 = mapFrom.adresselinje1,
                Adresselinje2 = mapFrom.adresselinje2,
                Adresselinje3 = mapFrom.adresselinje3,
                Bokstav = mapFrom.bokstav,
                Gatenavn = mapFrom.gatenavn,
                Husnr = mapFrom.husnr,
                Landkode = mapFrom.landkode,
                Postnr = mapFrom.postnr,
                Poststed = mapFrom.poststed
            };
        }
    }
}
