using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class TiltakshaverValidatorTests
    {
        private Aktoer _tiltakshaver;
        public TiltakshaverValidatorTests()
        {
            _tiltakshaver = new Aktoer("Tiltakshaver")
            {
                Navn = "benjamin",
                Epost = "epost@benjamin.no",
                Mobilnummer = "123456789",
                Telefonnummer = "98765432",
                Foedselsnummer = "08021612345",
                Organisasjonsnummer = "910065211"
            };
            _tiltakshaver.Adresse = new EnkelAdresse("Adresse", _tiltakshaver)
            {
                Adresselinje1 = "bøgate 123",
                Adresselinje2 = "adress 2",
                Adresselinje3 = "adress 3",
                Landkode = "NO",
                Postnr = "3801",
                Poststed = "Bø i telemark"
            };
            _tiltakshaver.Kontaktperson = new Kontaktperson("Kontaktperson", _tiltakshaver)
            {
                Navn = "Señor Presidente",
                Epost = "presi@dente.no",
                Mobilnummer = "98765432",
                Telefonnummer = "55564789"
            };
            _tiltakshaver.Partstype = new PartstypeCode("PartsType", _tiltakshaver)
            {
                Kodeverdi = "Privatperson",
                Kodebeskrivelse = "Privatperson"
            };
        }

        [Fact]
        public void TestTilashaver()
        {
            var codeListService = MockDataSource.IsCodeListValid(FtbCodeListNames.Partstype, true);

            var tiltakshaverValidator = new TiltakshaverValidator("unitest", codeListService);
            _tiltakshaver.Foedselsnummer = "54554";
            var tiltakshaverResult = tiltakshaverValidator.Validate(_tiltakshaver);

            tiltakshaverResult.Should().NotBeNull();
        }
    }
}
