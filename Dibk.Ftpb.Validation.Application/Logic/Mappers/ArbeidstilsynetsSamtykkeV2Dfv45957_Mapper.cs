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
            var matrikkel = formMapper.Map<Matrikkel>(dataModel.eiendomByggested.eiendomsidentifikasjon);
            
            arbeidstilsynetsSamtykke2Form45957.Eiendom = eiendom;

            return arbeidstilsynetsSamtykke2Form45957;
        }
        public IMapper FormMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;

                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType, Models.ValidationEntities.Eiendom>()
                .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.adresse))
                .ForMember(dest => dest.Matrikkel, opt => opt.MapFrom(src => src.eiendomsidentifikasjon))
                .ForMember(dest => dest.Bygningsnummer, opt => opt.MapFrom(src => src.bygningsnummer))
                .ForMember(dest => dest.Bolignummer, opt => opt.MapFrom(src => src.bolignummer))
                .ForMember(dest => dest.Kommunenavn, opt => opt.MapFrom(src => src.kommunenavn));

                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendommensAdresseType, Models.ValidationEntities.EiendomsAdresse>()
                .ForMember(dest => dest.Adresselinje1, opt => opt.MapFrom(src => src.adresselinje1))
                .ForMember(dest => dest.Adresselinje2, opt => opt.MapFrom(src => src.adresselinje2))
                .ForMember(dest => dest.Adresselinje3, opt => opt.MapFrom(src => src.adresselinje3))
                .ForMember(dest => dest.Landkode, opt => opt.MapFrom(src => src.landkode))
                .ForMember(dest => dest.Postnr, opt => opt.MapFrom(src => src.postnr))
                .ForMember(dest => dest.Poststed, opt => opt.MapFrom(src => src.poststed))
                .ForMember(dest => dest.Gatenavn, opt => opt.MapFrom(src => src.gatenavn))
                .ForMember(dest => dest.Husnr, opt => opt.MapFrom(src => src.husnr))
                .ForMember(dest => dest.Bokstav, opt => opt.MapFrom(src => src.bokstav));

                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MatrikkelnummerType, Models.ValidationEntities.Matrikkel>()
                .ForMember(dest => dest.Gaardsnummer, opt => opt.MapFrom(src => src.gaardsnummer))
                .ForMember(dest => dest.Bruksnummer, opt => opt.MapFrom(src => src.bruksnummer))
                .ForMember(dest => dest.Seksjonsnummer, opt => opt.MapFrom(src => src.seksjonsnummer))
                .ForMember(dest => dest.Kommunenummer, opt => opt.MapFrom(src => src.kommunenummer))
                .ForMember(dest => dest.Festenummer, opt => opt.MapFrom(src => src.festenummer));

                //cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MetadataType, Models.ValidationEntities.Metadata>();
            });

            return config.CreateMapper();
        }
    }
}
