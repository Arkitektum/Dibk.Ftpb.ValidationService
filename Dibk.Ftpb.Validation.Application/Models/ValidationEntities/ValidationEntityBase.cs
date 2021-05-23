namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public abstract class ValidationEntityBase
    {
        public ValidationEntityBase(string xmlElementName, ValidationEntityBase parentEntity = null)
        {
            XmlElementName = xmlElementName;
            ParentEntity = parentEntity;
        }

        private ValidationEntityBase ParentEntity { get; set; }
        private string XmlElementName { get; set; }
        public string GetXpathForEntity()
        {             
            return ParentEntity != null ? $"{ParentEntity.GetXpathForEntity()}/{XmlElementName}" : $"{XmlElementName}";                
        }
    }
}
