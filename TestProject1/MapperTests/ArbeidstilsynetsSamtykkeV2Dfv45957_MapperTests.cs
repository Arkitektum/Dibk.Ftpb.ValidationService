using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.IO;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ArbeidstilsynetsSamtykkeV2Dfv45957_MapperTests
    {
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
            ArbeidsplasserValidationEntity arbeidsplasser = new ArbeidsplasserMapper().Map(dataForm.arbeidsplasser);

            arbeidsplasser.Should().NotBeNull();
        }

        [Fact]
        public void TiltakshaverMapTest()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke2.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            var postmanXml = TestHelper.GetXmlWithoutSpaces(xmlData);

            var tiltakshaver = new AktoerMapper().Map(form.tiltakshaver);

            tiltakshaver.Should().NotBeNull();

        }
        [Fact]
        public void FakturamottakerMapTest()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke2.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);

            var fakturamottaker = new FakturamottakerMapper().Map(form.fakturamottaker);

            fakturamottaker.Should().NotBeNull();
        }
        [Fact]
        public void beskrivelseAvTiltakMapTest()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);

            var xmlData1 = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957_Test.xml");
            var form1 = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData1);

            var beskrivelseAvTiltak = new BeskrivelseAvTiltakMapper().Map(form.beskrivelseAvTiltak);
            var beskrivelseAvTiltak1 = new BeskrivelseAvTiltakMapper().Map(form1.beskrivelseAvTiltak);
            if (Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltak))
            {
                //
            }
            if (Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltak1))
            {
                //
            }

            beskrivelseAvTiltak.Should().NotBeNull();
        }
    }
}
