using System.Collections.Generic;
using System.IO;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class MatrikkelValidatorV2Tests
    {
        private MatrikkelValidatorV2 _validator;
        private Matrikkel _matrikkel;

        private ICodeListService _codeListService;
        private readonly IList<EntityValidatorNode> _tree;


        public MatrikkelValidatorV2Tests()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);
            _matrikkel = form.EiendomByggested[0].Matrikkel;


          var matrikkelNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.MatrikkelValidatorV2, ParentID = null},
            };

            _tree = EntityValidatiorTree.BuildTree(matrikkelNodeList);
            _codeListService = MockDataSource.GetCodelistTagValue(SosiKodelisterEnum.kommunenummer);
            _validator = new MatrikkelValidatorV2(_tree, _codeListService);
        }

        [Fact]
        public void testMatrikkel()
        {
            _codeListService = MockDataSource.GetCodelistTagValue(SosiKodelisterEnum.kommunenummer,"3817", "Midt-Telemark","dgrt");
            _validator = new MatrikkelValidatorV2(_tree, _codeListService);
            var result = _validator.Validate(_matrikkel);
            result.Should().NotBeNull();
        }
    }
}
