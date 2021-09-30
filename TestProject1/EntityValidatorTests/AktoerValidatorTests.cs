using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class AktoerValidatorTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private AktoerValidationEntity _aktoerValidationEntity;

        private ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        private IAktoerValidator _aktoerValidator;

        public AktoerValidatorTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");
            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, false);

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957_Test.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _aktoerValidationEntity = new Logic.Mappers.ArbeidstilsynetsSamtykkeV2.AktoerMapper(AktoerEnum.tiltakshaver).Map(_form.tiltakshaver, "UnitTest");

            var tiltakshaverNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.TiltakshaverValidator, ParentID = null},
                new () {NodeId = 2, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = 1},
                new () {NodeId = 3, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 1},
                new () {NodeId = 4, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 1}
            };
            var tree = EntityValidatiorTree.BuildTree(tiltakshaverNodeList);

            //Tiltakshaver
            IKontaktpersonValidator tiltakshaverKontaktpersonValidator = new KontaktpersonValidator(tree, 2);
            IKodelisteValidator tiltakshaverPartstypeValidator = new PartstypeValidator(tree, 3, _codeListService);
            var tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(tree, 4, _postalCodeService);
            _aktoerValidator = new TiltakshaverValidator(tree, tiltakshaverEnkelAdresseValidator, tiltakshaverKontaktpersonValidator, tiltakshaverPartstypeValidator, _codeListService);


        }
        [Fact]
        public void TestTilashaver()
        {
            //_aktoerValidationEntity.ModelData.Partstype.ModelData = null;

            var tiltakshaverResult = _aktoerValidator.Validate(_aktoerValidationEntity);

            tiltakshaverResult.Should().NotBeNull();
        }
    }
}
