using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ArbeidsplasserValidator : EntityValidatorBase
    {
        private List<string> _attachmentList;

        public ArbeidsplasserValidator() : base()
        {   
        }
        public ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, List<string> attachments = null)
        {
            InitializeValidationRules(arbeidsplasser.DataModelXpath);

            _attachmentList = attachments;

            ValidateEntityFields(arbeidsplasser);

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            this.AddValidationRule("arbeidsplasser_utfylt", xPathForEntity);
            this.AddValidationRule("framtidige_eller_eksisterende_utfylt", xPathForEntity);
            this.AddValidationRule("faste_eller_midlertidige_utfylt", xPathForEntity);
            this.AddValidationRule("type_arbeid_utfylt", xPathForEntity);
            this.AddValidationRule("utleieBygg", xPathForEntity);
            this.AddValidationRule("arbeidsplasser_beskrivelse", xPathForEntity, "beskrivelse");
        }


        public void ValidateEntityFields(ArbeidsplasserValidationEntity arbeidsplasserValEntity)
        {
            var arbeidsplasser = arbeidsplasserValEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(arbeidsplasser))
            {
                AddMessageFromRule("arbeidsplasser_utfylt");
            }
            else
            {
                if (!arbeidsplasser.Eksisterende.GetValueOrDefault(false) && !arbeidsplasser.Framtidige.GetValueOrDefault(false))
                {
                    AddMessageFromRule("framtidige_Eller_eksisterende_Utfylt");
                }
                else
                {
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        AddMessageFromRule("faste_eller_midlertidige_utfylt");
                    }

                    if (arbeidsplasser.UtleieBygg.GetValueOrDefault(false))
                    {
                        int antallVirksomheter;
                        int.TryParse(arbeidsplasser.AntallVirksomheter, out antallVirksomheter);
                        if (antallVirksomheter <= 0)
                        {
                            AddMessageFromRule("utleieBygg");
                        }
                    }

                    if (string.IsNullOrEmpty(arbeidsplasser.Beskrivelse))
                    {
                        if (_attachmentList == null || !_attachmentList.Contains("BeskrivelseTypeArbeidProsess"))
                        {
                            AddMessageFromRule("arbeidsplasser_beskrivelse");
                        }
                    }
                }
            }
        }
    }
}
