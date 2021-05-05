using Dibk.Ftpb.Validation.Application.Logic.Mappers;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;
using FluentAssertions;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykkeV2Dfv45957_MapperTests
    {
        [Fact]
        public void Test1()
        {
            var dataForm = new ArbeidstilsynetsSamtykkeType()
            {
                eiendomByggested = new EiendomType()
                {
                    adresse = new EiendommensAdresseType()
                    {
                        adresselinje1 = "Bø gate 31A",
                        bokstav = "A",
                        husnr = "31",
                        gatenavn = "Bø gate"

                    },
                    bolignummer = "12345",
                    kommunenavn = "Bø i Telemark"
                }
            };
            var dataFomr = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataForm);

            dataFomr.Eiendom.Should().NotBeNull();
        }
    }
}
