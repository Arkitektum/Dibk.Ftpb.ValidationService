using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.Common;
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
            var xmlData_10000 = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");

            var xmlData_41999_Common = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke_41999_Common>(xmlData_41999);
            var xmlData_45957_Common = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke_45957_Common>(xmlData_45957);
            var xmlData_10000_Common = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Common>(xmlData_10000);


            xmlData_10000_Common.Should().NotBeNull();


        }
    }
}
