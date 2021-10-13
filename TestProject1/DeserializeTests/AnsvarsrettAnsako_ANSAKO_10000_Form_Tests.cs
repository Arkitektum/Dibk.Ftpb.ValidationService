using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.DeserializeTests
{
  public  class AnsvarsrettAnsako_ANSAKO_10000_Form_Tests
    {
        [Fact]
        public void FormTest()
        {
            var xmlData_10000 = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");

            var xmlData_10000_Common = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(xmlData_10000);
            xmlData_10000_Common.Should().NotBeNull();
        }
        [Fact]
        public void ForetakTest()
        {
            var xmlData_10000 = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");

            var xmlData_10000_Common = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(xmlData_10000);
            var foretak = xmlData_10000_Common.Ansvarsretts.Foretak;
            foretak.Should().NotBeNull();
        }
        [Fact]
        public void AnsvarsomraadesTest()
        {
            var xmlData_10000 = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");

            var xmlData_10000_Common = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(xmlData_10000);
            var ansvarsomraades = xmlData_10000_Common.Ansvarsretts.Ansvarsomraades;
            ansvarsomraades.Should().NotBeNull();
        }
    }
}
