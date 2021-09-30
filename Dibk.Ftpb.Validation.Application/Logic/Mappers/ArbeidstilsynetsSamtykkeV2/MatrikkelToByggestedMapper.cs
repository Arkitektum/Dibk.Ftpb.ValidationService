using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class MatrikkelToByggestedMapper
    {
        public static MatrikkelValidationEntity Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MatrikkelnummerType matrikkelnummerType, string parentElementXpath = null)
        {
            MatrikkelValidationEntity matrikkel = null;
            if (matrikkelnummerType != null)
             matrikkel = new MatrikkelValidationEntity()
            {
                Bruksnummer = matrikkelnummerType.bruksnummer,
                Festenummer = matrikkelnummerType.festenummer,
                Gaardsnummer = matrikkelnummerType.gaardsnummer,
                Kommunenummer = matrikkelnummerType.kommunenummer,
                Seksjonsnummer = matrikkelnummerType.seksjonsnummer
            };

            return matrikkel;
        }
    }
}
