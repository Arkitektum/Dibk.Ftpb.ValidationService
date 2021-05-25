namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class MatrikkelValidationEntity : ValidationEntityBase<Matrikkel>
    {
        public MatrikkelValidationEntity(Matrikkel modelData, string xmlElementName, string parentEntityDataModelXpath = null) 
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }

    public class Matrikkel
    {
        public string Kommunenummer { get; set; }
        public string Gaardsnummer { get; set; }
        public string Bruksnummer { get; set; }
        public string Festenummer { get; set; }
        public string Seksjonsnummer { get; set; }
    }
}