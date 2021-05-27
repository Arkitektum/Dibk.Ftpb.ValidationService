namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class MetadataValidationEntity : ValidationEntityBase<Metadata>
    {
        public MetadataValidationEntity(Metadata modelData, string xmlElementName, string parentEntityDataModelXpath = null) 
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class Metadata
    {
        public string FraSluttbrukersystem { get; set; }
        public string Prosjektnavn { get; set; }
    }
}
