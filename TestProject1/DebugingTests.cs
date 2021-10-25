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
            var reference1 = "10000.1.35.21.2";
            var reference2 = "10000.1.27.29.126.1";
            var validatorEnums2 = TestHelper.DebugValidatorFormReference(reference2);

            var validatorEnums = TestHelper.DebugValidatorFormReference(reference);
            var validatorEnums1 = TestHelper.DebugValidatorFormReference(reference1);

            validatorEnums.Should().NotBeNullOrEmpty();
        }
    }
}
