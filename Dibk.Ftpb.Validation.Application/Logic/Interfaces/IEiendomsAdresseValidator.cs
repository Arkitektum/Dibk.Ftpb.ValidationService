using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEiendomsAdresseValidator
    {
        string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity);
    }
}