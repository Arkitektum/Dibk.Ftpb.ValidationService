using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IKodelisteValidator
    {
        //string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(KodelisteValidationEntity kodelistetype);
    }
}