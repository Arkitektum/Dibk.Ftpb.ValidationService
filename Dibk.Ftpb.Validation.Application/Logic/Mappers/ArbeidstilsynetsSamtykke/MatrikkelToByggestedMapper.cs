using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class MatrikkelToByggestedMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MatrikkelnummerType, MatrikkelValidationEntity>
    {
        public override MatrikkelValidationEntity Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MatrikkelnummerType matrikkelnummerType, string parentElementXpath = null)
        {
            Matrikkel matrikkel = null;
            if (matrikkelnummerType != null)
             matrikkel = new Matrikkel()
            {
                Bruksnummer = matrikkelnummerType.bruksnummer,
                Festenummer = matrikkelnummerType.festenummer,
                Gaardsnummer = matrikkelnummerType.gaardsnummer,
                Kommunenummer = matrikkelnummerType.kommunenummer,
                Seksjonsnummer = matrikkelnummerType.seksjonsnummer
            };

            return new MatrikkelValidationEntity(matrikkel, "eiendomsIdentifikasjon", parentElementXpath);
        }
    }
}
