using System.IO;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.Utils
{
    public class SerializeUtilTests
    {
        [Fact]
        public void FilterValidationResultTest()
        {
            var xmlData_45957 = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            var xmlData_41999 = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_dfv41999.xml");

            var xmlData_41999_Common = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke_41999_Form>(xmlData_41999);
            var xmlData_45957_Common = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData_45957);

            xmlData_41999_Common.Should().NotBeNull();
            xmlData_45957_Common.Should().NotBeNull();
        }
    }
}
