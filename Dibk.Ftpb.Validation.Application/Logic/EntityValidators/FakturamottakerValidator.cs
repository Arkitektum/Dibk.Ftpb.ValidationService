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

        private EnkelAdresseValidator _enkelAdresseValidator;
        public FakturamottakerValidator(string parentxPath) : base(parentxPath, "fakturamottaker")
        {
            _enkelAdresseValidator = new EnkelAdresseValidator(EntityXPath);
            InitializeValidationRules();
        }
        public sealed override void InitializeValidationRules()
        {
            AddValidationRule("fakturamottaker_utfylt", EntityXPath);
        }

        public ValidationResult Validate(Fakturamottaker fakturamottaker = null)
        {
            var enkelAdressValidator = new EnkelAdresseValidator(EntityXPath);

            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker))
            {
                AddMessageFromRule("fakturamottaker_utfylt");
            }
            else
            {
                _enkelAdresseValidator.Validate(fakturamottaker.Adresse);
            }
            UpdateValidationResultWithSubValidations(enkelAdressValidator.ValidationResult);

            return ValidationResult;
        }
    }
}
