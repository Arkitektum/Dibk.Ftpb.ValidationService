using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ArbeidsplasserValidatorV2 : ArbeidsplasserValidator
    {
        private List<string> _attachmentList;
        
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public ArbeidsplasserValidatorV2(IList<EntityValidatorNode> entityValidatorTree) 
            : base(entityValidatorTree)
        { }

        public ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, IEnumerable<SjekklistekravValidationEntity> sjekkliste, List<string> attachments = null)
        {
            _attachmentList = attachments;
            ValidateEntityFields(arbeidsplasser, sjekkliste);

            return _validationResult;
        }

        protected override void InitializeValidationRules()
        {
            base.InitializeValidationRules();
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.veiledning);
            AddValidationRuleOverideXpath(ValidationRuleEnum.sjekklistepunkt_1_17_dokumentasjon_utfylt, "/krav{0}/dokumentasjon");
        }

        public void ValidateEntityFields(ArbeidsplasserValidationEntity arbeidsplasserValEntity, IEnumerable<SjekklistekravValidationEntity> sjekkliste)
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
                    //AddMessageFromRule(ValidationRuleEnum.framtidige_eller_eksisterende_utfylt, xpath);
                }
                else
                {
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        //AddMessageFromRule(ValidationRuleEnum.faste_eller_midlertidige_utfylt, xpath);
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

                        foreach (var krav in sjekkliste)
                        {
                            if (krav.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals("1.17"))
                            {
                                if (string.IsNullOrEmpty(krav.ModelData.Dokumentasjon))
                                {
                                    //AddMessageFromRule(ValidationRuleEnum.sjekklistepunkt_1_17_dokumentasjon_utfylt, krav.ModelData.Sjekklistepunkt.DataModelXpath);
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(arbeidsplasser.Beskrivelse))
                    {
                        if (_attachmentList == null || !_attachmentList.Contains("BeskrivelseTypeArbeidProsess"))
                        {
                            AddMessageFromRule(ValidationRuleEnum.beskrivelse, FieldNameEnum.beskrivelse);
                        }
                    }
                    
                    if (arbeidsplasser.Veiledning.HasValue)
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.veiledning);
                    }
                }
            }
        }


    }
}
