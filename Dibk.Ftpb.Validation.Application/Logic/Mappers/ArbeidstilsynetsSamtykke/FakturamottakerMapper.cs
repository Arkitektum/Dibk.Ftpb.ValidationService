using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class FakturamottakerMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke.FakturamottakerType, FakturamottakerValidationEntity>
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
                
                fakturamottaker.Adresse = EnkelAdresseMapper.Map(mapFrom.adresse);
            }

            return new FakturamottakerValidationEntity(fakturamottaker, "fakturamottaker", parentElementXpath);
        }
    }
}