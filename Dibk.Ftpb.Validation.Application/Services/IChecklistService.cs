using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IChecklistService
    {
        IEnumerable<Sjekk> GetChecklist(string dataFormatVersion, string filter);
        IEnumerable<ValidationMessage> FilterValidationResult(string dataFormatVersion, IEnumerable<ValidationMessage> validationMessages, IEnumerable<string> tiltakstyper);
        PrefillChecklist GetPrefillChecklist(ValidationResult validationResult, string dataFormatVersion, string processCategory);
        string GetPrefillDemo();
        FormProperties GetFormProperties(string dataFormatVersion);
    }
}