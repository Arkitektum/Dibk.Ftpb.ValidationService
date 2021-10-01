using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class BetalingMapper
    {
        public BetalingValidationEntity Map(BetalingType mapFrom)
        {
            BetalingValidationEntity betaling = null;
            if (mapFrom != null)
                betaling = new BetalingValidationEntity()
                {
                    Beskrivelse = mapFrom.beskrivelse,
                    GebyrKategori = mapFrom.gebyrkategori,
                    OrdreId = mapFrom.ordreId,
                    SkalFaktureres = mapFrom.skalFaktureres,
                    Sum = mapFrom.sum,
                    TransId = mapFrom.transId
                };

            return betaling;
        }
    }
}
