using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class FakturamottakerMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.FakturamottakerType, Fakturamottaker>
    {
        public override Fakturamottaker Map(FakturamottakerType mapFrom, ValidationEntityBase parentEntity = null)
        {
            if (mapFrom == null) return null;
            var fakturaMottaker = new Fakturamottaker("Fakturamottaker", parentEntity)
            {
                BestillerReferanse = mapFrom.bestillerReferanse,
                EhfFaktura = mapFrom.ehfFaktura,
                FakturaPapir = mapFrom.fakturaPapir,
                Fakturareferanser = mapFrom.fakturareferanser,
                Navn = mapFrom.navn,
                Organisasjonsnummer = mapFrom.organisasjonsnummer,
                Prosjektnummer = mapFrom.prosjektnummer
            };
            fakturaMottaker.Adresse = new EnkelAdresseMapper().Map(mapFrom.adresse, parentEntity);

            return fakturaMottaker;
        }
    }
}