using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEntityValidator
    {
        ValidationResult ResetValidationMessages();
        ValidationResult ValidationResult { get; set; }
        
    }
}
