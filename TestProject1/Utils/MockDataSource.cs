using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices;
using Dibk.Ftpb.Validation.Application.Enums;
using Moq;

namespace Dibk.Ftpb.Validation.Application.Tests.Utils
{
    public class MockDataSource
    {
        public static IMunicipalityValidator MunicipalityValidatorResult(MunicipalityValidationEnum validationStatus)
        {

            var municipalityValidator = new Mock<IMunicipalityValidator>();
            municipalityValidator.Setup(s => s.Validate_kommunenummerStatus(It.IsAny<string>())).Returns(new MunisipalituValidationResult() { Status = validationStatus, Message = $"kommunenumer er : '{validationStatus.ToString()}'." });
            return municipalityValidator.Object;
            
        }

    }
}
