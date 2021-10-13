using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class KontaktpersonValidatorV2Tests
    {
        private readonly KontaktpersonValidatorV2 _foretakKontaktpersonValidator;
        private AnsvarsrettAnsako_ANSAKO_10000_Form _form;

        public KontaktpersonValidatorV2Tests()
        {
            var xmlData = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");
            _form = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(xmlData);
            var foretakNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 01, EnumId = EntityValidatorEnum.KontaktpersonValidatorV2, ParentID = null},
            };
            var tree = EntityValidatiorTree.BuildTree(foretakNodeList);


            _foretakKontaktpersonValidator = new KontaktpersonValidatorV2(tree,01);


        }
        [Fact]
        public void KontaktpersonRule_Count()
        {
            var result = _foretakKontaktpersonValidator.Validate(_form.Ansvarsretts.Foretak.Kontaktperson);
            var rules = result?.ValidationRules;
            rules.Count.Should().Be(6);
        }
        [Fact]
        public void Kontaktperson_navn_utfylt()
        {
            _form.Ansvarsretts.Foretak.Kontaktperson.Navn = null;
            var result = _foretakKontaktpersonValidator.Validate(_form.Ansvarsretts.Foretak.Kontaktperson);
            var validationMessages = result?.ValidationMessages;
            validationMessages?.Count.Should().Be(1);
        }
        [Fact]
        public void Kontaktperson_telmob_utfylt_Utfylt()
        {
            _form.Ansvarsretts.Foretak.Kontaktperson.Mobilnummer = null;
            _form.Ansvarsretts.Foretak.Kontaktperson.Telefonnummer = null;
            var result = _foretakKontaktpersonValidator.Validate(_form.Ansvarsretts.Foretak.Kontaktperson);
            var validationMessages = result?.ValidationMessages;
            validationMessages?.Count.Should().Be(1);
        }
        [Fact]
        public void Kontaktperson_telefonnummer_gyldig()
        {
            _form.Ansvarsretts.Foretak.Kontaktperson.Telefonnummer = "-4445sdf";
            var result = _foretakKontaktpersonValidator.Validate(_form.Ansvarsretts.Foretak.Kontaktperson);
            var validationMessages = result?.ValidationMessages;
            validationMessages?.Count.Should().Be(1);
        }
        [Fact]
        public void Kontaktperson_mobilnummer_gyldig()
        {
            _form.Ansvarsretts.Foretak.Kontaktperson.Mobilnummer = "-4445sdf";
            var result = _foretakKontaktpersonValidator.Validate(_form.Ansvarsretts.Foretak.Kontaktperson);
            var validationMessages = result?.ValidationMessages;
            validationMessages?.Count.Should().Be(1);
        }
        [Fact]
        public void Kontaktperson_epost_utfylt()
        {
            _form.Ansvarsretts.Foretak.Kontaktperson.Epost = null;
            var result = _foretakKontaktpersonValidator.Validate(_form.Ansvarsretts.Foretak.Kontaktperson);
            var validationMessages = result?.ValidationMessages;
            validationMessages?.Count.Should().Be(1);
        }
    }
}
