using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class BetalingMapper : ModelToValidationEntityMapper<BetalingType, BetalingValidationEntity>
    {
        public override BetalingValidationEntity Map(BetalingType mapFrom, string parentElementXpath = null)
        {
            Betaling betaling = null;
            if (mapFrom != null)
                betaling = new Betaling()
                {
                    Beskrivelse = mapFrom.beskrivelse,
                    GebyrKategori = mapFrom.gebyrkategori,
                    OrdreId = mapFrom.ordreId,
                    SkalFaktureres = mapFrom.skalFaktureres,
                    Sum = mapFrom.sum,
                    TransId = mapFrom.transId
                };

            return new BetalingValidationEntity(betaling, "betaling", parentElementXpath);
        }
    }
}
