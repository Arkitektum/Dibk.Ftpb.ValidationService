using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IAktoerValidatorV2
    {
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(AktoerV2 tiltakshaver = null);
    }
}