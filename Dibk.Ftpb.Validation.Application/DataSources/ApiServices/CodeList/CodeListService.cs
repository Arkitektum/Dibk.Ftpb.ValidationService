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

        public async Task<Dictionary<string, CodelistFormat>> GetCodeList(object codeListName, RegistryType registryType = RegistryType.Arbeidstilsynet)
        {
            var codeListNameSt = codeListName.ToString();
            return await GetCodeList(codeListName.ToString(), registryType);
        }

        public bool IsCodelistValid(object codeListName, string codeValue)
        {
            if (String.IsNullOrEmpty(codeValue)) return false;
            Dictionary<string, CodelistFormat> codelist = GetCodeList(codeListName, RegistryType.Arbeidstilsynet).Result;
            return codelist.ContainsKey(codeValue);
        }

        public bool IsCodelistLabelValid(object codeListName, string codeValue, string codeName, RegistryType registryType = RegistryType.Byggesoknad)
        {
            if (String.IsNullOrEmpty(codeValue) || string.IsNullOrEmpty(codeName)) return false;

            Dictionary<string, CodelistFormat> codelist = GetCodeList(codeListName, registryType).Result;
            CodelistFormat result;
            if (codelist.TryGetValue(codeValue, out result))
            {
                if (result.Name.Equals(codeName)) return true;
                else return false;
            }
            else return false;
        }


        private async Task<Dictionary<string, CodelistFormat>> GetCodeList(string cocelistName, RegistryType registryType = RegistryType.Arbeidstilsynet)
        {
            //Get from cache
            if (_memoryCache.TryGetValue<Dictionary<string, CodelistFormat>>(cocelistName, out var codeList))
                return codeList;

            //Get from API
            var jsonResponce = await _codelistApiHttpClient.GetCodeList(cocelistName, registryType);
            codeList = ParseCodeList(jsonResponce);

            //Add to cache if found
            if (jsonResponce != null)
            {
                _memoryCache.Set(cocelistName, codeList, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(6, 0, 0)
                });
            }
            return codeList;
        }
        private Dictionary<string, CodelistFormat> ParseCodeList(string jsonString)
        {
            try
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
            catch (Exception)
            {
                throw new ArgumentException($"Can not parse jsonResponse :'{jsonString}'");
            }
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
