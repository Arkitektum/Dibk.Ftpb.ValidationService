using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Ansako;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.FormValidatorTests
{
    public class AnsvarsrettAnsako_ANSAKO_10000_Validator_Tests
    {
        private AnsvarsrettAnsako_ANSAKO_10000_Validator _formValidator;
        private readonly IValidationMessageComposer _validationMessageComposer;
        private readonly IChecklistService _checklistService;

        public AnsvarsrettAnsako_ANSAKO_10000_Validator_Tests()
        {
            _validationMessageComposer = new ValidationMessageComposer();
            _checklistService = MockDataSource.GetCheckpoints("AT");

            _formValidator = new AnsvarsrettAnsako_ANSAKO_10000_Validator(_validationMessageComposer, _checklistService);
        }
         [Fact]
        public void ValidatortestFor2Eiendommer()
        {
            var xmlData = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");
            ValidationInput validationInput = new();
            validationInput.FormData = xmlData;
            var newValidationReport = _formValidator.StartValidation("10000", validationInput);
            newValidationReport.Should().NotBeNull();
        }
    }
}
