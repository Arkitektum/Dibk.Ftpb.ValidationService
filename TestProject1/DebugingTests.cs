using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class DebugingTests
    {
        [Fact]
        public void GetValidationEnumXpath()
        {

            var reference = "45957.4.12.15.2";
            var reference1 = "4595745957.4.12.19.5";
            var validatorEnums = TestHelper.DebugValidatorFormReference(reference);
            var validatorEnums1 = TestHelper.DebugValidatorFormReference(reference1);

            validatorEnums.Should().NotBeNullOrEmpty();
        }
    }
}
