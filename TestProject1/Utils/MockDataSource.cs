using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Moq;

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

        public static ICodeListService GetCodeListService(string codeValue, string lable = null)
        {
            var codelistClient = new Mock<ICodeListService>();
            var dictionary = new Dictionary<string, CodelistFormat>()
            {
                {codeValue, new CodelistFormat(lable,"","")}
            };

            codelistClient.Setup(a => a.GetCodeList(It.IsAny<string>(), It.IsAny<RegistryType>())).Returns(Task.FromResult(dictionary));

            return codelistClient.Object;
        }

    }
}
