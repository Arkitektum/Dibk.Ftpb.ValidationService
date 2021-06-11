namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class KodelisteValidationEntity : ValidationEntityBase<Kodeliste>
    {
        public KodelisteValidationEntity(Kodeliste modelData, string xmlElementName, string parentEntityDataModelXpath = null) : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class Kodeliste 
    {
        public string Kodeverdi { get; set; }
        public string Kodebeskrivelse { get; set; }
    }
}
