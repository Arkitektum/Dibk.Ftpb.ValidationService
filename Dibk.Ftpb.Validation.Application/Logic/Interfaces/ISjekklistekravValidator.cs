using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface ISjekklistekravValidator
    {
        //string ruleXmlElement { get; }
        ValidationResult ValidationResult { get; }
        ValidationResult Validate(string dataFormatId, string dataFormatVersion, SjekklistekravValidationEntity[] sjekklistekrav, IChecklistService checklistService);
    }
}
