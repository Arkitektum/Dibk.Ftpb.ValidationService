using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEiendomByggestedValidator
    {
        //string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(IEnumerable<EiendomValidationEntity> eiendomValidationEntities);
    }
}