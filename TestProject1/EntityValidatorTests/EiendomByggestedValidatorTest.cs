using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Dibk.Ftpb.Validation.Application.DataSources;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class EiendomByggestedValidatorTest
    {
        private Eiendom[] _eiendomValidationEntities;
        IMunicipalityValidator _municipalityValidator;


        private FormValidatorConfiguration _formValidatorConfiguration;
        
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private EiendomByggestedValidator _eiendomByggestedValidator;




        public EiendomByggestedValidatorTest()
        {
            // Test data shoud be 
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957_Test.xml");
            //var xmlData = File.ReadAllText(@"C:\Temp\FTB - TestXML\ArbeidstilsynetsSamtykke_v2_empty.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);

            _eiendomValidationEntities = form.EiendomByggested;


            List<EntityValidatorNode> eiendombyggestedNodeList = new()
            {
                new() { NodeId = 1, EnumId = EntityValidatorEnum.EiendomByggestedValidator, ParentID = null },
                //new() { NodeId = 2, EnumId = EntityValidatorEnum.EiendomsAdresseValidator, ParentID = 1 },
                //new() { NodeId = 3, EnumId = EntityValidatorEnum.MatrikkelValidator, ParentID = 1 },
            };
            var tree = EntityValidatiorTree.BuildTree(eiendombyggestedNodeList);


            _municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);

            _eiendomsAdresseValidator = MockDataSource.EiendomsAdresseValidator();
            _matrikkelValidator = MockDataSource.MatrikkelValidator();
            _eiendomByggestedValidator = new EiendomByggestedValidator(tree, _eiendomsAdresseValidator, _matrikkelValidator);


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

            _eiendomValidationEntities[0].Bygningsnummer = "nokoRara";


            var result = _eiendomByggestedValidator.Validate(_eiendomValidationEntities.FirstOrDefault());

            var ValidationMessages = result.ValidationMessages
                .Select(r => string.Concat("Rule: ", r.Rule, "- xpath: ", r.XpathField)).ToArray();

            var messageErrors = JArray.Parse(JsonConvert.SerializeObject(ValidationMessages));

            result.Should().NotBeNull();
        }
        [Fact(Skip = "noko")]

        public void TestEiendom()
        {
            var nn = EnumHelper.GetEnumXmlNodeName(ValidationRuleEnum.utfylt);

           

            var description = typeof(ValidationRuleEnum)
                .GetField(nameof(ValidationRuleEnum.utfylt))
                .GetCustomAttribute<EntityValidatorEnumerationAttribute>(false)
                ?.XmlNode;

            description.Should().NotBeNullOrEmpty();
        }
    }
}
