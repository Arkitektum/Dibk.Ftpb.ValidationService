using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Tests.Utils
{
    public class MockDataSource
    {
        public static IMunicipalityValidator MunicipalityValidatorResult(MunicipalityValidationEnum validationStatus)
        {

            var municipalityValidator = new Mock<IMunicipalityValidator>();
            municipalityValidator.Setup(s => s.Validate_kommunenummerStatus(It.IsAny<string>())).Returns(new MunicipalityValidationResult() { Status = validationStatus, Message = $"kommunenumer er : '{validationStatus.ToString()}'." });
            return municipalityValidator.Object;

        }

        public static ICodeListService GetCodeListService(ArbeidstilsynetCodeListNames codeValue, string lable = null)
        {
            var codelistClient = new Mock<ICodeListService>();
            var dictionary = new Dictionary<string, CodelistFormat>()
            {
                {codeValue.ToString(), new CodelistFormat(lable,"","")}
            };

            codelistClient.Setup(a => a.GetCodeList(It.IsAny<ArbeidstilsynetCodeListNames>(), It.IsAny<RegistryType>())).Returns(Task.FromResult(dictionary));

            return codelistClient.Object;
        }
        public static ICodeListService IsCodeListValid(ArbeidstilsynetCodeListNames CodeListName, bool valid = true)
        {
            var codelistClient = new Mock<ICodeListService>();
            codelistClient.Setup(a => a.IsCodelistValid(It.IsAny<ArbeidstilsynetCodeListNames>(), It.IsAny<string>())).Returns(valid);
            return codelistClient.Object;
        }
        public static ICodeListService IsCodeListValid(FtbCodeListNames CodeListName, bool valid = true)
        {
            var codelistClient = new Mock<ICodeListService>();
            codelistClient.Setup(a => a.IsCodelistValid(It.IsAny<ArbeidstilsynetCodeListNames>(), It.IsAny<string>())).Returns(valid);
            return codelistClient.Object;
        }

    }
}
