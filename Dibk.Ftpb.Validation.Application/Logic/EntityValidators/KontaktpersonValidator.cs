using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class KontaktpersonValidator : EntityValidatorBase, IKontaktpersonValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KontaktpersonValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId)
            : base(entityValidatorTree, nodeId)
        {
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt, "navn");
            AddValidationRule(ValidationRuleEnum.utfylt, "telefonnummer");
        }

        public ValidationResult Validate(KontaktpersonValidationEntity kontaktperson = null)
        {

            var xpath = kontaktperson?.DataModelXpath;
            if (string.IsNullOrEmpty(kontaktperson?.ModelData?.Navn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);

            if (string.IsNullOrEmpty(kontaktperson?.ModelData?.Telefonnummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);

            return _validationResult;
        }
    }
}
