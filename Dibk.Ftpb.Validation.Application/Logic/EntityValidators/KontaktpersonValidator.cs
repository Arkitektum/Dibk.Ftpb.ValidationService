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
        private string _xPath;
        private const string _entityName = "kontaktperson";

        public KontaktpersonValidator(string parentxPath)
        {
            _xPath = $"{parentxPath}/{_entityName}";
            InitializeValidationRules();
        }
        public override void InitializeValidationRules(string parentxPath = null)
        {
            AddValidationRule("navn_utfylt", _xPath, "navn");
        }

        public ValidationResult Validate(string parentXpath = null, Kontaktperson kontaktperson = null)
        {
            if (string.IsNullOrEmpty(kontaktperson?.Navn))
                //TODO fill upp {0} parent Node/class/context... in message
                AddMessageFromRule("navn_utfylt");

            return ValidationResult;
        }
    }
}
