using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class BetalingValidator : EntityValidatorBase
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public BetalingValidator(IList<EntityValidatorNode> entityValidatorTree)
            : base(entityValidatorTree)
        { }

        public ValidationResult Validate(BetalingValidationEntity betaling, string[] attachments = null)
        {
            ValidateEntityFields(betaling);

            return _validationResult;
        }

        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ValidationRuleEnum.utfylt);
            this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.beskrivelse);
            this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.sum);
            this.AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.sum);
            this.AddValidationRule(ValidationRuleEnum.numerisk, FieldNameEnum.sum);
            this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.gebyrkategori);
            this.AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.gebyrkategori);
            this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.skalFaktureres);
        }

        public void ValidateEntityFields(BetalingValidationEntity betalingValEntity)
        {
            var betaling = betalingValEntity;
            if (Helpers.ObjectIsNullOrEmpty((object)betaling))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (betaling.Beskrivelse == null)
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.beskrivelse);
                }

                if (betaling.SkalFaktureres == null)
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.skalFaktureres);
                }

                if (string.IsNullOrEmpty((string)betaling.GebyrKategori))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.gebyrkategori);
                }

                if (string.IsNullOrEmpty((string)betaling.Sum))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.sum);
                }
                else
                {
                    var isNumeric = int.TryParse(betaling.Sum, out int numericAmount);
                    if (!isNumeric)
                    {
                        AddMessageFromRule(ValidationRuleEnum.numerisk, FieldNameEnum.sum);
                    }
                    else
                    {
                        if (numericAmount <= 0)
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.sum);
                        }
                    }
                }
            }
        }
    }
}
