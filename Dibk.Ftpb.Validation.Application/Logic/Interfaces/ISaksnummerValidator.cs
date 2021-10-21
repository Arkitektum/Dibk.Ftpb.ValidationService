using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface ISaksnummerValidator
    {
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(Saksnummer kodelistetype);
    }
}