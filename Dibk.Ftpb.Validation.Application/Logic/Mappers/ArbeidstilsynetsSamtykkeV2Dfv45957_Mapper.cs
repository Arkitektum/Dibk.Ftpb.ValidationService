using AutoMapper;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers
{
    public class ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper
    {

        public ArbeidstilsynetsSamtykke2Form_45957 GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var formMapper = FormMapperConfiguration();
            var arbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykke2Form_45957();
            var eiendom = formMapper.Map<Eiendom>(dataModel.eiendomByggested);
            arbeidstilsynetsSamtykke2Form45957.Eiendom = eiendom;
            return arbeidstilsynetsSamtykke2Form45957;

        }
        public IMapper FormMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType, Models.ValidationEntities.Eiendom>();
                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MetadataType, Models.ValidationEntities.Metadata>();
            });


            return config.CreateMapper();
        }
    }
}
