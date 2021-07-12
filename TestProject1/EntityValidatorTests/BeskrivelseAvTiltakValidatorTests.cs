using System.Collections.Generic;
using System.IO;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
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
        public BeskrivelseAvTiltakValidatorTests()
        {
            var xmlData = File.ReadAllText(@"ArbeidstilsynetsSamtykke_v2_dfv45957_Test");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);

            _formValidatorConfiguration = new FormValidatorConfiguration();
            _formValidatorConfiguration.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";
            _formValidatorConfiguration.FormXPathRoot = "ArbeidstilsynetsSamtykke";
            _formValidatorConfiguration.Validators = new List<EntityValidatorInfo>();

            //BeskrivelseAvTiltak
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.AnleggstypeValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.NaeringsgruppeValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.BygningstypeValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltaksformaalValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltakstypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));

            //Tree
            var flatList = new List<EntityValidatorNode>()
            {
                new ()
                {
                    NodeId = 1,
                    EnumId = EntityValidatorEnum.BeskrivelseAvTiltakValidator,
                    ParentID = null,
                }, //root node
                new ()
                {
                    NodeId = 2,
                    EnumId = EntityValidatorEnum.FormaaltypeValidator,
                    ParentID = 1,
                },
                new ()
                {
                    NodeId = 3,
                    EnumId = EntityValidatorEnum.AnleggstypeValidator,
                    ParentID = 2,
                },
                new ()
                {
                    NodeId = 4,
                    EnumId = EntityValidatorEnum.NaeringsgruppeValidator,
                    ParentID = 2,
                },
                new ()
                {
                    NodeId = 5,
                    EnumId = EntityValidatorEnum.BygningstypeValidator,
                    ParentID = 2,
                },
                new ()
                {
                    NodeId = 6,
                    EnumId = EntityValidatorEnum.TiltaksformaalValidator,
                    ParentID = 2,
                },
                new ()
                {
                    NodeId = 7,
                    EnumId = EntityValidatorEnum.TiltakstypeValidator,
                    ParentID = 1,
                }
            };

            _tree = EntityValidatiorTree.BuildTree(flatList);

            ICodeListService codeListServiceMock = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, false);

            AnleggstypeValidator anleggstypeValidator = new AnleggstypeValidator(_tree, 3, codeListServiceMock);
            NaeringsgruppeValidator naeringsgruppeValidator = new NaeringsgruppeValidator(_tree, 4, codeListServiceMock);
            BygningstypeValidator bygningstypeValidator = new BygningstypeValidator(_tree, 5, codeListServiceMock);

            FormaaltypeValidator formaaltypeValidator = new FormaaltypeValidator(_tree, 2, anleggstypeValidator, naeringsgruppeValidator, bygningstypeValidator);

            ICodeListService tiltaksformaalCodeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);
            TiltakstypeValidator tiltakstypeValidator = new TiltakstypeValidator(_tree, 7, tiltaksformaalCodeListService);

            _beskrivelseAvTiltakValidator = new BeskrivelseAvTiltakValidator(_tree, 1, formaaltypeValidator, tiltakstypeValidator);

        }

        [Fact]
        public void testBeskrivelseAvTiltak()
        {

            var formEntity = new ArbeidstilsynetsSamtykke2_45957_Mapper().GetFormEntity(_form);

            var beskrivelseValidationResult = _beskrivelseAvTiltakValidator.Validate(formEntity.ModelData.BeskrivelseAvTiltakValidationEntity);
            beskrivelseValidationResult.Should().NotBeNull();

        }
    }
}
