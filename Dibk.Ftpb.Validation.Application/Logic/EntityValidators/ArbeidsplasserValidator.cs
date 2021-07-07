using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ArbeidsplasserValidator : EntityValidatorBase, IArbeidsplasserValidator
    {
        private List<string> _attachmentList;
        
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public ArbeidsplasserValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId) 
            : base(entityValidatorTree, nodeId)
        {
        }

        public ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, List<string> attachments = null)
        {

            _attachmentList = attachments;

            ValidateEntityFields(arbeidsplasser);

            return _validationResult;
        }

        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_utfylt);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt);
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_type_arbeid_utfylt, "antallVirksomheter");
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_utleieBygg, "utleieBygg");
            this.AddValidationRule(ValidationRuleEnum.arbeidsplasser_beskrivelse, "beskrivelse");
        }

        public void ValidateEntityFields(ArbeidsplasserValidationEntity arbeidsplasserValEntity)
        {
            var xpath = arbeidsplasserValEntity.DataModelXpath;
            var arbeidsplasser = arbeidsplasserValEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(arbeidsplasser))
            {
                AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_utfylt, xpath);
            }
            else
            {
                if (!arbeidsplasser.Eksisterende.GetValueOrDefault(false) && !arbeidsplasser.Framtidige.GetValueOrDefault(false))
                {
                    AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt, xpath);
                }
                else
                {
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt, xpath);
                    }

                    if (arbeidsplasser.UtleieBygg.GetValueOrDefault(false))
                    {
                        int antallVirksomheter;
                        int.TryParse(arbeidsplasser.AntallVirksomheter, out antallVirksomheter);
                        if (antallVirksomheter <= 0)
                        {
                            AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_utleieBygg, xpath);
                        }
                    }

                    if (string.IsNullOrEmpty(arbeidsplasser.Beskrivelse))
                    {
                        if (_attachmentList == null || !_attachmentList.Contains("BeskrivelseTypeArbeidProsess"))
                        {
                            AddMessageFromRule(ValidationRuleEnum.arbeidsplasser_beskrivelse, xpath);
                        }
                    }
                }
            }
        }
    }
}
