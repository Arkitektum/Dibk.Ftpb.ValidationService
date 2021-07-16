using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
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
    public class ArbeidsplasserValidatorLogicTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private readonly IPostalCodeService _postalCodeService;

        private ArbeidsplasserValidatorLogic _arbeidsplasserValidatorLogic;
        private IArbeidsplasserValidator _arbeidsplasserValidator;
        private ArbeidsplasserValidationEntity _arbeidsplasserValidationEntity;

        public ArbeidsplasserValidatorLogicTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _arbeidsplasserValidationEntity = new Logic.Mappers.ArbeidstilsynetsSamtykke2.ArbeidsplasserMapper().Map(_form.arbeidsplasser, "UnitTest");

            _arbeidsplasserValidatorLogic = new ArbeidsplasserValidatorLogic(1);
            _arbeidsplasserValidator = _arbeidsplasserValidatorLogic.Validator;
        }

        [Fact]
        public void TestTree()
        {
            var validatorTree = _arbeidsplasserValidatorLogic.Tree;
            validatorTree.Count.Should().Be(1);
            validatorTree?.FirstOrDefault()?.Children.Count.Should().Be(0);
        }
        [Fact]
        public void TestValidationRules()
        {
            var validatorNodes = _arbeidsplasserValidatorLogic.ValidationRules;
            var rulesCount = validatorNodes.Count;

            rulesCount.Should().Be(6);

        }
        [Fact]
        public void TestValidator()
        {
            var validator = _arbeidsplasserValidatorLogic.Validator;
            var result = validator.Validate(_arbeidsplasserValidationEntity, new List<string>());

            result.Should().NotBeNull();

        }
    }
}
