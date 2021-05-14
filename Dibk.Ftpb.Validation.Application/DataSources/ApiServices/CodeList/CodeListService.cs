using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList
{
    public class CodeListService : ICodeListService
    {
        private readonly CodelistApiHttpClient _codelistApiHttpClient;
        private readonly IMemoryCache _memoryCache;

        public CodeListService(CodelistApiHttpClient codelistApiHttpClient, IMemoryCache memoryCache)
        {
            _codelistApiHttpClient = codelistApiHttpClient;
            _memoryCache = memoryCache;
        }

        public async Task<Dictionary<string, CodelistFormat>> GetCodeList(string cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet)
        {
            //Get from cache
            if (_memoryCache.TryGetValue<Dictionary<string, CodelistFormat>>(cocelistName, out var codeList))
                return codeList;

            //Get from API
            var jsonResponce = await _codelistApiHttpClient.GetCodeList(cocelistName, registryType);
            var codeListFromApi = ParseCodeList(jsonResponce);

            //Add to cache if found
            if (jsonResponce != null)
            {
                _memoryCache.Set(cocelistName, codeListFromApi, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(6, 0, 0)
                });
            }
            return codeList;
        }

        private Dictionary<string, CodelistFormat> ParseCodeList(string jsonString)
        {
            var codeList = new Dictionary<string, CodelistFormat>(StringComparer.CurrentCultureIgnoreCase);

            var response = JObject.Parse(jsonString);

            foreach (var code in response["containeditems"])
            {
                string codevalue = code["codevalue"].ToString();
                string codename = code["label"].ToString();
                string codestatus = code["status"].ToString();
                string codeDescription = code["description"]?.ToString();

                if (!string.IsNullOrWhiteSpace(codevalue) && !codeList.ContainsKey(codevalue))
                {
                    codeList.Add(codevalue, new CodelistFormat(codename, codeDescription, codestatus));
                }
            }

            return codeList;
        }
        private List<string> GetCodeListNames(string data)
        {
            var codeListNames = new List<string>();

            if (!string.IsNullOrEmpty(data))
            {
                var response = JObject.Parse(data);

                foreach (var registry in response["containedSubRegisters"])
                {
                    var registryId = registry["id"].ToString();

                    var codeListName = registryId.Split('/').Last();
                    codeListNames.Add(codeListName);
                }
            }
            else
            {
                //_logger.Error("Unable to find code list names. The JSON-string is empty.");
            }

            return codeListNames;
        }

    }
}
