namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class ParttypeCodeValidationEntity : ValidationEntityBase<PartstypeCode>
    {
        public ParttypeCodeValidationEntity(PartstypeCode modelData, string xmlElementName, string parentEntityDataModelXpath = null) : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class PartstypeCode 
    {
        public string Kodeverdi { get; set; }
        public string Kodebeskrivelse { get; set; }
    }
}
