using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class EiendomAdresseMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendommensAdresseType, EiendomsAdresseValidationEntity>
    {
        public override EiendomsAdresseValidationEntity Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendommensAdresseType mapFrom, string parentElementXpath = null)
        {
            EiendomsAdresse addresse = null;
            if (mapFrom != null)
                addresse = new EiendomsAdresse()
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

            return new EiendomsAdresseValidationEntity(addresse, "adresse", parentElementXpath);
        }
    }
}
