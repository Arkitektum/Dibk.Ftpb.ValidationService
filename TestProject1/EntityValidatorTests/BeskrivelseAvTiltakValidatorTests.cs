using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class BeskrivelseAvTiltakValidatorTests
    {

        private FormValidatorConfiguration _formValidatorConfiguration;
        private ICodeListService _codeListService;

        private ArbeidstilsynetsSamtykkeType _form;
        private BeskrivelseAvTiltakValidator _beskrivelseAvTiltakValidator;

        private IList<EntityValidatorNode> _tree;
        private IKodelisteValidator _tiltakstypeValidator;
        private IFormaaltypeValidator _formaaltypeValidator;

        public BeskrivelseAvTiltakValidatorTests()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);

            //Tree
            var flatList = new List<EntityValidatorNode>()
            {
                new ()
                {
                    NodeId = 1,
                    EnumId = EntityValidatorEnum.BeskrivelseAvTiltakValidator,
                    ParentID = null,
                } //root node

            };

            _tree = EntityValidatiorTree.BuildTree(flatList);

            _formaaltypeValidator = MockDataSource.formaaltypeValidator();
            _tiltakstypeValidator = MockDataSource.KodelisteValidator();


            _beskrivelseAvTiltakValidator = new BeskrivelseAvTiltakValidator(_tree, _formaaltypeValidator, _tiltakstypeValidator);

        }

        [Fact]
        public void testBeskrivelseAvTiltak()
        {
            var formEntity = new ArbeidstilsynetsSamtykke2_45957_Mapper().GetFormEntity(_form);

            _beskrivelseAvTiltakValidator.ValidateEntityFields(formEntity.BeskrivelseAvTiltakValidationEntity);
            _beskrivelseAvTiltakValidator.ValidationResult.ValidationRules.Count.Should().Be(2);

        }

        [Fact]
        public void testBeskrivelseAvTiltak_GetTypes()
        {
            var formEntity = new ArbeidstilsynetsSamtykke2_45957_Mapper().GetFormEntity(_form);

            var result = _beskrivelseAvTiltakValidator.Validate(formEntity.BeskrivelseAvTiltakValidationEntity);
            var noko = _beskrivelseAvTiltakValidator.Tiltakstypes;
            _beskrivelseAvTiltakValidator.ValidationResult.ValidationRules.Count.Should().Be(2);

        }
        [Fact]
        public void testBeskrivelseAvTiltak_GetTypes_Error()
        {
            var formEntity = new ArbeidstilsynetsSamtykke2_45957_Mapper().GetFormEntity(_form);

            var xpath = "/beskrivelseAvTiltak{0}/type{0}";

            _tiltakstypeValidator = MockDataSource.KodelisteValidator($"{xpath}/{FieldNameEnum.kodeverdi}",ValidationRuleEnum.validert);
            _beskrivelseAvTiltakValidator = new BeskrivelseAvTiltakValidator(_tree, _formaaltypeValidator, _tiltakstypeValidator);
            
            var result = _beskrivelseAvTiltakValidator.Validate(formEntity.BeskrivelseAvTiltakValidationEntity);
            var noko = _beskrivelseAvTiltakValidator.Tiltakstypes;
            _beskrivelseAvTiltakValidator.ValidationResult.ValidationRules.Count.Should().Be(2);

        }
    }
}
