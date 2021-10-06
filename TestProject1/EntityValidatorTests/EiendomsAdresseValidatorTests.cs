using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class EiendomsAdresseValidatorTests
    {
        private readonly EiendomValidationEntity[] _eiendomValidationEntities;

        IMunicipalityValidator _municipalityValidator;

        private FormValidatorConfiguration _formValidatorConfiguration;
        private EiendomsAdresseValidator _validator;

        public EiendomsAdresseValidatorTests()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);

            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);

            _eiendomValidationEntities = form.EiendomByggested;

            _formValidatorConfiguration = new FormValidatorConfiguration();
            _formValidatorConfiguration.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";
            _formValidatorConfiguration.FormXPathRoot = "ArbeidstilsynetsSamtykke";

            var validatorNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.EiendomsAdresseValidator, ParentID = null},
            };

            _validator = new EiendomsAdresseValidator(EntityValidatiorTree.BuildTree(validatorNodeList), 1);
        }

        [Fact]
        public void testMatrikkel()
        {
            var matrikkelInfo = _eiendomValidationEntities.ToArray();
            var result = _validator.Validate(matrikkelInfo[0].Adresse);

            result.Should().NotBeNull();
        }

    }
}
