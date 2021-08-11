using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IChecklistService
    {
        string GetPrefillChecklist(string soknadsType, ValidationResult validationResult);
    }
}