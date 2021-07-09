using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class KodelisteValidatorTests
    {
        private KodelisteValidationEntity _kodelisteValidationEntity;
        private KodelisteValidator _kodelisteValidator;
        private ICodeListService _codeListService;

        public KodelisteValidatorTests()
        {
            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);
            Kodeliste kodeliste = new Kodeliste()
            {
                Kodeverdi = "kodeverdi Value",
                Kodebeskrivelse = "kodebeskrivelse value"
            };
            _kodelisteValidationEntity = new KodelisteValidationEntity(kodeliste, "unitTest");

            var entityValidatorNodes = new List<EntityValidatorNode>()
            {
                new()
                {
                    NodeId = 1,
                    EnumId = EntityValidatorEnum.PartstypeValidator,
                    ParentID = null,

                }, //root node
            };
            var entityValidationTree = EntityValidatiorTree.BuildTree(entityValidatorNodes);
            _kodelisteValidator = new PartstypeValidator(entityValidationTree, 1, _codeListService);

        }
        [Fact(DisplayName = "kodelist utfylt - Error")]
        public void KodelistyeUtfylt()
        {
            _kodelisteValidationEntity.ModelData = null;
            var result = _kodelisteValidator.Validate(_kodelisteValidationEntity);
            result.ValidationMessages.Count.Should().Be(1);
        }
    }
}
