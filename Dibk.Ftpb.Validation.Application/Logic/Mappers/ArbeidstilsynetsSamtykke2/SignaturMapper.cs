using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class SignaturMapper
    {
        public SignaturValidationEntity Map(SignaturType mapFrom)
        {
            SignaturValidationEntity signatur = null;
            if (mapFrom != null)
                signatur = new SignaturValidationEntity()
                {
                     Signaturdato = mapFrom.signaturdato,
                     SignertAv = mapFrom.signertAv,
                     SignertPaaVegneAv = mapFrom.signertPaaVegneAv
                };

            return signatur;
        }
    }
}
