using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Options;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList
{
    public class CodelistApiHttpClient
    {
        public HttpClient _httpClient;
        private readonly IOptions<CodelistApiSettings> _options;

        public CodelistApiHttpClient(HttpClient httpClient, IOptions<CodelistApiSettings> options)
        {
            _options = options;
            _httpClient = httpClient;
        }

        public async Task<string> GetCodeList(string codeListName, RegistryType registryType)
        {
            var url = GetUrlForCodeList(registryType);
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestPath = $"{codeListName}";

            var response = await _httpClient.GetAsync(requestPath);
            string jsonCodeList = String.Empty;

            if (response.IsSuccessStatusCode)
                jsonCodeList = await response.Content.ReadAsStringAsync();

            return jsonCodeList;
        }

        private string GetUrlForCodeList(RegistryType registryType)
        {
            switch (registryType)
            {
                case RegistryType.Arbeidstilsynet:
                    return _options.Value.ArbeidstilsynetUrl;
                case RegistryType.Byggesoknad:
                    return _options.Value.ByggesoknadUrl;
                default:
                    throw new ArgumentException($"No URL registered for '{registryType}'.");
            }
        }
    }
}
