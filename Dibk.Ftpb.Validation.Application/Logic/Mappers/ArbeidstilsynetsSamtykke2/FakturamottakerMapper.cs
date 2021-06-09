using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class FakturamottakerMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.FakturamottakerType, FakturamottakerValidationEntity>
    {
        public override FakturamottakerValidationEntity Map(FakturamottakerType mapFrom, string parentElementXpath = null)
        {
            Fakturamottaker fakturamottaker = null;
            if (mapFrom != null)
            {
                fakturamottaker = new Fakturamottaker()
                {
                    BestillerReferanse = mapFrom.bestillerReferanse,
                    EhfFaktura = mapFrom.ehfFaktura,
                    FakturaPapir = mapFrom.fakturaPapir,
                    Fakturareferanser = mapFrom.fakturareferanser,
                    Navn = mapFrom.navn,
                    Organisasjonsnummer = mapFrom.organisasjonsnummer,
                    Prosjektnummer = mapFrom.prosjektnummer
                };
                
                fakturamottaker.Adresse = new EnkelAdresseMapper().Map(mapFrom.adresse, $"{parentElementXpath}/fakturamottaker");
            }

            return new FakturamottakerValidationEntity(fakturamottaker, "fakturamottaker", parentElementXpath);
        }
    }
}