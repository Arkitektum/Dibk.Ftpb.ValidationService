using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IMetadataValidator
    {
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(Models.ValidationEntities.Metadata matrikkel);
    }
}