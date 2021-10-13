using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class AnsvarsrettValidator : EntityValidatorBase
    {
        private readonly IForetakValidator _foretakValidator;
        private readonly IAnsvarsomraadeValidator _ansvarsomraadeValidator;
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public AnsvarsrettValidator(IList<EntityValidatorNode> entityValidatorTree, IForetakValidator foretakValidator, IAnsvarsomraadeValidator ansvarsomraadeValidator) : base(entityValidatorTree)
        {
            _foretakValidator = foretakValidator;
            _ansvarsomraadeValidator = ansvarsomraadeValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
        }

        public ValidationResult Validate(Ansvarsrett ansvarsrett)
        {
            var foretakValidationResult = _foretakValidator.Validate(ansvarsrett?.Foretak);
            UpdateValidationResultWithSubValidations(foretakValidationResult);

            var index = GetArrayIndex(ansvarsrett.Ansvarsomraades);
            for (int i = 0; i < index; i++)
            {
                var ansvarsomraade = Helpers.ObjectIsNullOrEmpty(ansvarsrett.Ansvarsomraades) ? null : ansvarsrett.Ansvarsomraades[i];
                var ansvarsomraadeResult = _ansvarsomraadeValidator.Validate(ansvarsomraade);
                UpdateValidationResultWithSubValidations(ansvarsomraadeResult, i);
            }

            return ValidationResult;
        }

        public void ValidateEntityFields(Ansvarsrett ansvarsrett)
        {

        }
    }
}
