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
        public override string ruleXmlElement { get { return "/fakturamottaker"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly IEnkelAdresseValidator _enkelAdresseValidator;

        public FakturamottakerValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, IEnkelAdresseValidator enkelAdresseValidator) 
            : base(entityValidatorOrchestrator)
        {
            InitializeValidationRules(EntityXPath);

            //TODO: Automize this?
            _enkelAdresseValidator = enkelAdresseValidator;
        }
        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.fakturamottaker_utfylt, xPathForEntity);
        }

        public ValidationResult Validate(FakturamottakerValidationEntity fakturamottaker = null)
        {

            if (Helpers.ObjectIsNullOrEmpty(fakturamottaker.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.fakturamottaker_utfylt);
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
