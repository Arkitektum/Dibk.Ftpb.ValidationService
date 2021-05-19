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

        private List<string> _attachmentList;

        public ArbeidsplasserValidator(string parentXPath) : base(parentXPath, "arbeidsplasser")
        {
            InitializeValidationRules();
        }
        public ValidationResult Validate(Arbeidsplasser arbeidsplasser, List<string> attachments = null)
        {

            _attachmentList = attachments;

            ValidateEntityFields(arbeidsplasser);

            return ValidationResult;
        }

        public sealed override void InitializeValidationRules()
        {
            this.AddValidationRule("arbeidsplasser_utfylt", EntityXPath);
            this.AddValidationRule("framtidige_eller_eksisterende_utfylt", EntityXPath);
            this.AddValidationRule("faste_eller_midlertidige_utfylt", EntityXPath);
            this.AddValidationRule("type_arbeid_utfylt", EntityXPath);
            this.AddValidationRule("utleieBygg", EntityXPath);
            this.AddValidationRule("arbeidsplasser_beskrivelse", EntityXPath, "beskrivelse");
        }


        public void ValidateEntityFields(Arbeidsplasser arbeidsplasser)
        {

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
