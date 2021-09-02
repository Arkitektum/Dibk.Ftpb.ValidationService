using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class MatrikkelValidatorTests
    {
        private ArbeidstilsynetsSamtykkeType _form;
        private readonly IEnumerable<EiendomValidationEntity> _eiendomValidationEntities;

        IMunicipalityValidator _municipalityValidator;
        
        private FormValidatorConfiguration _formValidatorConfiguration;
        private MatrikkelValidator _validator;
        private string _xmlData;

        public MatrikkelValidatorTests()
        {
            _xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(_xmlData);
            
            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);


            _eiendomValidationEntities = new Logic.Mappers.ArbeidstilsynetsSamtykke2.EiendomByggestedMapper().Map(_form.eiendomByggested, "ArbeidstilsynetsSamtykke");

            _formValidatorConfiguration = new FormValidatorConfiguration();
            _formValidatorConfiguration.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";
            _formValidatorConfiguration.FormXPathRoot = "ArbeidstilsynetsSamtykke";

            var matrikkelNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.MatrikkelValidator, ParentID = null},
            };

            _validator = new MatrikkelValidator(EntityValidatiorTree.BuildTree(matrikkelNodeList), _municipalityValidator);
        }

        [Fact]
        public void testMatrikkel()
        {
            var matrikkelInfo = _eiendomValidationEntities.ToArray();
            var result = _validator.Validate(matrikkelInfo[0].ModelData.Matrikkel);

            var jsonPostamn = TestHelper.GetJsonForPostman(null, _form);

            result.Should().NotBeNull();
        }
    }
}
