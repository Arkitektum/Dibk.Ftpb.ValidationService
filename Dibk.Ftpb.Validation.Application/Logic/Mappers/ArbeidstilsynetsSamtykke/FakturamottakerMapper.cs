using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class FakturamottakerMapper
    {
        public FakturamottakerValidationEntity Map(FakturamottakerType mapFrom)
        {
            FakturamottakerValidationEntity fakturamottaker = null;
            if (mapFrom != null)
            {
                fakturamottaker = new FakturamottakerValidationEntity
                {
                    BestillerReferanse = mapFrom.bestillerReferanse,
                    EhfFaktura = mapFrom.ehfFaktura,
                    FakturaPapir = mapFrom.fakturaPapir,
                    Fakturareferanser = mapFrom.fakturareferanser,
                    Navn = mapFrom.navn,
                    Organisasjonsnummer = mapFrom.organisasjonsnummer,
                    Prosjektnummer = mapFrom.prosjektnummer,
                    Adresse = EnkelAdresseMapper.Map(mapFrom.adresse)
                };
            }

            return fakturamottaker;
        }
    }
}