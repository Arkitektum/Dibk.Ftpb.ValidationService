using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests.EntityValidatorLogic
{
   public class EiendombyggestesLogicTests
    {
        IMunicipalityValidator _municipalityValidator;
        private EiendombyggestedValidatorLogic _eiendombyggestedLogic;

        public EiendombyggestesLogicTests()
        {
            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);
            _eiendombyggestedLogic = new EiendombyggestedValidatorLogic(1, _municipalityValidator);

        }
        [Fact]
        public void testTree()
        {

            var eiendoNodeList = _eiendombyggestedLogic.NodeList;
            var eiendomTree = _eiendombyggestedLogic.Tree;
            var classEiendom = _eiendombyggestedLogic.Validator;
            var rulesValidator = _eiendombyggestedLogic.ValidationRules;
            eiendomTree.Count.Should().Be(1);

        }
    }
}
