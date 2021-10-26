using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarsomraadeValidator : EntityValidatorBase, IAnsvarsomraadeValidator
    {
        private readonly IKodelisteValidator _funksjonValidator;
        private readonly IKodelisteValidator _tilttaksklasseValidator;
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public AnsvarsomraadeValidator(IList<EntityValidatorNode> entityValidatorTree, IKodelisteValidator funksjonValidator, IKodelisteValidator tilttaksklasseValidator) : base(entityValidatorTree)
        {
            _funksjonValidator = funksjonValidator;
            _tilttaksklasseValidator = tilttaksklasseValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt,FieldNameEnum.beskrivelseAvAnsvarsomraade);

        }

        public ValidationResult Validate(Ansvarsomraade ansvarsomraade = null)
        {
            base.ResetValidationMessages();

            ValidateEntityFields(ansvarsomraade);
            var funksjonResult = _funksjonValidator.Validate(ansvarsomraade.Funksjon);
            UpdateValidationResultWithSubValidations(funksjonResult);

            var tiltaksklasseResult = _tilttaksklasseValidator.Validate(ansvarsomraade.Funksjon);
            UpdateValidationResultWithSubValidations(tiltaksklasseResult);
            return ValidationResult;
        }

        public void ValidateEntityFields(Ansvarsomraade ansvarsomraade = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(ansvarsomraade))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (string.IsNullOrEmpty(ansvarsomraade.BeskrivelseAvAnsvarsomraade))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.beskrivelseAvAnsvarsomraade);
                }
            }
        }

    }
}
