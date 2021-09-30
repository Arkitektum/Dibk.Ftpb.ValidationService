namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class AktoerValidationEntity 
    {
        public KodelisteValidationEntity Partstype { get; set; }

        public string Foedselsnummer { get; set; }

        public string Organisasjonsnummer { get; set; }

        public string Navn { get; set; }

        public EnkelAdresseValidationEntity Adresse { get; set; }

        public string Telefonnummer { get; set; }

        public string Mobilnummer { get; set; }

        public string Epost { get; set; }

        public KontaktpersonValidationEntity Kontaktperson { get; set; }
    }
}
