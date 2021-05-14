using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class TiltakshaverValidatorTests
    {
        private Aktoer _tiltakshaver;
        public TiltakshaverValidatorTests()
        {
            _tiltakshaver = new Aktoer()
            {
                Navn = "benjamin",
                Epost = "epost@benjamin.no",
                Mobilnummer = "123456789",
                Telefonnummer = "98765432",
                Foedselsnummer = "08021612345",
                Organisasjonsnummer = "910065211",
                Adresse = new EnkelAdresse()
                {
                    Adresselinje1 = "bøgate 123",
                    Adresselinje2 = "adress 2",
                    Adresselinje3 = "adress 3",
                    Landkode = "NO",
                    Postnr = "3801",
                    Poststed = "Bø i telemark"
                },
                Kontaktperson = new Kontaktperson()
                {
                    Navn = "Señor Presidente",
                    Epost = "presi@dente.no",
                    Mobilnummer = "98765432",
                    Telefonnummer = "55564789"
                },
                Partstype = new PartstypeCode()
                {
                    Kodeverdi = "Privatperson",
                    kodebeskrivelse = "Privatperson"
                }
            };
        }

        [Fact]
        public void TestTilashaver()
        {
            var tiltakshaverValidator = new TiltakshaverValidator("unitest");
            _tiltakshaver.Foedselsnummer = "54554";
            var tiltakshaverResult = tiltakshaverValidator.Validate(null, _tiltakshaver);

            tiltakshaverResult.Should().NotBeNull();
        }
    }
}
