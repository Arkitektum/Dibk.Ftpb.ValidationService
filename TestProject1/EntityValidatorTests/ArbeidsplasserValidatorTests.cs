using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class ArbeidsplasserValidatorTests
    {
        private ArbeidsplasserValidationEntity _arbeidsplasser;
        private List<string> _attachemntList;


        public ArbeidsplasserValidatorTests()
        {
            var arbeidsplasser = new Arbeidsplasser()
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

            _arbeidsplasser = new ArbeidsplasserValidationEntity(arbeidsplasser, "Arbeidsplasser");

            _attachemntList = new List<string>() { "BeskrivelseTypeArbeidProsess" };
        }

        [Fact]
        public void ArbeidsplasserTest()
        {
            EntityValidatorOrchestrator entityValidatorOrchestrator = new EntityValidatorOrchestrator();

            var arbeidsplasser = new ArbeidsplasserValidator(entityValidatorOrchestrator);
            _arbeidsplasser.ModelData.Beskrivelse = null;
            _attachemntList.Remove("BeskrivelseTypeArbeidProsess");
            var validationsResult = arbeidsplasser.Validate(_arbeidsplasser, _attachemntList);

            //validationsResult.Any(r => r.ValidationResult == ValidationResultEnum.ValidationFailed).Should().BeFalse();
            //validationsResult.Should().NotBeEmpty();
        }
    }
}
