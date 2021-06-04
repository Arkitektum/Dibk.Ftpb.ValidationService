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
            EntityValidatorOrchestrator entityValidatorOrchestrator = new EntityValidatorOrchestrator();

            var partsType = new PartstypeCode() { Kodeverdi = "*Privatperson" };
            var ptValEntity = new ParttypeCodeValidationEntity(partsType, "PartsType");
            var result = new PartstypeValidator(entityValidatorOrchestrator, "", codeListService).Validate(ptValEntity);
            result.Should().NotBeNull();
        }
    }
}
