using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Microsoft.Extensions.Options;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList
{
    public class CodelistApiHttpClient 
    {
        private readonly IOptions<CodelistApiSettings> _options;

        public CodelistApiHttpClient(IOptions<CodelistApiSettings> options)
        {
            _options = options;
        }

        public async Task<string> GetCodeList(string codeListName, RegistryType registryType)
        {
            string jsonCodeList = String.Empty;
            var url = GetUrlForCodeList(registryType);

            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(url);
                    httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var requestPath = $"{codeListName}";

                    var response = await httpClient.GetAsync(requestPath);

                    if (response.IsSuccessStatusCode)
                        jsonCodeList = await response.Content.ReadAsStringAsync();
                    else
                    {
                        //TODO do something 
                    }
                }
            }
            catch (Exception e)
            {
                //TODO Add logg and skip 'Throw'
                throw new ArgumentException($"Can not get codeList :'{codeListName}' from '{registryType.ToString()}' with Url: '{url}'");
            }
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
