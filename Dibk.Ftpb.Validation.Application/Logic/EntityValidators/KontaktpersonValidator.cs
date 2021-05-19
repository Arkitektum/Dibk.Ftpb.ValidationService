using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class KontaktpersonValidator : EntityValidatorBase
    {
 
        public KontaktpersonValidator(string parentxPath): base(parentxPath, "kontaktperson")
        {
            InitializeValidationRules();
        }
        public sealed override void InitializeValidationRules()
        {
            AddValidationRule("kontaktpersonNavn_utfylt", EntityXPath, "navn");
        }

        public ValidationResult Validate(string parentXpath = null, Kontaktperson kontaktperson = null)
        {
            if (string.IsNullOrEmpty(kontaktperson?.Navn))
                //TODO fill upp {0} parent Node/class/context... in message
                AddMessageFromRule("kontaktpersonNavn_utfylt");

            return ValidationResult;
        }
    }
}
