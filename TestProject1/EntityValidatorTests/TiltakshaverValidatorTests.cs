using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class TiltakshaverValidatorTests
    {
        private AktoerValidationEntity _tiltakshaver;
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

            _tiltakshaver = new Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2.TiltakshaverMapper().Map(tiltakshaver);

            //_tiltakshaver = new Aktoer("Tiltakshaver")
            //{
            //    Navn = "benjamin",
            //    Epost = "epost@benjamin.no",
            //    Mobilnummer = "123456789",
            //    Telefonnummer = "98765432",
            //    Foedselsnummer = "08021612345",
            //    Organisasjonsnummer = "910065211"
            //};
            //_tiltakshaver.Adresse = new EnkelAdresse("Adresse", _tiltakshaver)
            //{
            //    Adresselinje1 = "bøgate 123",
            //    Adresselinje2 = "adress 2",
            //    Adresselinje3 = "adress 3",
            //    Landkode = "NO",
            //    Postnr = "3801",
            //    Poststed = "Bø i telemark"
            //};
            //_tiltakshaver.Kontaktperson = new Kontaktperson("Kontaktperson", _tiltakshaver)
            //{
            //    Navn = "Señor Presidente",
            //    Epost = "presi@dente.no",
            //    Mobilnummer = "98765432",
            //    Telefonnummer = "55564789"
            //};
            //_tiltakshaver.Partstype = new PartstypeCode("PartsType", _tiltakshaver)
            //{
            //    Kodeverdi = "Privatperson",
            //    Kodebeskrivelse = "Privatperson"
            //};
        }

        [Fact]
        public void TestTilashaver()
        {
            var codeListService = MockDataSource.IsCodeListValid(FtbCodeListNames.Partstype, true);
            EntityValidatorOrchestrator entityValidatorOrchestrator = new EntityValidatorOrchestrator();
            IEnkelAdresseValidator enkelAdresseValidator = new EnkelAdresseValidator(entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator);
            IKontaktpersonValidator kontaktpersonValidator = new KontaktpersonValidator(entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator);
            IPartstypeValidator partstypeValidator = new PartstypeValidator(entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator, codeListService);

            var tiltakshaverValidator = new TiltakshaverValidator(entityValidatorOrchestrator, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, codeListService);
            _tiltakshaver.ModelData.Foedselsnummer = "54554";
            var tiltakshaverResult = tiltakshaverValidator.Validate(_tiltakshaver);

            tiltakshaverResult.Should().NotBeNull();
        }
    }
}
