using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class FakturamottakerMapper
    {
        public FakturamottakerValidationEntity Map(FakturamottakerType mapFrom)
        {
            FakturamottakerValidationEntity fakturamottaker = null;
            if (mapFrom != null)
            {
                fakturamottaker = new FakturamottakerValidationEntity()
                {
                    BestillerReferanse = mapFrom.bestillerReferanse,
                    EhfFaktura = mapFrom.ehfFaktura,
                    FakturaPapir = mapFrom.fakturaPapir,
                    Fakturareferanser = mapFrom.fakturareferanser,
                    Navn = mapFrom.navn,
                    Organisasjonsnummer = mapFrom.organisasjonsnummer,
                    Prosjektnummer = mapFrom.prosjektnummer
                };
                
                fakturamottaker.Adresse = EnkelAdresseMapper.Map(mapFrom.adresse);
            }

            return fakturamottaker;
        }
    }
}