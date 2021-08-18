using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IArbeidsplasserValidator
    {
        //string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; set; }
        
        //TODO: Fix this
        ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, List<string> attachments = null);
        ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, IEnumerable<SjekklistekravValidationEntity> sjekkliste, List<string> attachments = null);
    }
}