using Dibk.Ftpb.Validation.Application.Logic.Mappers;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;
using FluentAssertions;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykkeV2Dfv45957_MapperTests
    {
        //[Fact]
        //public void Test1()
        //{
        //    var dataForm = new ArbeidstilsynetsSamtykkeType()
        //    {
        //        eiendomByggested = new EiendomType()
        //        {
        //            adresse = new EiendommensAdresseType()
        //            {
        //                adresselinje1 = "Bø gate 31A",
        //                bokstav = "A",
        //                husnr = "31",
        //                gatenavn = "Bø gate"

        //            },
        //            bolignummer = "12345",
        //            kommunenavn = "Bø i Telemark"
        //        }
        //};
        //    var dataFomr = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataForm);
        //    //string[] array = list.ToArray();
        //    dataFomr.Eiendom.Should().NotBeNull();
        //}
        [Fact]
        public void ArbeidsplasserTest()
        {
            var dataForm = new ArbeidstilsynetsSamtykkeType()
            {
                arbeidsplasser = new ArbeidsplasserType()
                {
                    framtidige = false,
                    framtidigeSpecified = true,
                    faste = true,
                    fasteSpecified = true,
                    midlertidige = true,
                    midlertidigeSpecified = true,

                    antallAnsatte = "5",
                    antallVirksomheter = "2",
                    eksisterende = true,
                    eksisterendeSpecified = true,
                    beskrivelse = "noko rar kommer har",
                    
                    utleieBygg = null,
                    utleieByggSpecified = false
                }
            };
            Arbeidsplasser arbeidsplasser = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().MapArbeidsplasser(dataForm.arbeidsplasser);

            arbeidsplasser.Should().NotBeNull();
        }
    }
}
