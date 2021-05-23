using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public partial class TiltakshaverMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.PartType, Aktoer>
    {
        public override Aktoer Map(PartType mapFrom, ValidationEntityBase parentEntity = null)
        {
            if (mapFrom == null) return null;
            var tiltakshaver = new Aktoer("Tiltakshaver", parentEntity)
            {
                Epost = mapFrom.epost,
                Foedselsnummer = mapFrom.foedselsnummer,
                Mobilnummer = mapFrom.mobilnummer,
                Navn = mapFrom.navn,
                Organisasjonsnummer = mapFrom.organisasjonsnummer
            };

            tiltakshaver.Adresse = new EnkelAdresseMapper().Map(mapFrom.adresse, tiltakshaver);
            tiltakshaver.Kontaktperson = new KontaktpersonMapper().Map(mapFrom.kontaktperson, tiltakshaver);
            tiltakshaver.Partstype = new PartstypeCodeMapper().Map(mapFrom.partstype, tiltakshaver);            

            return tiltakshaver;
        }

        private class KontaktpersonMapper : ModelToValidationEntityMapper<KontaktpersonType, Kontaktperson>
        {
            public override Kontaktperson Map(KontaktpersonType mapFrom, ValidationEntityBase parentEntity = null)
            {
                if (mapFrom == null) return null;
                return new Kontaktperson("Kontaktperson", parentEntity)
                {
                    Epost = mapFrom.epost,
                    Mobilnummer = mapFrom.mobilnummer,
                    Navn = mapFrom.navn,
                    Telefonnummer = mapFrom.telefonnummer
                };
            }
        }

        private class PartstypeCodeMapper : ModelToValidationEntityMapper<KodeType, PartstypeCode>
        {
            public override PartstypeCode Map(KodeType mapFrom, ValidationEntityBase parentEntity = null)
            {
                if (mapFrom == null) return null;
                return new PartstypeCode("PartsType", parentEntity)
                {
                    Kodebeskrivelse = mapFrom.kodebeskrivelse,
                    Kodeverdi = mapFrom.kodeverdi
                };
            }
        }
    }
}