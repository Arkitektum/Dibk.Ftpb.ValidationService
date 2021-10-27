using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList
{
    public class CodeListService : ICodeListService
    {
        private readonly CodelistApiHttpClient _codelistApiHttpClient;
        private readonly IMemoryCache _memoryCache;

        public CodeListService(IMemoryCache memoryCache, IOptions<CodelistApiSettings> options)
        {
            _codelistApiHttpClient = new CodelistApiHttpClient(options);
            _memoryCache = memoryCache;
        }

        public async Task<Dictionary<string, CodelistFormat>> GetCodeList(object codeListName, RegistryType registryType)
        {
            var codeListNameSt = codeListName.ToString();
            return await GetCodeList(codeListName.ToString(), registryType);
        }

        public bool? IsCodelistValid(object codeListName, string codeValue, RegistryType registryType)
        {
            if (String.IsNullOrEmpty(codeValue)) return false;

            Dictionary<string, CodelistFormat> codelist = GetCodeList(codeListName, registryType).Result;

            if (codelist == null)
                return null;

            CodelistFormat result;
            if (codelist.TryGetValue(codeValue, out result))
                return result.Status.Equals("Gyldig", StringComparison.CurrentCultureIgnoreCase)
                    ||
                    result.Status.Equals("Sendt inn", StringComparison.CurrentCultureIgnoreCase);

            return false;
        }

        public CodelistFormat GetCodelistTagValue(object codeListName, string codeValue, RegistryType registryType)
        {
            CodelistFormat result = null;

            if (String.IsNullOrEmpty(codeValue))
            {
                return new CodelistFormat(codeListName.ToString(), null, "Utfylt");
            }
            else
            {
                Dictionary<string, CodelistFormat> codelist = GetCodeList(codeListName, registryType).Result;
                if (codelist == null)
                {
                    return new CodelistFormat(codeListName.ToString(), null, "IkkeValidert");
                }
                else
                {
                    if (!codelist.TryGetValue(codeValue, out result))
                    {
                        return new CodelistFormat(codeListName.ToString(), null, "ugyldig");
                    }
                }
            }
            return result;
        }


        public bool? IsCodelistLabelValid(object codeListName, string codeValue, string codeName, RegistryType registryType)
        {
            if (String.IsNullOrEmpty(codeValue) || string.IsNullOrEmpty(codeName)) return false;

            Dictionary<string, CodelistFormat> codelist = GetCodeList(codeListName, registryType).Result;

            if (codelist == null)
                return null;

            CodelistFormat result;
            if (codelist.TryGetValue(codeValue, out result))
                if (result.Name.Equals(codeName))
                    return true;

            return false;
        }


        private async Task<Dictionary<string, CodelistFormat>> GetCodeList(string cocelistName, RegistryType registryType)
        {
            Dictionary<string, CodelistFormat> codeList = null;
            //Get from cache
            if (_memoryCache.TryGetValue<Dictionary<string, CodelistFormat>>(cocelistName, out codeList))
                return codeList;

            //Get from API
            var jsonResponce = await _codelistApiHttpClient.GetCodeList(cocelistName, registryType);

            //Add to cache if found
            if (!string.IsNullOrEmpty(jsonResponce))
            {
                codeList = ParseCodeList(jsonResponce);
                _memoryCache.Set(cocelistName, codeList, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(6, 0, 0)
                });
            }

            return codeList;
        }
        private Dictionary<string, CodelistFormat> ParseCodeList(string jsonString)
        {
            var codeList = new Dictionary<string, CodelistFormat>(StringComparer.CurrentCultureIgnoreCase);

            try
            {

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
            }
            catch (Exception e)
            {
                //TODO Add logg and skip 'Throw'
                throw new ArgumentException($"Can not parse jsonResponse :'{jsonString}'");
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
