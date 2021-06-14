using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FakturamottakerValidator : EntityValidatorBase, IFakturamottakerValidator
    {
        public override string ruleXmlElement { get { return "fakturamottaker"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly IEnkelAdresseValidator _enkelAdresseValidator;

        public FakturamottakerValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, IEnkelAdresseValidator enkelAdresseValidator) 
            : base(entityValidatorOrchestrator)
        {
            //TODO: Automize this?
            _enkelAdresseValidator = enkelAdresseValidator;
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.fakturamottaker_utfylt);
        }

        public ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null)
        {
            var xpath = fakturamottaker.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.fakturamottaker_utfylt, xpath);
            }
            else
            {
                var adresseValidationResult = _enkelAdresseValidator.Validate(fakturamottaker.ModelData.Adresse);
                UpdateValidationResultWithSubValidations(adresseValidationResult);
            }
            return ValidationResult;
        }
    }
}
