using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class TiltakshaverMapper// : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke.PartType, AktoerValidationEntity>
    {
        //public override AktoerValidationEntity Map(PartType mapFrom, string parentElementXpath = null)
        //{
        //    Aktoer tiltakshaver = null;
        //    if (mapFrom != null)
        //    {
        //        tiltakshaver = new Aktoer()
        //    {
        //        Epost = mapFrom.epost,
        //        Foedselsnummer = mapFrom.foedselsnummer,
        //        Mobilnummer = mapFrom.mobilnummer,
        //        Navn = mapFrom.navn,
        //        Organisasjonsnummer = mapFrom.organisasjonsnummer
        //    };  

        //    tiltakshaver.Adresse = new EnkelAdresseMapper().Map(mapFrom.adresse, $"{parentElementXpath}/tiltakshaver");
        //        tiltakshaver.Kontaktperson = new KontaktpersonMapper().Map(mapFrom.kontaktperson, $"{parentElementXpath}/tiltakshaver");
        //        tiltakshaver.Partstype = new PartstypeCodeMapper().Map(mapFrom.partstype, $"{parentElementXpath}/tiltakshaver");
        //    }

        //    return  new AktoerValidationEntity(tiltakshaver, "tiltakshaver", parentElementXpath);
        //}

        //private class KontaktpersonMapper : ModelToValidationEntityMapper<KontaktpersonType, KontaktpersonValidationEntity>
        //{
        //    public override KontaktpersonValidationEntity Map(KontaktpersonType mapFrom, string parentElementXpath = null)
        //    {
        //        Kontaktperson kontaktperson = null;
        //        if (mapFrom != null)
        //            kontaktperson =  new Kontaktperson()
        //        {
        //            Epost = mapFrom.epost,
        //            Mobilnummer = mapFrom.mobilnummer,
        //            Navn = mapFrom.navn,
        //            Telefonnummer = mapFrom.telefonnummer
        //        };

        //        return new KontaktpersonValidationEntity(kontaktperson, "Kontaktperson", parentElementXpath);
        //    }
        //}

        //private class PartstypeCodeMapper : ModelToValidationEntityMapper<KodeType, KodelisteValidationEntity>
        //{
        //    public override KodelisteValidationEntity Map(KodeType mapFrom, string parentElementXpath = null)
        //    {
        //        Kodeliste partstypeCode = null;
        //        if (mapFrom != null)
        //            partstypeCode= new Kodeliste()
        //        {
        //            Kodebeskrivelse = mapFrom.kodebeskrivelse,
        //            Kodeverdi = mapFrom.kodeverdi
        //        };

        //        return new KodelisteValidationEntity(partstypeCode, "partstype", parentElementXpath);
        //    }
        //}
    }
}