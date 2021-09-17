using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IFakturamottakerValidator
    {
        //string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null);
        void ValidateEntityFields(FakturamottakerValidationEntity fakturamottaker);
    }
}