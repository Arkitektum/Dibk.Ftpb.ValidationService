using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class MatrikkelToByggestedMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MatrikkelnummerType, Matrikkel>
    {
        public override Matrikkel Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MatrikkelnummerType matrikkelnummerType, ValidationEntityBase parentEntity = null)
        {
            if (matrikkelnummerType == null) return null;
            var matrikkel = new Matrikkel("EiendomsIdentifikasjon", parentEntity)
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
