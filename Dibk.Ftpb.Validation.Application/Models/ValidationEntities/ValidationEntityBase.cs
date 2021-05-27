namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public abstract class ValidationEntityBase<T>
    {
        public T ModelData { get; set; }
        public string DataModelXpath { get; set; }

        public ValidationEntityBase(T modelData, string xmlElementName, string parentEntityDataModelXpath = null)
        {
            ModelData = modelData;
            DataModelXpath = parentEntityDataModelXpath != null ? $"{parentEntityDataModelXpath}/{xmlElementName}" : $"{xmlElementName}";
        }
    }
}
