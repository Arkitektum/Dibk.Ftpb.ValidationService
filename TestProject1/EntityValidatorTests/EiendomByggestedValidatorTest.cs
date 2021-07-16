using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Municipality;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
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
    public class EiendomByggestedValidatorTest
    {
        private ArbeidstilsynetsSamtykkeType _form;
        private readonly IEnumerable<EiendomValidationEntity> _eiendomValidationEntities;

        IMunicipalityValidator _municipalityValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;

        private MatrikkelValidator _matrikkelValidator;

        private FormValidatorConfiguration _formValidatorConfiguration;
        private EiendombyggestedLogic _eiendombyggestedLogic;


        public EiendomByggestedValidatorTest()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);

            _eiendomValidationEntities = new Logic.Mappers.ArbeidstilsynetsSamtykke2.EiendomByggestedMapper().Map(_form.eiendomByggested, "ArbeidstilsynetsSamtykke");

            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);

            _formValidatorConfiguration = new FormValidatorConfiguration();
            _formValidatorConfiguration.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";
            _formValidatorConfiguration.FormXPathRoot = "ArbeidstilsynetsSamtykke";

            _eiendombyggestedLogic = new EiendombyggestedLogic(1, _municipalityValidator);
            _eiendomByggestedValidator = _eiendombyggestedLogic.Validator;

            _formValidatorConfiguration.ValidatorsTree = _eiendombyggestedLogic.Tree;
        }


        [Fact(Skip = "noko")]
        public void EiendomTest()
        {
            _eiendomValidationEntities.FirstOrDefault().ModelData.Adresse = null;

            var result = _eiendomByggestedValidator.Validate(_eiendomValidationEntities);
            result.Should().NotBeNull();
        }
        [Fact(Skip = "noko")]

        public void TestEiendom()
        {
            var nn = Helpers.GetEnumXmlNodeName(EiendomValidationEnum.utfylt);

            var description = typeof(EiendomValidationEnum)
                .GetField(nameof(EiendomValidationEnum.utfylt))
                .GetCustomAttribute<EnumerationAttribute>(false)
                ?.XmlNode;

            description.Should().NotBeNullOrEmpty();
        }
    }
}
