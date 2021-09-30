using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.IO;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class FakturamottakerValidatorTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private readonly IPostalCodeService _postalCodeService;

        private IFakturamottakerValidator _fakturamottakerValidator;
        private FakturamottakerValidationEntity _fakturamottaker;
        private IEnkelAdresseValidator _fakturamottakerEnkelAdresseValidator;

        public FakturamottakerValidatorTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _fakturamottaker = new FakturamottakerMapper().Map(_form.fakturamottaker, "");

            //fakturamottake
            var fakturamottakeNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.FakturamottakerValidator, ParentID = null},
            };
            var tree = EntityValidatiorTree.BuildTree(fakturamottakeNodeList);

            _fakturamottakerEnkelAdresseValidator = MockDataSource.enkelAdresseValidator();
            _fakturamottakerValidator = new FakturamottakerValidator(tree, _fakturamottakerEnkelAdresseValidator);

        }

        [Fact]
        public void FakturaTest()
        {
            _fakturamottaker.ModelData.EhfFaktura = null;
            _fakturamottaker.ModelData.FakturaPapir = null;
            _fakturamottakerValidator.ValidateEntityFields(_fakturamottaker);
            var result = _fakturamottakerValidator.ValidationResult.ValidationMessages;
            result.Count.Should().Be(1);
        }
        [Fact]
        public void Faktura_organisasjonsnummer_Utfylt()
        {
            _fakturamottaker.ModelData.Organisasjonsnummer = null;

            _fakturamottakerValidator.ValidateEntityFields(_fakturamottaker);
            var result = _fakturamottakerValidator.ValidationResult.ValidationMessages;
            result.Count.Should().Be(1);
        }
        [Fact]
        public void Faktura_Adresse()
        {
            _fakturamottaker.ModelData.EhfFaktura = null;
            _fakturamottaker.ModelData.FakturaPapir = true;
            
            _fakturamottaker.ModelData.Adresse.ModelData.Adresselinje1 = null;

           var result =  _fakturamottakerValidator.Validate(_fakturamottaker);

           result.messages.Should().NotBeNull();
        }
    }
}
