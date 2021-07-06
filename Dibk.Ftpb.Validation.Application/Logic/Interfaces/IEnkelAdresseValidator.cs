using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEnkelAdresseValidator : IEntityBaseValidator
    {
        //string ruleXmlElement { get; }
       // ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null);
        void ValidateEntityFields(EnkelAdresseValidationEntity adresseValidationEntity);
    }
}