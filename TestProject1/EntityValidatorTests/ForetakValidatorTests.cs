using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class ForetakValidatorTests
    {
        private ICodeListService _codeListService;
        private IPostalCodeService _postalCodeService;

        //**foretak
        private IAktoerValidator _foretakValidator;
        private IKontaktpersonValidator _foretakKontaktpersonValidator;
        private IKodelisteValidator _foretakPartstypeValidator;
        private IEnkelAdresseValidator _foretakEnkelAdresseValidator;

        private AnsvarsrettAnsako_ANSAKO_10000_Form _form;

        public ForetakValidatorTests()
        {
            var xmlData = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");
            _form = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(xmlData);

            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i Telemark", "true");

            var foretakNodeList = new List<EntityValidatorNode>()
            {
                //foretak
                new () {NodeId = 01, EnumId = EntityValidatorEnum.ForetakValidator, ParentID = null},
                new () {NodeId = 02, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 01},
                new () {NodeId = 03, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 01},
                new () {NodeId = 04, EnumId = EntityValidatorEnum.KontaktpersonValidatorV2, ParentID = 01},
            };
            var tree = EntityValidatiorTree.BuildTree(foretakNodeList);

            //*Ansvarsrett
            //**foretak
            _foretakPartstypeValidator = new PartstypeValidator(tree, 02, _codeListService);
            _foretakEnkelAdresseValidator = new EnkelAdresseValidator(tree, 03, _postalCodeService);
            _foretakKontaktpersonValidator = new KontaktpersonValidatorV2(tree, 04);
            var foretakPartstypes = new[] { "Foretak" };
            _foretakValidator = new ForetakValidator(tree, _foretakEnkelAdresseValidator, _foretakKontaktpersonValidator, _foretakPartstypeValidator, _codeListService, foretakPartstypes);

        }

        [Fact]
        public void ForetakTest()
        {
            var result = _foretakValidator.Validate(_form.Ansvarsretts.Foretak);
            var errorMessages = result?.ValidationMessages;
            errorMessages.Should().BeNullOrEmpty();
        }
        [Fact]
        public void Foretak_Organisasjonsnummer_Utfylt()
        {
            //_form.Ansvarsretts.Foretak.Organisasjonsnummer  = "910297937" //Gyldig
            _form.Ansvarsretts.Foretak.Organisasjonsnummer = null;
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Foretak_Organisasjonsnummer_kontrollsiffer()
        {
            _form.Ansvarsretts.Foretak.Organisasjonsnummer = "123456789321654";
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Foretak_Organisasjonsnummer_gyldig()
        {
            _form.Ansvarsretts.Foretak.Organisasjonsnummer = "123456789";
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Foretak_Navn_Utfylt()
        {
            _form.Ansvarsretts.Foretak.Navn = null;
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Foretak_Epost_Utfylt()
        {
            _form.Ansvarsretts.Foretak.Epost = null;
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Foretak_telmob_utfylt_Utfylt()
        {
            _form.Ansvarsretts.Foretak.Mobilnummer = null;
            _form.Ansvarsretts.Foretak.Telefonnummer = null;
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Foretak_telefonnummer_gyldig()
        {
            _form.Ansvarsretts.Foretak.Telefonnummer = "-4445sdf";
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Foretak_mobilnummer_gyldig()
        {
            _form.Ansvarsretts.Foretak.Mobilnummer = "-4445sdf";
            //_foretakValidator.ValidateEntityFields(_form.Ansvarsretts.Foretak);
            var errorMessages = _foretakValidator?.ValidationResult?.ValidationMessages;
            errorMessages.Should().NotBeNullOrEmpty();
        }

    }
}
