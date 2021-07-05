using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IArbeidsplasserValidator
    {
        //string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, List<string> attachments = null);
    }
}