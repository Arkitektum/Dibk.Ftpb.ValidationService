using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class ArbeidstilsynetsSamtykke_41999_Mapper
    {
        public ArbeidstilsynetsSamtykke_41999_ValidationEntity GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var arbeidstilsynetsSamtykkeForm41999 = new ArbeidstilsynetsSamtykke_41999_Form();
            string XPathRoot = "";
            arbeidstilsynetsSamtykkeForm41999.TiltakshaverValidationEntity = new AktoerMapper().Map(dataModel.tiltakshaver);
            arbeidstilsynetsSamtykkeForm41999.AnsvarligSoekerValidationEntity = new AktoerMapper().Map(dataModel.ansvarligSoeker);
            arbeidstilsynetsSamtykkeForm41999.EiendomValidationEntities = new EiendomByggestedMapper().Map(dataModel.eiendomByggested);
            arbeidstilsynetsSamtykkeForm41999.ArbeidsplasserValidationEntity = new ArbeidsplasserMapper().Map(dataModel.arbeidsplasser);

            arbeidstilsynetsSamtykkeForm41999.FakturamottakerValidationEntity = new FakturamottakerMapper().Map(dataModel.fakturamottaker, XPathRoot);

            return new ArbeidstilsynetsSamtykke_41999_ValidationEntity(arbeidstilsynetsSamtykkeForm41999, XPathRoot, null);
        }
    }
}
