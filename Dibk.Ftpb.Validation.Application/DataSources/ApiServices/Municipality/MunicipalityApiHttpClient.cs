using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Municipality
{
    public class MunicipalityApiHttpClient
    {
        public HttpClient HttpClient { get; }
        private readonly IOptions<MunicipalityApiSettings> _options;

        public MunicipalityApiHttpClient(HttpClient httpClient, IOptions<MunicipalityApiSettings> options)
        {
            _options = options;
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(_options.Value.BaseAddress);
            HttpClient.DefaultRequestHeaders.Authorization = GetAuthenticationHeader(options.Value.UserName, options.Value.Password);
        }

        public async Task<MunicipalityViewModel> GetMunicipality(string municipalityCode)
        {
            MunicipalityViewModel municipality = null;

            var requestPath = $"{municipalityCode}";
            var result = await HttpClient.GetAsync(requestPath);

            if (result.IsSuccessStatusCode)
                municipality = await result.Content.ReadFromJsonAsync<MunicipalityViewModel>();

            return municipality;
        }

        private AuthenticationHeaderValue GetAuthenticationHeader(string userName, string password)
        {
            var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
        }

    }
}
