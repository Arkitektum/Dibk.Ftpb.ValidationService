using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IChecklistService
    {
        IEnumerable<ChecklistAnswer> GetPrefillChecklistAnswer(string dataFormatVersion, PrefillChecklistInput prefillChecklistInput);
        IEnumerable<Sjekk> GetChecklist(string dataFormatVersion, string filter);
        IEnumerable<ValidationMessage> FilterValidationResult(string dataFormatVersion, IEnumerable<ValidationMessage> validationMessages, IEnumerable<string> tiltakstyper);
        FormProperties GetFormProperties(string dataFormatVersion);
    }
}