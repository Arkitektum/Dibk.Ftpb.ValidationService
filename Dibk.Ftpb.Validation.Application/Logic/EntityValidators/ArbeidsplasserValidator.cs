using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ArbeidsplasserValidator : EntityValidatorBase
    {
        private List<string> _attachmentList;

        public ArbeidsplasserValidator(string xPath) : base()
        {   
            InitializeValidationRules(xPath);
        }
        public ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, List<string> attachments = null)
        {

            _attachmentList = attachments;

            ValidateEntityFields(arbeidsplasser);

            return ValidationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_utfylt, xPathForEntity);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt, xPathForEntity);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt, xPathForEntity);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_type_arbeid_utfylt, xPathForEntity);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_utleieBygg, xPathForEntity);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_beskrivelse, xPathForEntity, "beskrivelse");
        }


        public void ValidateEntityFields(ArbeidsplasserValidationEntity arbeidsplasserValEntity)
        {
            var arbeidsplasser = arbeidsplasserValEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(arbeidsplasser))
            {
                AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_utfylt);
            }
            else
            {
                if (!arbeidsplasser.Eksisterende.GetValueOrDefault(false) && !arbeidsplasser.Framtidige.GetValueOrDefault(false))
                {
                    AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt);
                }
                else
                {
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt);
                    }

                    if (arbeidsplasser.UtleieBygg.GetValueOrDefault(false))
                    {
                        int antallVirksomheter;
                        int.TryParse(arbeidsplasser.AntallVirksomheter, out antallVirksomheter);
                        if (antallVirksomheter <= 0)
                        {
                            AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_utleieBygg);
                        }
                    }

                    if (string.IsNullOrEmpty(arbeidsplasser.Beskrivelse))
                    {
                        if (_attachmentList == null || !_attachmentList.Contains("BeskrivelseTypeArbeidProsess"))
                        {
                            AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_beskrivelse);
                        }
                    }
                }
            }
        }
    }
}
