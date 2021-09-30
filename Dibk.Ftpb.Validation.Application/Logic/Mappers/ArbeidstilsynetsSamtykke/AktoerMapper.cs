using System;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;
namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class AktoerMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke.PartType, AktoerValidationEntity>
    {
        private readonly AktoerEnum _aktoerEnum;

        public AktoerMapper(AktoerEnum aktoerEnum)
        {
            _aktoerEnum = aktoerEnum;
        }
        public override AktoerValidationEntity Map(PartType mapFrom, string parentElementXpath = null)
        {
            Aktoer aktoerType = null;
            if (mapFrom != null)
            {
                aktoerType = new Aktoer()
                {
                    Epost = mapFrom.epost,
                    Foedselsnummer = mapFrom.foedselsnummer,
                    Mobilnummer = mapFrom.mobilnummer,
                    Navn = mapFrom.navn,
                    Organisasjonsnummer = mapFrom.organisasjonsnummer
                };

                aktoerType.Adresse = EnkelAdresseMapper.Map(mapFrom.adresse);
                aktoerType.Kontaktperson = new KontaktpersonMapper().Map(mapFrom.kontaktperson, $"{parentElementXpath}/{Enum.GetName(typeof(AktoerEnum), _aktoerEnum)}");
                aktoerType.Partstype = new PartstypeMapper().Map(mapFrom.partstype, $"{parentElementXpath}/{Enum.GetName(typeof(AktoerEnum), _aktoerEnum)}");
            }

            return new AktoerValidationEntity(aktoerType, Enum.GetName(typeof(AktoerEnum), _aktoerEnum), parentElementXpath);
        }

        private class KontaktpersonMapper : ModelToValidationEntityMapper<KontaktpersonType, KontaktpersonValidationEntity>
        {
            public override KontaktpersonValidationEntity Map(KontaktpersonType mapFrom, string parentElementXpath = null)
            {
                Kontaktperson kontaktperson = null;
                if (mapFrom != null)
                    kontaktperson = new Kontaktperson()
                    {
                        Epost = mapFrom.epost,
                        Mobilnummer = mapFrom.mobilnummer,
                        Navn = mapFrom.navn,
                        Telefonnummer = mapFrom.telefonnummer
                    };

                return new KontaktpersonValidationEntity(kontaktperson, "kontaktperson", parentElementXpath);
            }
        }

        private class PartstypeMapper : ModelToValidationEntityMapper<KodeType, KodelisteValidationEntity>
        {
            public override KodelisteValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
            {
                Kodeliste partstypeCode = null;
                if (mapFrom != null)
                    partstypeCode = new Kodeliste()
                    {
                        Kodebeskrivelse = mapFrom.kodebeskrivelse,
                        Kodeverdi = mapFrom.kodeverdi
                    };

                return new KodelisteValidationEntity(partstypeCode, "partstype", parentElementXpath);
            }
        }
    }
}
