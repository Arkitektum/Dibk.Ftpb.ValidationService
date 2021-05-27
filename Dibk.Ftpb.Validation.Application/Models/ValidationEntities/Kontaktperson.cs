namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class KontaktpersonValidationEntity : ValidationEntityBase<Kontaktperson>
    {
        public KontaktpersonValidationEntity(Kontaktperson modelData, string xmlElementName, string parentEntityDataModelXpath = null) 
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class Kontaktperson
    { 
        public string Navn { get; set; }

        public string Telefonnummer { get; set; }

        public string Mobilnummer { get; set; }

        public string Epost { get; set; }
    }
}
