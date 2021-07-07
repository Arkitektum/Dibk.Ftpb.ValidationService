using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using FluentAssertions;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class TiltakshaverValidatorTests
    {
        private AktoerValidationEntity _tiltakshaver;

        private ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        private AktoerValidator _aktoerValidator;
        private AktoerValidator _aktoerValidatorTest;
        public TiltakshaverValidatorTests()
        {
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i telemark", "true");

            var tiltakshaver = new no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.PartType()
            {
                adresse = new no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EnkelAdresseType()
                {
                    adresselinje1 = "bøgate 123",
                    adresselinje2 = "adress 2",
                    adresselinje3 = "adress 3",
                    landkode = "NO",
                    postnr = "3801",
                    poststed = "Bø i telemark"
                },
                navn = "benjamin",
                epost = "epost@benjamin.no",
                mobilnummer = "123456789",
                telefonnummer = "98765432",
                foedselsnummer = "08021612345",
                organisasjonsnummer = "910065211",
                kontaktperson = new no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.KontaktpersonType()
                {
                    navn = "Señor Presidente",
                    epost = "presi@dente.no",
                    mobilnummer = "98765432",
                    telefonnummer = "55564789"
                },
                partstype = new no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.KodeType()
                {
                    kodeverdi = "Privatperson",
                    kodebeskrivelse = "Privatperson"
                }
            };

            _tiltakshaver = new Logic.Mappers.ArbeidstilsynetsSamtykke2.AktoerMapper(AktoerEnum.tiltakshaver).Map(tiltakshaver, "UnitTest");
            _codeListService = MockDataSource.IsCodeListValid(FtbCodeListNames.partstype, true);


            var entityValidatorNodes = new List<EntityValidatorNode>()
            {
                new ()
                {
                    Id = 1,
                    EnumId = EntityValidatorEnum.TiltakshaverValidator,
                    ParentID = null,

                }, //root node
                new ()
                {
                    Id = 2,
                    EnumId = EntityValidatorEnum.KontaktpersonValidator,
                    ParentID = 1,
                    
                },
                new ()
                {
                    Id = 3,
                    EnumId = EntityValidatorEnum.PartstypeValidator,
                    ParentID = 1,
                },
                new ()
                {
                    Id = 4,
                    EnumId = EntityValidatorEnum.EnkelAdresseValidator,
                    ParentID = 1,
                },
            };

            var entityValidationTree = EntityValidatiorTree.BuildTree(entityValidatorNodes);

            var partstypeValidator = new PartstypeValidator(entityValidationTree, 1, _codeListService);
            IEnkelAdresseValidator enkelAdresseValidator = new EnkelAdresseValidator(entityValidationTree,4,_postalCodeService);
            IKontaktpersonValidator kontaktpersonValidator = new KontaktpersonValidator(entityValidationTree,2);

            _aktoerValidatorTest = new TiltakshaverValidator(entityValidationTree, 1, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, _codeListService);
        }

        [Fact]
        public void TestTilashaver()
        {
            //_tiltakshaver.ModelData.Mobilnummer = null;
            //_tiltakshaver.ModelData.Telefonnummer = null;
            _tiltakshaver.ModelData.Partstype.ModelData = null;

            var tiltakshaverResult = _aktoerValidatorTest.Validate(_tiltakshaver);

            tiltakshaverResult.Should().NotBeNull();
        }
    }
}
