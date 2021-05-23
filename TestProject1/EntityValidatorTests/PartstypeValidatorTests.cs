﻿using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public  class PartstypeValidatorTests
    {
        [Fact]
        public void TestPartstype()
        {
            var codeListService = MockDataSource.IsCodeListValid(ArbeidstilsynetCodeListNames.arbeidstilsynets_krav, true);

            var partsType = new PartstypeCode("PartsType") { Kodeverdi = "*Privatperson" };
            var result = new PartstypeValidator("unitTEst", codeListService).Validate(null, partsType);
            result.Should().NotBeNull();
        }
    }
}
