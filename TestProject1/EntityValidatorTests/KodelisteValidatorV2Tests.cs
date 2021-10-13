using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class KodelisteValidatorV2Tests
    {
        private Kodeliste _kodeliste;
        private KodelisteValidatorV2 _kodelisteValidator;
        private ICodeListService _codeListService;

        public KodelisteValidatorV2Tests()
        {
            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.funksjon, true);

            _kodeliste = new Kodeliste()
            {
                Kodeverdi = "kodeverdi Value",
                Kodebeskrivelse = "kodebeskrivelse value"
            };

            var entityValidatorNodes = new List<EntityValidatorNode>()
            {
                new() {NodeId = 1, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = null,},
            };
            var tree = EntityValidatiorTree.BuildTree(entityValidatorNodes);
            _kodelisteValidator = new KodelisteValidatorV2(tree, 1, FtbKodeListeEnum.funksjon, RegistryType.Byggesoknad,_codeListService);

        }
        [Fact(DisplayName = "kodelist utfylt - Error")]
        public void KodelistyeUtfylt()
        {
            _kodeliste = null;
            var result = _kodelisteValidator.Validate(_kodeliste);
            result.ValidationMessages.Count.Should().Be(1);
        }
    }
}
