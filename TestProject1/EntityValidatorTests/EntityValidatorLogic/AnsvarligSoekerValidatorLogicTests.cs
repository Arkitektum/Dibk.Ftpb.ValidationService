using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests.EntityValidatorLogic
{
    public class AnsvarligSoekerValidatorLogicTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        private AnsvarligSoekerValidatorLogic _ansvarligSoekerValidatorLogic;
        private IAktoerValidator _aktoerValidator;
        private AktoerValidationEntity _ansvarligSoeker;

        public AnsvarligSoekerValidatorLogicTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");
            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _ansvarligSoeker = new Logic.Mappers.ArbeidstilsynetsSamtykke2.AktoerMapper(AktoerEnum.ansvarligSoeker).Map(_form.ansvarligSoeker, "UnitTest");

            _ansvarligSoekerValidatorLogic = new AnsvarligSoekerValidatorLogic(1, _codeListService, _postalCodeService);
            _aktoerValidator = _ansvarligSoekerValidatorLogic.Validator;
        }

        [Fact]
        public void TestTree()
        {
            var validatorNodes = _ansvarligSoekerValidatorLogic.Tree;
            validatorNodes.Count.Should().Be(1);
            validatorNodes?.FirstOrDefault()?.Children.Count.Should().Be(3);
        }
        [Fact]
        public void TestNode()
        {
            var validatorNodes = _ansvarligSoekerValidatorLogic.NodeList;
            validatorNodes.Count.Should().Be(4);
        }
        [Fact]
        public void TestValidationRules()
        {
            var validatorNodes = _ansvarligSoekerValidatorLogic.ValidationRules;
            var rulesCount = validatorNodes.Count;

            rulesCount.Should().Be(21);

        }
        [Fact]
        public void TestValidator()
        {
            var result = _aktoerValidator.Validate(_ansvarligSoeker);

            result.Should().NotBeNull();
        }




    }
}
