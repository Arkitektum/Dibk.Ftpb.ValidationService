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
    public class FakturamottakerValidatorLogicTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private readonly IPostalCodeService _postalCodeService;

        private FakturamottakerValidatorLogic _fakturamottakerValidatorLogic;
        private IFakturamottakerValidator _fakturamottakerValidator;
        private FakturamottakerValidationEntity _fakturamottaker;

        public FakturamottakerValidatorLogicTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _fakturamottaker = new Logic.Mappers.ArbeidstilsynetsSamtykke2.FakturamottakerMapper().Map(_form.fakturamottaker, "UnitTest");

            _fakturamottakerValidatorLogic = new FakturamottakerValidatorLogic(1,  _postalCodeService);
            _fakturamottakerValidator = _fakturamottakerValidatorLogic.Validator;
        }

        [Fact]
        public void TestTree()
        {
            var fakturamottakerTree = _fakturamottakerValidatorLogic.Tree;
            fakturamottakerTree.Count.Should().Be(1);
            fakturamottakerTree.FirstOrDefault().Children.Count.Should().Be(1);
        }
        [Fact]
        public void TestValidationRules()
        {
            var validatorNodes = _fakturamottakerValidatorLogic.ValidationRules;
            var rulesCount = validatorNodes.Count;

            rulesCount.Should().Be(9);

        }
    }
}
