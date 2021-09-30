using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class AktoerMapper
    {
        public AktoerValidationEntity Map(PartType mapFrom)
        {
            AktoerValidationEntity aktoerType = null;
            if (mapFrom != null)
            {
                aktoerType = new AktoerValidationEntity()
            {
                Epost = mapFrom.epost,
                Foedselsnummer = mapFrom.foedselsnummer,
                Mobilnummer = mapFrom.mobilnummer,
                Telefonnummer = mapFrom.telefonnummer,
                Navn = mapFrom.navn,
                Organisasjonsnummer = mapFrom.organisasjonsnummer
            };  

            aktoerType.Adresse = EnkelAdresseMapper.Map(mapFrom.adresse);
            aktoerType.Kontaktperson = KontaktpersonMapper.Map(mapFrom.kontaktperson);
            aktoerType.Partstype = KodelisteValidationEntityMapper.Map(mapFrom.partstype);
            }

            return aktoerType;
        }
    }
}