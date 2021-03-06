using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode
{
    public class PostalCodeHttpClient
    {
        public HttpClient _httpClient;

        public PostalCodeHttpClient(HttpClient httpClient, IOptions<PostalCodeSettings> options)
        {
            _httpClient = httpClient;

            var url = options.Value.BaseAddress;
            
            var requestUrl = options.Value.ClientAddress;
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-Bring-Client-URL", requestUrl);
        }

        public async Task<PostalCodeValidationResult> ValidatePostnr(string pnr, string country)
        {
            if (string.IsNullOrEmpty(pnr))
                return null;

            var requestPath = $"?pnr={pnr}&country={country?.ToUpper()}";
            var response = await _httpClient.GetAsync(requestPath);
            string jsonCodeList = String.Empty;
            PostalCodeValidationResult postalCodeValidationResult = null;

            if (response.IsSuccessStatusCode)
            {
                jsonCodeList = await response.Content.ReadAsStringAsync();
                postalCodeValidationResult = JsonConvert.DeserializeObject<PostalCodeValidationResult>(jsonCodeList);
            }
            return postalCodeValidationResult;
        }
    }
}
