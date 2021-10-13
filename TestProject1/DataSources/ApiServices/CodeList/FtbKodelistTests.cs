using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration.Json;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.DataSources.ApiServices.CodeList
{
    public class FtbKodelistTests
    {
        private IMemoryCache _memoryCache;
        private IOptions<CodelistApiSettings> _options;
        public FtbKodelistTests()
        {
            //https://stackoverflow.com/a/38319045
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            _memoryCache = serviceProvider.GetService<IMemoryCache>();
            
            var config = TestHelper.InitConfiguration();
            var ArbeidstilsynetUrl = config["CodeListApi:ArbeidstilsynetUrl"];

            _options = Options.Create(new CodelistApiSettings()
            {
                ArbeidstilsynetUrl = ArbeidstilsynetUrl,
            });
        }
        
        [Fact]
        public void TestTylTakstype()
        {
           
            //var tiltakstype
            
        }
    }
}
