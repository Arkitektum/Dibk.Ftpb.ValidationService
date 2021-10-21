using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEiendomByggestedValidator
    {
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(Eiendom eiendom);
        void ValidateEntityFields(Eiendom eiendom);
    }
}