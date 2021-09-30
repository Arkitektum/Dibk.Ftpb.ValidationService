using AutoMapper;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class ArbeidstilsynetsSamtykke2_45957_Mapper
    {
        public ArbeidstilsynetsSamtykke2_45957_Form GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var arbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykke2_45957_Form();
            string parentPath = "";
            arbeidstilsynetsSamtykke2Form45957.MetadataValidationEntity = new MetadataMapper().Map(dataModel.metadata);
            arbeidstilsynetsSamtykke2Form45957.TiltakshaverValidationEntity = new AktoerMapper().Map(dataModel.tiltakshaver);
            arbeidstilsynetsSamtykke2Form45957.AnsvarligSoekerValidationEntity = new AktoerMapper().Map(dataModel.ansvarligSoeker);
            arbeidstilsynetsSamtykke2Form45957.EiendomValidationEntities = new EiendomByggestedMapper().Map(dataModel.eiendomByggested);
            arbeidstilsynetsSamtykke2Form45957.ArbeidsplasserValidationEntity = new ArbeidsplasserMapper().Map(dataModel.arbeidsplasser);


            arbeidstilsynetsSamtykke2Form45957.BetalingValidationEntity = new BetalingMapper().Map(dataModel.betaling, parentPath);
            arbeidstilsynetsSamtykke2Form45957.FakturamottakerValidationEntity = new FakturamottakerMapper().Map(dataModel.fakturamottaker, parentPath);
            arbeidstilsynetsSamtykke2Form45957.SjekklistekravValidationEntities = new SjekklistekravMapper().Map(dataModel.krav, parentPath);
            arbeidstilsynetsSamtykke2Form45957.BeskrivelseAvTiltakValidationEntity = new BeskrivelseAvTiltakMapper().Map(dataModel.beskrivelseAvTiltak, parentPath);
            arbeidstilsynetsSamtykke2Form45957.ArbeidstilsynetsSaksnummerValidationEntity = new ArbeidstilsynetsSaksnummerMapper().Map(dataModel.arbeidstilsynetsSaksnummer, parentPath);
            arbeidstilsynetsSamtykke2Form45957.KommunensSaksnummerValidationEntity = new KommunensSaksnummerMapper().Map(dataModel.kommunensSaksnummer, parentPath);

            return arbeidstilsynetsSamtykke2Form45957;
        }

    }
}
