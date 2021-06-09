namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class PartstypeValidationEntity : ValidationEntityBase<Partstype>
    {
        public PartstypeValidationEntity(Partstype modelData, string xmlElementName, string parentEntityDataModelXpath = null) : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class Partstype 
    {
        public string Kodeverdi { get; set; }
        public string Kodebeskrivelse { get; set; }
    }
}
