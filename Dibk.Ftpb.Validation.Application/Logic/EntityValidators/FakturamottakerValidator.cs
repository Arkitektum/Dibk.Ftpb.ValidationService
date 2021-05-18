using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FakturamottakerValidator : EntityValidatorBase
    {

        private string _xPath;
        private const string _entityName = "fakturamottaker";

        public FakturamottakerValidator(string parentxPath)
        {
            _xPath = $"{parentxPath}/{_entityName}";
            InitializeValidationRules();
        }
        public override void InitializeValidationRules(string parentContext = null)
        {
            AddValidationRule("fakturamottaker_utfylt", _xPath);
        }

        public ValidationResult Validate(string xPath = null, Fakturamottaker fakturamottaker = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker))
            {
                AddMessageFromRule("fakturamottaker_utfylt");
            }
            else
            {
                var enkelAdress = new EnkelAdresseValidator(_xPath).Validate(null, fakturamottaker.adresse);
                UpdateValidationResultWithSubValidations(enkelAdress);
            }
            
            return ValidationResult;
        }
        private void UpdateValidationResultWithSubValidations(ValidationResult newValudationResult)
        {
            ValidationResult.ValidationRules.AddRange(newValudationResult.ValidationRules);
            ValidationResult.ValidationMessages.AddRange(newValudationResult.ValidationMessages);
        }
    }
}
