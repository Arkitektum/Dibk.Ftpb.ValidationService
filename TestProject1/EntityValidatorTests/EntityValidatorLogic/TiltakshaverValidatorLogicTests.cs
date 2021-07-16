using System.IO;
using System.Linq;
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
    public class TiltakshaverValidatorLogicTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private AktoerValidationEntity _tiltakshaver;

        private ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        private TiltakshaverValidatorLogic _tiltakshaverValidatorLogic;
        private IAktoerValidator _aktoerValidator;

        public TiltakshaverValidatorLogicTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");
            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957_Test.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _tiltakshaver = new Logic.Mappers.ArbeidstilsynetsSamtykke2.AktoerMapper(AktoerEnum.tiltakshaver).Map(_form.tiltakshaver, "UnitTest");

            _tiltakshaverValidatorLogic = new TiltakshaverValidatorLogic(1, _codeListService, _postalCodeService);
            _aktoerValidator = _tiltakshaverValidatorLogic.Validator;
        }

        [Fact]
        public void TestTree()
        {
            var TiltakshaverTree = _tiltakshaverValidatorLogic.Tree;
            TiltakshaverTree.Count.Should().Be(1);
            TiltakshaverTree.FirstOrDefault().Children.Count.Should().Be(3);
        }
        [Fact]
        public void TestNode()
        {
            var validatorNodes = _tiltakshaverValidatorLogic.NodeList;
            validatorNodes.Count.Should().Be(4);
        }
        [Fact]
        public void TestValidationRules()
        {
            var validatorNodes = _tiltakshaverValidatorLogic.ValidationRules;
            var numberRules = validatorNodes.Count;
        }


    }
}
