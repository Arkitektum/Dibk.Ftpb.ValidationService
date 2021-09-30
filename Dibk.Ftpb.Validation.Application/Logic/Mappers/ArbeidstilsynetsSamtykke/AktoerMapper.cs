using System;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;
namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
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
                    Navn = mapFrom.navn,
                    Organisasjonsnummer = mapFrom.organisasjonsnummer
                };

                aktoerType.Adresse = EnkelAdresseMapper.Map(mapFrom.adresse);
                aktoerType.Kontaktperson = KontaktpersonMapper.Map(mapFrom.kontaktperson);
                aktoerType.Partstype = PartstypeMapper.Map(mapFrom.partstype);
            }

            return aktoerType;
        }
    }
}
