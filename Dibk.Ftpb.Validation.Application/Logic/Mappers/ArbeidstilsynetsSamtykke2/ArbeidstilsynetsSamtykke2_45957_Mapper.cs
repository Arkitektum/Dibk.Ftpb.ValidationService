using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class ArbeidstilsynetsSamtykke2_45957_Mapper
    {
        public ArbeidstilsynetsSamtykke2_45957_ValidationEntity GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var arbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykke2_45957_Form();
            string XPathRoot = "";
            arbeidstilsynetsSamtykke2Form45957.EiendomValidationEntities = new EiendomByggestedMapper().Map(dataModel.eiendomByggested, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.ArbeidsplasserValidationEntity = new ArbeidsplasserMapper().Map(dataModel.arbeidsplasser, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.TiltakshaverValidationEntity = new AktoerMapper(AktoerEnum.tiltakshaver).Map(dataModel.tiltakshaver, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.AnsvarligSoekerValidationEntity = new AktoerMapper(AktoerEnum.ansvarligSoeker).Map(dataModel.ansvarligSoeker, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.FakturamottakerValidationEntity = new FakturamottakerMapper().Map(dataModel.fakturamottaker, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.SjekklistekravValidationEntities = new SjekklistekravMapper().Map(dataModel.krav, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.BeskrivelseAvTiltakValidationEntity = new BeskrivelseAvTiltakMapper().Map(dataModel.beskrivelseAvTiltak, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.MetadataValidationEntity = new MetadataMapper().Map(dataModel.metadata, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.ArbeidstilsynetsSaksnummerValidationEntity = new ArbeidstilsynetsSaksnummerMapper().Map(dataModel.arbeidstilsynetsSaksnummer, XPathRoot);
            arbeidstilsynetsSamtykke2Form45957.KommunensSaksnummerValidationEntity = new KommunensSaksnummerMapper().Map(dataModel.kommunensSaksnummer, XPathRoot);

            return new ArbeidstilsynetsSamtykke2_45957_ValidationEntity(arbeidstilsynetsSamtykke2Form45957, XPathRoot, null);
        }
    }
}
