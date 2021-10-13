using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.DataSources.ApiServices.CodeList
{
    public class SosiKodelisterTests
    {
        private readonly IMemoryCache? _memoryCache;
        private readonly IOptions<CodelistApiSettings> _options;

        public SosiKodelisterTests()
        {
            //https://stackoverflow.com/a/38319045
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            _memoryCache = serviceProvider.GetService<IMemoryCache>();

            var config = TestHelper.InitConfiguration();
            var sosiKodelisterUrl = config["CodeListApi:SosiKodelisterUrl"];

            _options = Options.Create(new CodelistApiSettings()
            {
                SosiKodelisterUrl = sosiKodelisterUrl,
            });
        }
        [Fact]
        public void GetCodeList()
        {

            var codeListService = new CodeListService(_memoryCache, _options);
            var kommuneListe = codeListService.GetCodeList(SosiKodelisterEnum.kommunenummer, RegistryType.SosiKodelister);
            var list = kommuneListe.Result;
            list.Should().NotBeNull();
        }
        [Fact]
        public void GetCodelistTagValueTest()
        {

            var codeListService = new CodeListService(_memoryCache, _options);
            var kommuneListe = codeListService.GetCodelistTagValue(SosiKodelisterEnum.kommunenummer,"3817", RegistryType.SosiKodelister);
            var list = kommuneListe;
            list.Should().NotBeNull();
        }
    }
}
