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
        private List<string> _attachmentList;

        //public ValidationResponse Validate(string parentContext, Arbeidsplasser arbeidsplasser)
        //{
        //    this.AddValidationRule("arbeidsplasser_utfylt", "Context");
        //    this.AddValidationRule("framtidige_Eller_eksisterende_Utfylt", "Context");
        //    this.AddValidationRule("type_Arbeid_Utfylt", "Context");
        //    this.AddValidationRule("UtleieBygg", "Context");
        //    this.AddValidationRule("arbeidsplasser_Beskrivelse", "Context");

        //    return ValidationResponse;
        //}

        public ValidationResult Validate(string parentContext, Arbeidsplasser arbeidsplasser, List<string> attachments = null)
        {
            _context = $"{parentContext}/{_entityName}";
            _attachmentList = attachments;

            InitializeValidationRules(_context);
            ValidateEntityFields(arbeidsplasser, _context);

            return ValidationResult;
        }

        public override void InitializeValidationRules(string xPath)
        {
            this.AddValidationRule("arbeidsplasser_utfylt", $"{xPath}");
            this.AddValidationRule("framtidige_eller_eksisterende_utfylt", $"{xPath}");
            this.AddValidationRule("faste_eller_midlertidige_utfylt", $"{xPath}");
            this.AddValidationRule("type_arbeid_utfylt", "Context");
            this.AddValidationRule("utleieBygg", "Context");
            this.AddValidationRule("arbeidsplasser_beskrivelse", "Context");
        }


        public void ValidateEntityFields(Arbeidsplasser arbeidsplasser, string xPath)
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
