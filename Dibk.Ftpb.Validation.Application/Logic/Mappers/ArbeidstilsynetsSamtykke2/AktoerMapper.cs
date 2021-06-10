using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class AktoerMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.PartType, AktoerValidationEntity>
    {
        private readonly AktoerEnum _aktoerRole;

        public AktoerMapper(AktoerEnum aktoerRole)
        {
            _aktoerRole = aktoerRole;
        }
        public override AktoerValidationEntity Map(PartType mapFrom, string parentElementXpath = null)
        {
            Aktoer tiltakshaver = null;
            if (mapFrom != null)
            {
                tiltakshaver = new Aktoer()
            {
                Epost = mapFrom.epost,
                Foedselsnummer = mapFrom.foedselsnummer,
                Mobilnummer = mapFrom.mobilnummer,
                Navn = mapFrom.navn,
                Organisasjonsnummer = mapFrom.organisasjonsnummer
            };  

            tiltakshaver.Adresse = new EnkelAdresseMapper().Map(mapFrom.adresse, $"{parentElementXpath}/{Enum.GetName(typeof(AktoerEnum), _aktoerRole)}");
                tiltakshaver.Kontaktperson = new KontaktpersonMapper().Map(mapFrom.kontaktperson, $"{parentElementXpath}/{Enum.GetName(typeof(AktoerEnum), _aktoerRole)}");
                tiltakshaver.Partstype = new PartstypeMapper().Map(mapFrom.partstype, $"{parentElementXpath}/{Enum.GetName(typeof(AktoerEnum), _aktoerRole)}");
            }

            return  new AktoerValidationEntity(tiltakshaver, Enum.GetName(typeof(AktoerEnum), _aktoerRole), parentElementXpath);
        }

        private class KontaktpersonMapper : ModelToValidationEntityMapper<KontaktpersonType, KontaktpersonValidationEntity>
        {
            public override KontaktpersonValidationEntity Map(KontaktpersonType mapFrom, string parentElementXpath = null)
            {
                Kontaktperson kontaktperson = null;
                if (mapFrom != null)
                    kontaktperson =  new Kontaktperson()
                {
                    Epost = mapFrom.epost,
                    Mobilnummer = mapFrom.mobilnummer,
                    Navn = mapFrom.navn,
                    Telefonnummer = mapFrom.telefonnummer
                };

                return new KontaktpersonValidationEntity(kontaktperson, "kontaktperson", parentElementXpath);
            }
        }

        private class PartstypeMapper : ModelToValidationEntityMapper<KodeType, PartstypeValidationEntity>
        {
            public override PartstypeValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
            {
                Partstype partstypeCode = null;
                if (mapFrom != null)
                    partstypeCode= new Partstype()
                {
                    Kodebeskrivelse = mapFrom.kodebeskrivelse,
                    Kodeverdi = mapFrom.kodeverdi
                };

                return new PartstypeValidationEntity(partstypeCode, "partstype", parentElementXpath);
            }
        }
    }
}