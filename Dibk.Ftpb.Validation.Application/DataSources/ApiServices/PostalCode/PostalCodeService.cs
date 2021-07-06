using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode
{
    public class PostalCodeService : IPostalCodeService
    {
        private readonly PostalCodeHttpClient _postalCodeHttpClient;
        private readonly IMemoryCache _memoryCache;

        public PostalCodeService(PostalCodeHttpClient postalCodeHttpClient, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _postalCodeHttpClient = postalCodeHttpClient;
        }

        public async Task<PostalCodeValidationResult> ValidatePostnr(string pnr, string country)
        {
            return await GetValidationPostNr(pnr, country);
        }

        private async Task<PostalCodeValidationResult> GetValidationPostNr(string pnr, string country)
        {
            //Get from cache
            if (_memoryCache.TryGetValue<PostalCodeValidationResult>($"{pnr}:{country}", out var municipality))
                return municipality;
            //Get from API
            var ValidatePostnrFromApi = await _postalCodeHttpClient.ValidatePostnr(pnr, country);

            //Add to cache if found
            if (ValidatePostnrFromApi != null)
                _memoryCache.Set($"{pnr}:{country}", ValidatePostnrFromApi, new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = new TimeSpan(6, 0, 0)
                });

            return ValidatePostnrFromApi;
        }

    }
}
