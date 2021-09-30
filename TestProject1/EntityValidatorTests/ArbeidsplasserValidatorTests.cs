using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.IO;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class ArbeidsplasserValidatorTests
    {
        private ArbeidsplasserValidationEntity _arbeidsplasser;
        private List<string> _attachemntList;
        private ArbeidstilsynetsSamtykkeType _form;
        private ArbeidsplasserValidatorV2 _arbeidsplasserValidator;


        public ArbeidsplasserValidatorTests()
        {

            var xmlData = File.ReadAllText(@"Data\ArbeidstilsynetsSamtykke_v2_dfv45957.xml");
            _form = SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
            _arbeidsplasser = new ArbeidsplasserMapper().Map(_form.arbeidsplasser, "");
            var arbeidsplasserValidatorNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = 17, EnumId = EntityValidatorEnum.ArbeidsplasserValidatorV2, ParentID = null}
            };
            var tree = EntityValidatiorTree.BuildTree(arbeidsplasserValidatorNodeList);
            _arbeidsplasserValidator = new ArbeidsplasserValidatorV2(tree);
            _attachemntList = new List<string>() { "BeskrivelseTypeArbeidProsess" };


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

        }

        [Fact]
        public void ArbeidsplasserTest()
        {
            
        }
    }
}
