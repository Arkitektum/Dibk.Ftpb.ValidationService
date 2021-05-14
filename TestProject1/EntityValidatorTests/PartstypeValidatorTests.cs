using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
  public  class PartstypeValidatorTests
    {
        [Fact]
        public void TestPartstype()
        {
            var partsType = new PartstypeCode() {Kodeverdi = "*Privatperson"};
            var result = new PartstypeValidator("unitTEst").Validate(null, partsType);
            result.Should().NotBeNull();
        }
    }
}
