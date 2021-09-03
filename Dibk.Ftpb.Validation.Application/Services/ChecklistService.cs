using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class ChecklistService : IChecklistService
    {
        private readonly AtilChecklistApiHttpClient _atilChecklistApiHttpClient;
        private readonly DibkChecklistApiHttpClient _dibkChecklistApiHttpClient;

        public ChecklistService(AtilChecklistApiHttpClient atilChecklistApiHttpClient, DibkChecklistApiHttpClient dibkChecklistApiHttpClient)
        {
            _atilChecklistApiHttpClient = atilChecklistApiHttpClient;
            _dibkChecklistApiHttpClient = dibkChecklistApiHttpClient;
        }
        public string GetPrefillChecklist(string soknadsType, ValidationResult validationResult)
        {
            var response = _atilChecklistApiHttpClient.GetPrefillChecklist().Result;
            return "response";
        }
        public IEnumerable<Sjekk> GetAtilCheckpoints(string category)
        {
            var response = _atilChecklistApiHttpClient.GetChecklist(category).Result;
            return response;
        }
        public IEnumerable<Sjekk> GetDibkCheckpoints(string category)
        {
            var response = _dibkChecklistApiHttpClient.GetChecklist(category).Result;
            return response;
        }
    }
}
