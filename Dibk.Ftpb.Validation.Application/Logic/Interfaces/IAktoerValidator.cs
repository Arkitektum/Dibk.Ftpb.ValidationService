using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IAktoerValidator
    {
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(Aktoer tiltakshaver = null);
    }
}