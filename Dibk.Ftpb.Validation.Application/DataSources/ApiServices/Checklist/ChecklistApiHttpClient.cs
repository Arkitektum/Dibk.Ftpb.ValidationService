using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<IEnumerable<Sjekk>> Get(string category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("Category cannot be null");
            }

            var requestUri = $"/{category}";
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
    }
}
