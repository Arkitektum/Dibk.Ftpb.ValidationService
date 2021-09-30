using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class EiendomAdresseMapper
    {
        public static EiendomsAdresseValidationEntity Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendommensAdresseType mapFrom, string parentElementXpath = null)
        {
            EiendomsAdresseValidationEntity eiendomsAdresse = null;
            if (mapFrom != null)
                eiendomsAdresse = new EiendomsAdresseValidationEntity()
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

            return eiendomsAdresse;
        }
    }
}
