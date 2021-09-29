using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.Models;
using Dibk.Ftpb.Validation.Application.Enums;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

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
            codelistClient.Setup(a => a.IsCodelistValid(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<RegistryType>())).Returns(valid);
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

        public static IFormaaltypeValidator formaaltypeValidator()
        {
            var formallTypeValidator = new Mock<IFormaaltypeValidator>();
            var validationResult = new ValidationResult();
            validationResult.ValidationRules = new List<ValidationRule>();
            validationResult.ValidationMessages = new EditableList<ValidationMessage>();
            formallTypeValidator.Setup(a => a.Validate(It.IsAny<FormaaltypeValidationEntity>())).Returns(validationResult);

            return formallTypeValidator.Object;
        }

        //Validator
        public static IKodelisteValidator KodelisteValidator(string xpath = null, ValidationRuleEnum? validationRule = null)
        {
            var kodelisteValidator = new Mock<IKodelisteValidator>();
            kodelisteValidator.Setup((a => a.Validate(It.IsAny<KodelisteValidationEntity>()))).Returns(ValidationResult(xpath, validationRule));
            return kodelisteValidator.Object;
        }

        public static IEnkelAdresseValidator enkelAdresseValidator(string xpath = null, ValidationRuleEnum? validationRule = null)
        {
            var adressValidator = new Mock<IEnkelAdresseValidator>();

            adressValidator.Setup(a => a.Validate(It.IsAny<EnkelAdresseValidationEntity>())).Returns(ValidationResult(xpath, validationRule));
            return adressValidator.Object;
        }

        public static IMatrikkelValidator MatrikkelValidator(string xpath = null,
            ValidationRuleEnum? validationRule = null)
        {
            var validator = new Mock<IMatrikkelValidator>();
            validator.Setup(a => a.Validate(It.IsAny<MatrikkelValidationEntity>())).Returns(ValidationResult(xpath, validationRule));
            return validator.Object;
        }
        public static IEiendomsAdresseValidator EiendomsAdresseValidator(string xpath = null, ValidationRuleEnum? validationRule = null)
        {
            var validator = new Mock<IEiendomsAdresseValidator>();
            validator.Setup(a => a.Validate(It.IsAny<EiendomsAdresseValidationEntity>())).Returns(ValidationResult(xpath, validationRule));
            return validator.Object;
        }

        private static ValidationResult ValidationResult(string xpath = null, ValidationRuleEnum? validationRule = null)
        {
            var validationResult = new ValidationResult()
            {
                ValidationRules = new List<ValidationRule>(),
                ValidationMessages = new List<ValidationMessage>()
            };

            if (string.IsNullOrEmpty(xpath) || validationRule.HasValue)
            {
                validationResult.ValidationMessages.Add(new ValidationMessage()
                {
                    XpathField = xpath,
                    Rule = validationRule?.ToString() ?? null,
                });
            }
            return validationResult;
        }
    }
}
