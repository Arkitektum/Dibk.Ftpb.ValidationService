using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;

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
        public static ICodeListService IsCodeListValid(object CodeListName, bool valid = true)
        {
            var codelistClient = new Mock<ICodeListService>();
            codelistClient.Setup(a => a.IsCodelistValid(It.IsAny<object>(), It.IsAny<string>(),It.IsAny<RegistryType>())).Returns(valid);
            return codelistClient.Object;
        }

        public static IPostalCodeService ValidatePostnr(bool valid, string result, string postalCodeType)
        {
            var postalCodeService = new Mock<IPostalCodeService>();
            postalCodeService.Setup((a => a.ValidatePostnr(It.IsAny<string>(), It.IsAny<string>())))
                .Returns(new PostalCodeValidationResult() { PostalCodeType = postalCodeType, Result = result, Valid = valid });
            return postalCodeService.Object;

        }
        public static IChecklistService GetCheckpoints(string category)
        {
            var checklistService = new Mock<IChecklistService>();
            checklistService.Setup((a => a.GetChecklist("", "")))
                .Returns(new List<Sjekk> { new Sjekk() { Id = "1.21", SjekkId = 2644, Navn = "Skal søknaden unntas offentilghet?", Prosesskategori = "AT", Rekkefolge = 26 } });
            return checklistService.Object;

        }


    }
}
