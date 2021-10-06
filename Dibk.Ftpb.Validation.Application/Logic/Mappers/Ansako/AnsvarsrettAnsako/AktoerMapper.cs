using System;
using System.Linq.Expressions;
using AutoMapper;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.ansvarsrettAnsako;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.Ansako.AnsvarsrettAnsako
{
    public class AktoerMapper
    {

        public static Expression<Func<no.kxml.skjema.dibk.ansvarsrettAnsako.PartType, AktoerValidationEntity>> FromForetak
        {
            get
            {
                return p => new AktoerValidationEntity
                {
                    Epost = p.epost,
                    Foedselsnummer = p.foedselsnummer,
                    Mobilnummer = p.mobilnummer,
                    Telefonnummer = p.telefonnummer,
                    Navn = p.navn,
                    Organisasjonsnummer = p.organisasjonsnummer,
                    Adresse = EnkelAdresseMapper.Map(p.adresse),
                    Kontaktperson = KontaktpersonMapper.Map(p.kontaktperson),
                    Partstype = KodelisteValidationEntityMapper.Map(p.partstype)
                };
            }
        }

        public static AktoerValidationEntity MAP(PartType original)
        {
            var func = FromForetak.Compile();
            var aktor = func(original);

            return aktor;
        }

    }
}