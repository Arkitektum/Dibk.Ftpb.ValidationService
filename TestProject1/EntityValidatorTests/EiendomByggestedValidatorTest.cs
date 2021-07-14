using System.Collections.Generic;
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
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class EiendomByggestedValidatorTest
    {
        private FormValidatorConfiguration _formValidatorConfiguration;
        private IEnumerable<EiendomValidationEntity> _eiendomValidationEntities;
        IMunicipalityValidator _municipalityValidator;
        private EiendomByggestedValidator _eiendomByggestedValidator;
        private EiendomByggestedValidator _eiendomByggestedValidatorTreeTest;
        public EiendomByggestedValidatorTest()
        {
            var eiendoms = new[] {new EiendomType()
            {
                eiendomsidentifikasjon = new MatrikkelnummerType()
                {
                    kommunenummer = "3817",
                    gaardsnummer = "55",
                    bruksnummer = "40",
                    festenummer = "0",
                    seksjonsnummer = "0",
                },
                adresse = new EiendommensAdresseType()
                {
                    gatenavn = "Kyrkjevegen",
                    husnr = "6",
                    adresselinje1 = "Kyrkjevegen 6",
                    adresselinje2 = null,
                    adresselinje3 = null,
                    postnr = "3800",
                    poststed = "Bø i Telemark",
                    landkode = "NO",
                    bokstav = null,
                },
                bolignummer = "H0214",
                kommunenavn = "MIDT-TELEMARK",
                bygningsnummer = "165679733",
            },new EiendomType()
            {
                eiendomsidentifikasjon = new MatrikkelnummerType()
                {
                    kommunenummer = "3817",
                    gaardsnummer = "55",
                    bruksnummer = "40",
                    festenummer = "0",
                    seksjonsnummer = "0",
                },
                adresse = new EiendommensAdresseType()
                {
                    gatenavn = "Kyrkjevegen",
                    husnr = "6",
                    adresselinje1 = "Kyrkjevegen 6",
                    adresselinje2 = null,
                    adresselinje3 = null,
                    postnr = "3800",
                    poststed = "Bø i Telemark",
                    landkode = "NO",
                    bokstav = null,
                },
                bolignummer = "H0214",
                kommunenavn = "MIDT-TELEMARK",
                bygningsnummer = "165679733",
            },new EiendomType()
            {
                eiendomsidentifikasjon = new MatrikkelnummerType()
                {
                    kommunenummer = "3817",
                    gaardsnummer = "55",
                    bruksnummer = "40",
                    festenummer = "0",
                    seksjonsnummer = "0",
                },
                adresse = new EiendommensAdresseType()
                {
                    gatenavn = "Kyrkjevegen",
                    husnr = "6",
                    adresselinje1 = "Kyrkjevegen 6",
                    adresselinje2 = null,
                    adresselinje3 = null,
                    postnr = "3800",
                    poststed = "Bø i Telemark",
                    landkode = "NO",
                    bokstav = null,
                },
                bolignummer = "H0214",
                kommunenavn = "MIDT-TELEMARK",
                bygningsnummer = "165679733",
            },new EiendomType()
            {
                eiendomsidentifikasjon = new MatrikkelnummerType()
                {
                    kommunenummer = "3817",
                    gaardsnummer = "55",
                    bruksnummer = "40",
                    festenummer = "0",
                    seksjonsnummer = "0",
                },
                adresse = new EiendommensAdresseType()
                {
                    gatenavn = "Kyrkjevegen",
                    husnr = "6",
                    adresselinje1 = "Kyrkjevegen 6",
                    adresselinje2 = null,
                    adresselinje3 = null,
                    postnr = "3800",
                    poststed = "Bø i Telemark",
                    landkode = "NO",
                    bokstav = null,
                },
                bolignummer = "H0214",
                kommunenavn = "MIDT-TELEMARK",
                bygningsnummer = "165679733",
            },

            };

            _eiendomValidationEntities = new Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2.EiendomByggestedMapper().Map(eiendoms, "ArbeidstilsynetsSamtykke");

            _formValidatorConfiguration = new FormValidatorConfiguration();
            _formValidatorConfiguration.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";
            _formValidatorConfiguration.FormXPathRoot = "ArbeidstilsynetsSamtykke";
            _formValidatorConfiguration.Validators = new List<EntityValidatorInfo>();
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomByggestedValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomsAdresseValidator, EntityValidatorEnum.EiendomByggestedValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.MatrikkelValidator, EntityValidatorEnum.EiendomByggestedValidator));

            //IEiendomsAdresseValidator eiendomsAdresseValidator = new EiendomsAdresseValidator(_formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);
            //IMatrikkelValidator matrikkelValidator = new MatrikkelValidator(_formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);
            //_municipalityValidator = MockDataSource.MunicipalityValidatorResult(MunicipalityValidationEnum.Ok);

            //_eiendomByggestedValidator = new EiendomByggestedValidator(_formValidatorConfiguration, eiendomsAdresseValidator, matrikkelValidator, _municipalityValidator);


            var flatList = new List<EntityValidatorNode>()
            {
                new ()
                {
                    NodeId = 1,
                    EnumId = EntityValidatorEnum.EiendomByggestedValidator,
                    ParentID = null,
                   
                }, //root node
                new ()
                {
                    NodeId = 2,
                    EnumId = EntityValidatorEnum.EiendomsAdresseValidator,
                    ParentID = 1,
                   
                },
                new ()
                {
                    NodeId = 3,
                    EnumId = EntityValidatorEnum.MatrikkelValidator,
                    ParentID = 1,
                   
                }
            };
            var tree = EntityValidatiorTree.BuildTree(flatList);
            _formValidatorConfiguration.ValidatorsTree = tree;
           // _eiendomByggestedValidatorTreeTest = new EiendomByggestedValidator(tree, eiendomsAdresseValidator, matrikkelValidator, _municipalityValidator);
        }


        [Fact]
        public void EiendomTest()
        {
            _eiendomValidationEntities.First().ModelData.Adresse = null;
            var eiendomByggestedTreeResult = _eiendomByggestedValidatorTreeTest.Validate(_eiendomValidationEntities);
            //var eiendomByggested = _eiendomByggestedValidator.Validate(_eiendomValidationEntities);
            eiendomByggestedTreeResult.Should().NotBeNull();
        }
        [Fact]
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
