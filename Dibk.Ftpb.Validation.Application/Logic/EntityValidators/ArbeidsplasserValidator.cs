using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class ArbeidsplasserValidator : EntityValidatorBase
    {

        private const string _entityName = "arbeidsplasser";
        private static string _context;
        private List<string> _attachemntList;

        public override void InitializeValidationRules(string context)
        {
            this.AddValidationRule("arbeidsplasser_utfylt", "Context");
            this.AddValidationRule("framtidige_Eller_eksisterende_Utfylt", "Context");
            this.AddValidationRule("type_Arbeid_Utfylt", "Context");
            this.AddValidationRule("UtleieBygg", "Context");
            this.AddValidationRule("arbeidsplasser_Beskrivelse", "Context");
        }

        public List<ValidationRule> Validate(string parentContext, Arbeidsplasser arbeidsplasser, List<string> attachemnts = null)
        {
            _context = $"{parentContext}/{_entityName}";
            _attachemntList = attachemnts;

            InitializeValidationRules(_context);
            ValidateEntityFields(arbeidsplasser);

            return this.ValidationRules;
        }

        public override void ValidateEntityFields(object objectData)
        {
            Arbeidsplasser arbeidsplasser = (Arbeidsplasser)objectData;

            var rule = this.RuleToValidate("arbeidsplasser_utfylt");
            if (Helpers.ObjectIsNullOrEmpty(arbeidsplasser))
            {
                UpdateValidationResult2Failed(rule);
            }
            else
            {
                rule = RuleToValidate("framtidige_Eller_eksisterende_Utfylt");
                if (!arbeidsplasser.Eksisterende.GetValueOrDefault(false) && !arbeidsplasser.Framtidige.GetValueOrDefault(false))
                {
                    UpdateValidationResult2Failed(rule);
                }
                else
                {
                    rule = RuleToValidate("type_Arbeid_Utfylt");
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        UpdateValidationResult2Failed(rule);
                    }

                    rule = RuleToValidate("UtleieBygg");
                    if (arbeidsplasser.UtleieBygg.GetValueOrDefault(false))
                    {
                        int antallVirksomheter;
                        int.TryParse(arbeidsplasser.AntallVirksomheter, out antallVirksomheter);
                        if (antallVirksomheter <= 0)
                            UpdateValidationResult2Failed(rule);
                    }

                    rule = RuleToValidate("arbeidsplasser_Beskrivelse");
                    if (string.IsNullOrEmpty(arbeidsplasser.Beskrivelse))
                    {
                        if (_attachemntList == null || !_attachemntList.Contains("BeskrivelseTypeArbeidProsess"))
                        {
                            UpdateValidationResult2Failed(rule);
                        }
                    }
                }
            }
        }
    }
}
