﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Municipality;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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


        public EiendomByggestedValidatorTest()
        {
            // Test data shoud be 
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957_Test.xml");
            //var xmlData = File.ReadAllText(@"C:\Temp\FTB - TestXML\ArbeidstilsynetsSamtykke_v2_empty.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);

            _eiendomValidationEntities = new Logic.Mappers.ArbeidstilsynetsSamtykke2.EiendomByggestedMapper().Map(_form.eiendomByggested, "ArbeidstilsynetsSamtykke");

            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);

            _formValidatorConfiguration = new FormValidatorConfiguration();
            _formValidatorConfiguration.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";
            _formValidatorConfiguration.FormXPathRoot = "ArbeidstilsynetsSamtykke";

        }


        [Fact]
        public void EiendomTest()
        {
            
            //_eiendomValidationEntities.FirstOrDefault().ModelData.Matrikkel.ModelData.Kommunenummer = "";
            //_eiendomValidationEntities.FirstOrDefault().ModelData.Matrikkel.ModelData.Gaardsnummer = "";
            //_eiendomValidationEntities.FirstOrDefault().ModelData.Matrikkel.ModelData.Bruksnummer = "";
            //_eiendomValidationEntities.FirstOrDefault().ModelData.Matrikkel.ModelData.Festenummer = "";
            //_eiendomValidationEntities.FirstOrDefault().ModelData.Matrikkel.ModelData.Seksjonsnummer = "";


            var result = _eiendomByggestedValidator.Validate(_eiendomValidationEntities);
            var ValidationMessages = result.ValidationMessages
                .Select(r => string.Concat("Rule: ", r.Rule, "- xpath: ", r.XpathField)).ToArray();

            var messageErrors = JArray.Parse(JsonConvert.SerializeObject(ValidationMessages));

            result.Should().NotBeNull();
        }
        [Fact(Skip = "noko")]

        public void TestEiendom()
        {
            var nn = Helpers.GetEnumXmlNodeName(ValidationRuleEnum.utfylt);

            var description = typeof(ValidationRuleEnum)
                .GetField(nameof(ValidationRuleEnum.utfylt))
                .GetCustomAttribute<EntityValidatorEnumerationAttribute>(false)
                ?.XmlNode;

            description.Should().NotBeNullOrEmpty();
        }
    }
}
