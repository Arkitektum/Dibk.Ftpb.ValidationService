using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ArbeidsplasserValidatorV2 : EntityValidatorBase, IArbeidsplasserValidator
    {
        private List<string> _attachmentList;
        
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public ArbeidsplasserValidatorV2(IList<EntityValidatorNode> entityValidatorTree, int nodeId) 
            : base(entityValidatorTree, nodeId)
        {
        }

        //TODO: Fix this
        public ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, List<string> attachments = null)
        {
            throw new System.NotImplementedException();
        }

        public ValidationResult Validate(ArbeidsplasserValidationEntity arbeidsplasser, IEnumerable<SjekklistekravValidationEntity> sjekkliste, List<string> attachments = null)
        {

            _attachmentList = attachments;

            ValidateEntityFields(arbeidsplasser, sjekkliste);

            return _validationResult;
        }

        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ArbeidsplasserValidationEnum.utfylt);
            this.AddValidationRule(ArbeidsplasserValidationEnum.framtidige_eller_eksisterende_utfylt);
            this.AddValidationRule(ArbeidsplasserValidationEnum.faste_eller_midlertidige_utfylt);
            this.AddValidationRule(ArbeidsplasserValidationEnum.type_arbeid_utfylt, "antallVirksomheter");
            this.AddValidationRule(ArbeidsplasserValidationEnum.utleieBygg, "utleieBygg");
            this.AddValidationRule(ArbeidsplasserValidationEnum.beskrivelse, "beskrivelse");
        }

        public void ValidateEntityFields(ArbeidsplasserValidationEntity arbeidsplasserValEntity, IEnumerable<SjekklistekravValidationEntity> sjekkliste)
        {
            var xpath = arbeidsplasserValEntity.DataModelXpath;
            var arbeidsplasser = arbeidsplasserValEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(arbeidsplasser))
            {
                AddMessageFromRule(ArbeidsplasserValidationEnum.utfylt, xpath);
            }
            else
            {
                if (!arbeidsplasser.Eksisterende.GetValueOrDefault(false) && !arbeidsplasser.Framtidige.GetValueOrDefault(false))
                {
                    AddMessageFromRule(ArbeidsplasserValidationEnum.framtidige_eller_eksisterende_utfylt, xpath);
                }
                else
                {
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        AddMessageFromRule(ArbeidsplasserValidationEnum.faste_eller_midlertidige_utfylt, xpath);
                    }

                    if (arbeidsplasser.UtleieBygg.GetValueOrDefault(false))
                    {
                        int antallVirksomheter;
                        int.TryParse(arbeidsplasser.AntallVirksomheter, out antallVirksomheter);
                        if (antallVirksomheter <= 0)
                        {
                            AddMessageFromRule(ArbeidsplasserValidationEnum.utleieBygg, xpath);
                        }

                        foreach (var krav in sjekkliste)
                        {
                            if (krav.ModelData.Sjekklistepunkt.ModelData.Kodeverdi.Equals("1.17"))
                            {
                                if (string.IsNullOrEmpty(krav.ModelData.Dokumentasjon))
                                {
                                    AddMessageFromRule(ATILSjekklistekravEnum.pkt_1_17_dokumentasjon_utfylt, xpath);
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(arbeidsplasser.Beskrivelse))
                    {
                        if (_attachmentList == null || !_attachmentList.Contains("BeskrivelseTypeArbeidProsess"))
                        {
                            AddMessageFromRule(ArbeidsplasserValidationEnum.beskrivelse, xpath);
                        }
                    }
                }
            }
        }


    }
}
