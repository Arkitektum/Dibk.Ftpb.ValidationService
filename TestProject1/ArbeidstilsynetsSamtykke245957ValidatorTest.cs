using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Xunit;
using Moq;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykke245957ValidatorTest
    {
        [Fact]
        public void Test1()
        {
            var municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);
            var noko = new ArbeidstilsynetsSamtykke2_45957_Validator(municipalityValidator);
            var dataFomr = noko.DeserializeDataForm("");
        }
    }
}
