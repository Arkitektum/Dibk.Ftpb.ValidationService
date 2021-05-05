namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEntityValidator
    {
        void InitializeValidationRules(string context);
        void ValidateEntityFields(object entityData);
    }
}
