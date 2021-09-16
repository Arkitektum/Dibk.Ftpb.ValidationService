using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IChecklistService
    {
        IEnumerable<Sjekk> GetChecklist(string dataFormatVersion, string filter);
        IEnumerable<ValidationMessage> FilterValidationResult(string dataFormatVersion, IEnumerable<ValidationMessage> validationMessages, IEnumerable<string> tiltakstyper);
        ValidationResult GetValidationReport(ValidationResult validationResult, string dataFormatVersion);
    }
}