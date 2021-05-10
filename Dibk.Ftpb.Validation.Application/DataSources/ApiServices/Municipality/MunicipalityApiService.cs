using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Municipality
{
    public class MunicipalityApiService : IMunicipalityApiService
    {
        private readonly MunicipalityApiHttpClient municipalityApiHttpClient;
        private readonly IMemoryCache memoryCache;

        public MunicipalityApiService(MunicipalityApiHttpClient municipalityApiHttpClient, IMemoryCache memoryCache)
        {
            this.municipalityApiHttpClient = municipalityApiHttpClient;
            this.memoryCache = memoryCache;
        }
        public async Task<MunicipalityViewModel> GetMunicipality(string municipalityCode)
        {
            //Get from cache
            if (memoryCache.TryGetValue<MunicipalityViewModel>(municipalityCode, out var municipality))
                return municipality;

            //Get from API
            var municipalityFromApi = await municipalityApiHttpClient.GetMunicipality(municipalityCode);

            //Add to cache if found
            if (municipalityFromApi != null)
                memoryCache.Set(municipalityCode, municipalityFromApi, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(6, 0, 0)
                });


            return municipalityFromApi;
        }
    }
}
