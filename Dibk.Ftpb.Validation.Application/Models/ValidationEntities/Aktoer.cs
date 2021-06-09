namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class AktoerValidationEntity : ValidationEntityBase<Aktoer>
    {
        public AktoerValidationEntity(Aktoer modelData, string xmlElementName, string parentEntityDataModelXpath = null) 
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class Aktoer 
    {
        public PartstypeValidationEntity Partstype { get; set; }

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
