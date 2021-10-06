using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class FormaalTypeValidatorTests
    {
        private readonly FormaaltypeValidationEntity _beskrivelseAvTiltak;
        private  FormaaltypeValidator _formaaltypeValidator;
        private IKodelisteValidator _tiltaksformaalValidator;
        private readonly IKodelisteValidator _anleggstypeValidator;
        private readonly IKodelisteValidator _naeringsgruppeValidator;
        private readonly IKodelisteValidator _bygningstypeValidator;
        private readonly IList<EntityValidatorNode> _tree;


        public FormaalTypeValidatorTests()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);
            _beskrivelseAvTiltak = form.BeskrivelseAvTiltak.Formaaltype;

            var beskrivelseAvTiltakNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.FormaaltypeValidator, ParentID = null},
            };
            _tree = EntityValidatiorTree.BuildTree(beskrivelseAvTiltakNodeList);


            _anleggstypeValidator = MockDataSource.KodelisteValidator();
            _naeringsgruppeValidator = MockDataSource.KodelisteValidator();
            _bygningstypeValidator = MockDataSource.KodelisteValidator();
            _tiltaksformaalValidator = MockDataSource.KodelisteValidator();

            _formaaltypeValidator = new FormaaltypeValidator(_tree, _anleggstypeValidator, _naeringsgruppeValidator, _bygningstypeValidator, _tiltaksformaalValidator);
            
        }

        [Fact]
        public void RuleCountTest()
        {
            _formaaltypeValidator.ValidateEntityFields(_beskrivelseAvTiltak);
            _formaaltypeValidator.ValidationResult.ValidationRules.Count.Should().Be(2);
        }
        [Fact]
        public void Annet_Test()
        {
            var xpath = @"/beskrivelseAvTiltak/bruk/tiltaksformaal{0}/kodeverdi";
            var entityXPath = "/beskrivelseAvTiltak/bruk/tiltaksformaal{0}";

            _tiltaksformaalValidator = MockDataSource.KodelisteValidator(xpath,ValidationRuleEnum.gyldig, entityXPath);

            _beskrivelseAvTiltak.BeskrivPlanlagtFormaal = null;
            _formaaltypeValidator = new FormaaltypeValidator(_tree, _anleggstypeValidator, _naeringsgruppeValidator, _bygningstypeValidator, _tiltaksformaalValidator);

            var result = _formaaltypeValidator.Validate(_beskrivelseAvTiltak);
            result.ValidationMessages.Count.Should().Be(3);
        }
        [Fact]
        public void Annet_Test_Error()
        {

            _beskrivelseAvTiltak.Tiltaksformaal[1].Kodeverdi = "Annet";
            _beskrivelseAvTiltak.BeskrivPlanlagtFormaal = null;
            var result = _formaaltypeValidator.Validate(_beskrivelseAvTiltak);
            result.ValidationMessages?.FirstOrDefault()?.Rule.Should().Be(ValidationRuleEnum.utfylt.ToString());
        }

    }
}
