﻿using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ArbeidsplasserValidator : EntityValidatorBase
    {
        private string[] _attachmentList;
        
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public ArbeidsplasserValidator(IList<EntityValidatorNode> entityValidatorTree) 
            : base(entityValidatorTree)
        { }

        public ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, string[] attachments = null)
        {
            _attachmentList = attachments;
            ValidateEntityFields(arbeidsplasser);

            return _validationResult;
        }

        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ValidationRuleEnum.utfylt);
            this.AddValidationRule(ValidationRuleEnum.framtidige_eller_eksisterende_utfylt); // (ValidationRuleEnum.gyldig, FieldNameEnum.framtidige_eller_eksisterende)
            this.AddValidationRule(ValidationRuleEnum.faste_eller_midlertidige_utfylt);
            this.AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.antallAnsatte);
            this.AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.antallVirksomheter);
            this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.beskrivelse);
        }

        public void ValidateEntityFields(ArbeidsplasserValidationEntity arbeidsplasserValEntity)
        {
            var arbeidsplasser = arbeidsplasserValEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(arbeidsplasser))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (!arbeidsplasser.Eksisterende.GetValueOrDefault(false) && !arbeidsplasser.Framtidige.GetValueOrDefault(false))
                {
                    AddMessageFromRule(ValidationRuleEnum.framtidige_eller_eksisterende_utfylt);
                }
                else
                {
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        AddMessageFromRule(ValidationRuleEnum.faste_eller_midlertidige_utfylt);
                    }
                    
                    int antallAnsatte;
                    int.TryParse(arbeidsplasser.AntallAnsatte, out antallAnsatte);
                    if (antallAnsatte <= 0)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.antallAnsatte);
                    }

                    if (arbeidsplasser.UtleieBygg.GetValueOrDefault(false))
                    {
                        int antallVirksomheter;
                        int.TryParse(arbeidsplasser.AntallVirksomheter, out antallVirksomheter);
                        if (antallVirksomheter <= 0)
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.antallVirksomheter);
                        }
                    }

                    if (string.IsNullOrEmpty(arbeidsplasser.Beskrivelse))
                    {
                        if (_attachmentList == null || _attachmentList.All(i=>!i.Equals("BeskrivelseTypeArbeidProsess")))
                        {
                            AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.beskrivelse);
                        }
                    }
                }
            }
        }
    }
}
