using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IChecklistService
    {
        string GetPrefillChecklist(string soknadsType, ValidationResult validationResult);
        IEnumerable<Sjekk> GetAtilCheckpoints(string category);
        IEnumerable<Sjekk> GetDibkCheckpoints(string category);
    }
}