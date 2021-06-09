using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class ArbeidstilsynetsSamtykke2_45957_Mapper
    {
        public ArbeidstilsynetsSamtykke2_45957_ValidationEntity GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel, string XPathRoot)
        {
            var arbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykke2_45957_Form();

            arbeidstilsynetsSamtykke2Form45957.EiendomValidationEntities = new EiendomByggestedMapper().Map(dataModel.eiendomByggested, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.ArbeidsplasserValidationEntity = new ArbeidsplasserMapper().Map(dataModel.arbeidsplasser, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.TiltakshaverValidationEntity = new TiltakshaverMapper().Map(dataModel.tiltakshaver, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.FakturamottakerValidationEntity = new FakturamottakerMapper().Map(dataModel.fakturamottaker, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.SjekklistekravValidationEntities = new SjekklistekravMapper().Map(dataModel.krav, XPathRoot);

            return new ArbeidstilsynetsSamtykke2_45957_ValidationEntity(arbeidstilsynetsSamtykke2Form45957, XPathRoot, null);
        }
    }
}
