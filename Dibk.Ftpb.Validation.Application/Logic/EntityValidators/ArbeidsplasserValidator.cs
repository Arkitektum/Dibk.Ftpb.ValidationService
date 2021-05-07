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

        public override void InitializeValidationRules(string context)
        {
            this.AddValidationRule("", "Context");
        }

        public List<ValidationRule> Validate(string parentContext, Arbeidsplasser arbeidsplasser)
        {
            _context = $"{parentContext}/{_entityName}";

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
                rule.validationResult = ValidationResultEnum.ValidationFailed;
            }
            else
            {
                rule = RuleToValidate("framtidige_Eller_eksisterende_Utfylt");
                if (!arbeidsplasser.Eksisterende.GetValueOrDefault(false) && !arbeidsplasser.Framtidige.GetValueOrDefault(false))
                {
                    rule.validationResult = ValidationResultEnum.ValidationFailed;
                }
                else
                {
                    rule = RuleToValidate("type_Arbeid_Utfylt");
                    if (!arbeidsplasser.Faste.GetValueOrDefault(false) && !arbeidsplasser.Midlertidige.GetValueOrDefault(false))
                    {
                        rule.validationResult = ValidationResultEnum.ValidationFailed;
                    }
                }

            }
        }
    }
}
