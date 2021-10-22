using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Ansako;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.FormValidatorTests
{
    public class AnsvarsrettAnsako_ANSAKO_10000_Validator_Tests
    {
        private AnsvarsrettAnsako_ANSAKO_10000_Validator _formValidator;
        private readonly IValidationMessageComposer _validationMessageComposer;
        private readonly IChecklistService _checklistService;

        private ICodeListService _codeListService;
        private IPostalCodeService _postalCodeService;
        private readonly IMunicipalityValidator _municipalityValidator;


        public AnsvarsrettAnsako_ANSAKO_10000_Validator_Tests()
        {
            _validationMessageComposer = new ValidationMessageComposer();
            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);

            _checklistService = MockDataSource.GetCheckpoints("AT");

            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i Telemark", "true");

            _formValidator = new AnsvarsrettAnsako_ANSAKO_10000_Validator(_validationMessageComposer, _municipalityValidator, _codeListService, _postalCodeService, _checklistService);
        }
         [Fact]
        public void ErklaeringAnsvarsrett_IntegrationTest()
        {
            var xmlData = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");
            ValidationInput validationInput = new();
            validationInput.FormData = xmlData;
            var newValidationReport = _formValidator.StartValidation(validationInput);
            newValidationReport.Should().NotBeNull();
        }
        [Fact]
        public void ErklaeringAnsvarsrett_RuleTest()
        {
            var xmlData = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");
            ValidationInput validationInput = new();
            validationInput.FormData = xmlData;
            var report = _formValidator.StartValidation(validationInput);
            var textNotFound = report.ValidationRules.Where(r => r.Message.Contains("Could not find")).ToArray();
            var rulesWithoutTextJson = JArray.FromObject(textNotFound);
            report.Should().NotBeNull();
        }

        [Fact]
        public void testInternal()
        {
            var xmlData = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");

            var noko = TestHelper.GetJsonForPostman(xmlData);

            noko.Should().NotBeEmpty();
        }
    }
}
