using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class ArbeidstilsynetsSamtykke2_45957_Mapper
    {
        public ArbeidstilsynetsSamtykke2_45957_Form GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var arbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykke2_45957_Form();
            string parentPath = "";
            arbeidstilsynetsSamtykke2Form45957.Metadata = new MetadataMapper().Map(dataModel.metadata);
            arbeidstilsynetsSamtykke2Form45957.Tiltakshaver = new AktoerMapper().Map(dataModel.tiltakshaver);
            arbeidstilsynetsSamtykke2Form45957.AnsvarligSoeker = new AktoerMapper().Map(dataModel.ansvarligSoeker);
            arbeidstilsynetsSamtykke2Form45957.EiendomByggested = new EiendomByggestedMapper().Map(dataModel.eiendomByggested);
            arbeidstilsynetsSamtykke2Form45957.Arbeidsplasser = new ArbeidsplasserMapper().Map(dataModel.arbeidsplasser);
            arbeidstilsynetsSamtykke2Form45957.Fakturamottaker = new FakturamottakerMapper().Map(dataModel.fakturamottaker);
            arbeidstilsynetsSamtykke2Form45957.BeskrivelseAvTiltak = new BeskrivelseAvTiltakMapper().Map(dataModel.beskrivelseAvTiltak);
            arbeidstilsynetsSamtykke2Form45957.ArbeidstilsynetsSaksnummer = new ArbeidstilsynetsSaksnummerMapper().Map(dataModel.arbeidstilsynetsSaksnummer);
            arbeidstilsynetsSamtykke2Form45957.Betaling = new BetalingMapper().Map(dataModel.betaling);
            arbeidstilsynetsSamtykke2Form45957.KommunensSaksnummer = new KommunensSaksnummerMapper().Map(dataModel.kommunensSaksnummer);
            arbeidstilsynetsSamtykke2Form45957.Sjekklistekrav = new SjekklistekravMapper().Map(dataModel.krav);

            return arbeidstilsynetsSamtykke2Form45957;
        }

    }
}
