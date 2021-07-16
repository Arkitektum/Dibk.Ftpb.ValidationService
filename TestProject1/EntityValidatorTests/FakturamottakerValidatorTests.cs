﻿using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.IO;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class FakturamottakerValidatorTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private readonly IPostalCodeService _postalCodeService;

        private FakturamottakerValidatorLogic _fakturamottakerValidatorLogic;
        private IFakturamottakerValidator _fakturamottakerValidator;
        private FakturamottakerValidationEntity _fakturamottaker;

        public FakturamottakerValidatorTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _fakturamottaker = new FakturamottakerMapper().Map(_form.fakturamottaker, "UnitTest");

            _fakturamottakerValidatorLogic = new FakturamottakerValidatorLogic(1, _postalCodeService);
            _fakturamottakerValidator = _fakturamottakerValidatorLogic.Validator;
        }

        [Fact]
        public void FakturaTest()
        {
            var result = _fakturamottakerValidatorLogic.Validator.Validate(_fakturamottaker);

            result.Should().NotBeNull();

        }
    }
}
