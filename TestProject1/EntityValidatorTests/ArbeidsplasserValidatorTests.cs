using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class ArbeidsplasserValidatorTests
    {
        private Arbeidsplasser _arbeidsplasser;
        private List<string> _attachemntList;


        public ArbeidsplasserValidatorTests()
        {
            _arbeidsplasser = new Arbeidsplasser()
            {
                AntallAnsatte = "3",
                AntallVirksomheter = "2",
                Beskrivelse = "noko rar har",
                Eksisterende = true,
                Faste = true,
                Framtidige = true,
                Midlertidige = false,
                UtleieBygg = null
            };
            _attachemntList = new List<string>() { "BeskrivelseTypeArbeidProsess" };
        }

        [Fact]
        public void ArbeidsplasserTest()
        {
            var arbeidsplasser = new ArbeidsplasserValidator("UnitTest");
            _arbeidsplasser.Beskrivelse = null;
            _attachemntList.Remove("BeskrivelseTypeArbeidProsess");
            var validationsResult = arbeidsplasser.Validate(_arbeidsplasser,_attachemntList);

            //validationsResult.Any(r => r.ValidationResult == ValidationResultEnum.ValidationFailed).Should().BeFalse();
            //validationsResult.Should().NotBeEmpty();
        }
    }
}
