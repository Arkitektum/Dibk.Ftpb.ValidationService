using AutoMapper;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers
{
    public class ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper
    {
        private IMapper _fomMapper;

        public ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper()
        {
            _fomMapper = FormMapperConfiguration();
        }
        public ArbeidstilsynetsSamtykke2Form_45957 GetFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var arbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykke2Form_45957();
            var formMapper = FormMapperConfiguration();

            List<Eiendom> eiendommer = new();
            foreach (var eiendomByggested in dataModel.eiendomByggested)
            {
                eiendommer.Add(formMapper.Map<Eiendom>(eiendomByggested));
            }

            arbeidstilsynetsSamtykke2Form45957.Eiendommer = eiendommer;
            arbeidstilsynetsSamtykke2Form45957.Arbeidsplasser = MapArbeidsplasser(dataModel.arbeidsplasser);
            arbeidstilsynetsSamtykke2Form45957.Tiltakshaver = MapTiltakshaver(dataModel.tiltakshaver);

            return arbeidstilsynetsSamtykke2Form45957;
        }

        public Arbeidsplasser MapArbeidsplasser(ArbeidsplasserType arbeidsplasserType)
        {
            var arbeidspalsser = _fomMapper.Map<Arbeidsplasser>(arbeidsplasserType);
            return arbeidspalsser;
        }

        public Aktoer MapTiltakshaver(PartType partType)
        {
            var tiltakstype = _fomMapper.Map<Aktoer>(partType);
            return tiltakstype;
        }
        public IMapper FormMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;

                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType, Eiendom>()
                .ForMember(dest => dest.Adresse, opt => opt.MapFrom(src => src.adresse))
                .ForMember(dest => dest.Matrikkel, opt => opt.MapFrom(src => src.eiendomsidentifikasjon))
                .ForMember(dest => dest.Bygningsnummer, opt => opt.MapFrom(src => src.bygningsnummer))
                .ForMember(dest => dest.Bolignummer, opt => opt.MapFrom(src => src.bolignummer))
                .ForMember(dest => dest.Kommunenavn, opt => opt.MapFrom(src => src.kommunenavn));

                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendommensAdresseType, EiendomsAdresse>()
                .ForMember(dest => dest.Adresselinje1, opt => opt.MapFrom(src => src.adresselinje1))
                .ForMember(dest => dest.Adresselinje2, opt => opt.MapFrom(src => src.adresselinje2))
                .ForMember(dest => dest.Adresselinje3, opt => opt.MapFrom(src => src.adresselinje3))
                .ForMember(dest => dest.Landkode, opt => opt.MapFrom(src => src.landkode))
                .ForMember(dest => dest.Postnr, opt => opt.MapFrom(src => src.postnr))
                .ForMember(dest => dest.Poststed, opt => opt.MapFrom(src => src.poststed))
                .ForMember(dest => dest.Gatenavn, opt => opt.MapFrom(src => src.gatenavn))
                .ForMember(dest => dest.Husnr, opt => opt.MapFrom(src => src.husnr))
                .ForMember(dest => dest.Bokstav, opt => opt.MapFrom(src => src.bokstav));

                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.MatrikkelnummerType, Matrikkel>()
                .ForMember(dest => dest.Gaardsnummer, opt => opt.MapFrom(src => src.gaardsnummer))
                .ForMember(dest => dest.Bruksnummer, opt => opt.MapFrom(src => src.bruksnummer))
                .ForMember(dest => dest.Seksjonsnummer, opt => opt.MapFrom(src => src.seksjonsnummer))
                .ForMember(dest => dest.Kommunenummer, opt => opt.MapFrom(src => src.kommunenummer))
                .ForMember(dest => dest.Festenummer, opt => opt.MapFrom(src => src.festenummer));
                //Arbeidsplasser
                cfg.CreateMap<ArbeidsplasserType, Arbeidsplasser>();
                
                //Aktoer - (Tltakshaver/AnsvarligSæker)
                cfg.CreateMap<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.PartType, Aktoer>();
                cfg.CreateMap<EnkelAdresseType, EnkelAdresse>();
                cfg.CreateMap<KodeType, Dibk.Ftpb.Validation.Application.Models.ValidationEntities.PartstypeCode>();
                cfg.CreateMap<KontaktpersonType,Kontaktperson>();

            });

            return config.CreateMapper();
        }
    }
}
