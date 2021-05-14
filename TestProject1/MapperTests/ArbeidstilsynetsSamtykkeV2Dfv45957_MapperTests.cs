using System.IO;
using Dibk.Ftpb.Validation.Application.Logic.Mappers;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;
using FluentAssertions;

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
            Arbeidsplasser arbeidsplasser = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().MapArbeidsplasser(dataForm.arbeidsplasser);

            arbeidsplasser.Should().NotBeNull();
        }

        [Fact]
        public void TiltakshaverMapTest()
        {
            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke2.xml");
            var form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            var postmanXml = TestHelper.GetXmlWithoutSpaces(xmlData);
           
            var tiltakshaver = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().MapTiltakshaver(form.tiltakshaver);

            tiltakshaver.Should().NotBeNull();

        }
    }
}
