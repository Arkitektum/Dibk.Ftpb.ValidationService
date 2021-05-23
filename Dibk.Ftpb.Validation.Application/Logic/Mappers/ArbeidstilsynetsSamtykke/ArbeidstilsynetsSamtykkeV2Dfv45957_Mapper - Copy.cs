using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper
    {
        public ArbeidstilsynetsSamtykke2Form_45957 GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var arbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykke2Form_45957("ArbeidstilsynetsSamtykke");

            arbeidstilsynetsSamtykke2Form45957.Eiendommer = new EiendomToByggestedMapper().Map(dataModel.eiendomByggested, arbeidstilsynetsSamtykke2Form45957);
            arbeidstilsynetsSamtykke2Form45957.Arbeidsplasser = new ArbeidsplasserMapper().Map(dataModel.arbeidsplasser, arbeidstilsynetsSamtykke2Form45957);
            arbeidstilsynetsSamtykke2Form45957.Tiltakshaver = new TiltakshaverMapper().Map(dataModel.tiltakshaver, arbeidstilsynetsSamtykke2Form45957);
            arbeidstilsynetsSamtykke2Form45957.Fakturamottaker = new FakturamottakerMapper().Map(dataModel.fakturamottaker, arbeidstilsynetsSamtykke2Form45957);

            return arbeidstilsynetsSamtykke2Form45957;
        }
    }
}
