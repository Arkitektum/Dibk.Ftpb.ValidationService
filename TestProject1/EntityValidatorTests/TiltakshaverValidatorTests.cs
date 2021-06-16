using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class TiltakshaverValidatorTests
    {
        private AktoerValidationEntity _tiltakshaver;
        private FormValidatorConfiguration _formValidatorConfiguration;
        private ICodeListService _codeListService;
        private AktoerValidator _aktoerValidator;
        public TiltakshaverValidatorTests()
        {
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

            _tiltakshaver = new Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2.AktoerMapper(AktoerEnum.tiltakshaver).Map(tiltakshaver, "ArbeidstilsynetsSamtykke");
            _codeListService = MockDataSource.IsCodeListValid(FtbCodeListNames.Partstype, true);
            _formValidatorConfiguration = new FormValidatorConfiguration();
            _formValidatorConfiguration.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";
            _formValidatorConfiguration.FormXPathRoot = "ArbeidstilsynetsSamtykke";
            _formValidatorConfiguration.Validators = new List<EntityValidatorInfo>();
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltakshaverValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.KontaktpersonValidator, EntityValidatorEnum.TiltakshaverValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.PartstypeValidator, EntityValidatorEnum.TiltakshaverValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.TiltakshaverValidator));

            IEnkelAdresseValidator enkelAdresseValidator = new EnkelAdresseValidator(_formValidatorConfiguration, EntityValidatorEnum.TiltakshaverValidator);
            IKontaktpersonValidator kontaktpersonValidator = new KontaktpersonValidator(_formValidatorConfiguration, EntityValidatorEnum.TiltakshaverValidator);
            IKodelisteValidator partstypeValidator = new PartstypeValidator(_formValidatorConfiguration, EntityValidatorEnum.TiltakshaverValidator, _codeListService);

            _aktoerValidator = new AktoerValidator(_formValidatorConfiguration, AktoerEnum.tiltakshaver, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, _codeListService);

        }

        [Fact]
        public void TestTilashaver()
        {

            //_tiltakshaver.ModelData.Partstype.ModelData.Kodeverdi = "54554";
            _tiltakshaver.ModelData.Mobilnummer = null;
            _tiltakshaver.ModelData.Telefonnummer = null;

            var tiltakshaverResult = _aktoerValidator.Validate(_tiltakshaver);
            tiltakshaverResult.Should().NotBeNull();
        }
    }
}
