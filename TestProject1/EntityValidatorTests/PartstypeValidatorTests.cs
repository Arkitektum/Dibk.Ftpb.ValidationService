using Dibk.Ftpb.Validation.Application.Enums;
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
            FormValidatorConfiguration formValidatorConfiguration = new FormValidatorConfiguration();

            var partsType = new Kodeliste() { Kodeverdi = "*Privatperson" };
            var ptValEntity = new KodelisteValidationEntity(partsType, "PartsType");
            //var result = new PartstypeValidator(formValidatorConfiguration, EntityValidatorEnum.FakturamottakerValidator, codeListService).Validate(ptValEntity);
            //result.Should().NotBeNull();
        }
    }
}
