﻿using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class ChecklistApiHttpClient
    {
        public HttpClient _httpClient;
        protected readonly IOptions<IChecklistSettings> _options;
        private string _requestUrl;

        public ChecklistApiHttpClient(HttpClient httpClient, IOptions<IChecklistSettings> options)
        {
            _options = options;
            _httpClient = httpClient;
            var url = options.Value.BaseAddress;
            _requestUrl = _options.Value.ChecklistUrl;
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Sjekk>> GetChecklist(string category, string filter)
        {
            if (category == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            var requestUri = $"/{category}";
            if (!string.IsNullOrEmpty(filter))
            {
                requestUri += $"?{filter}";
            }
            var response = await _httpClient.GetAsync($"{_requestUrl}{requestUri}");
            IEnumerable<Sjekk> liste = null;
            string json = String.Empty;
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                liste = JsonConvert.DeserializeObject<IEnumerable<Sjekk>>(json);
            }
            return liste;
        }

        public async Task<IEnumerable<ChecklistValidationRelations>> GetChecklistValidationRelations()
        {
            var requestUri = $"/relatedvalidations";
            var response = await _httpClient.GetAsync($"{_requestUrl}{requestUri}");
            List<ChecklistValidationRelations> liste = new List<ChecklistValidationRelations>();
            string json = String.Empty;
            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                liste = JsonConvert.DeserializeObject<IEnumerable<ChecklistValidationRelations>>(json).ToList();
            }

            liste.Add(new ChecklistValidationRelations()
            {
                ChecklistReference = "1.3",
                ProcessCategory = "AT",
                EnterpriseTerms = new List<string>() { "brannskille", "nyttbyggdriftsbygningover1000m2", "lydskille" },
                SupportingDataValidationRuleId = new List<string>() { ".7.14.10.1", ".7.14.10.2", ".7.14.10.7", ".20.9.4.2" }
            });

            return liste;
        }

        public async Task<IEnumerable<ChecklistAnswer>> GetPrefillChecklistAnswer(PrefillChecklistInput prefillChecklist)
        {
            var requestUri = $"/prefill";
            var jsonPostRequest = System.Text.Json.JsonSerializer.Serialize(prefillChecklist);

            var stringContent = new StringContent(jsonPostRequest, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_requestUrl}{requestUri}", stringContent);
            IEnumerable<ChecklistAnswer> liste = null;
            string jsonResponse = String.Empty;
            if (response.IsSuccessStatusCode)
            {
                jsonResponse = await response.Content.ReadAsStringAsync();
                liste = JsonConvert.DeserializeObject<IEnumerable<ChecklistAnswer>>(jsonResponse);
            }
            return liste;
        }
    }
}
