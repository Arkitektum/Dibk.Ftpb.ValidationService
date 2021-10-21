using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IAnsvarsomraadeValidator
    {
        ValidationResult ValidationResult { get; set; }

        ValidationResult Validate(Ansvarsomraade ansvarsomraade = null);
    }
}
