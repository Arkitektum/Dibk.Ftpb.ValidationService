using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class EnkelAdresseValidatorTests
    {
        private readonly ArbeidstilsynetsSamtykkeType _form;
        private readonly EnkelAdresseValidator _enkelAdresseValidator;
        private readonly IPostalCodeService _postalCodeService;
        private readonly EnkelAdresseValidationEntity _enkelAdresse;

        public EnkelAdresseValidatorTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _enkelAdresse = new EnkelAdresseMapper().Map(_form.fakturamottaker.adresse, "UnitTest");

            //fakturamottake
            var enkelAdresseNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = null}
            };
            var tree = EntityValidatiorTree.BuildTree(enkelAdresseNodeList);

            _enkelAdresseValidator = new EnkelAdresseValidator(tree, 1, _postalCodeService);
        }
        
        [Fact]
        public void EnkelAdressTest()
        {
            _enkelAdresse.ModelData = null;
            var result = _enkelAdresseValidator.Validate(_enkelAdresse);
            result.ValidationMessages.Count.Should().Be(1);
        }
        [Fact]
        public void AdresseLinje1_utfylt()
        {
            _enkelAdresse.ModelData.Adresselinje1 = null;
            var result = _enkelAdresseValidator.Validate(_enkelAdresse);
            result.ValidationMessages.Count.Should().Be(1);
        }
        [Fact]
        public void Landkode_Ugyldig()
        {
            _enkelAdresse.ModelData.Landkode = "NOKO";
            var result = _enkelAdresseValidator.Validate(_enkelAdresse);
            result.ValidationMessages.Count.Should().Be(1);
        }
    }
}
