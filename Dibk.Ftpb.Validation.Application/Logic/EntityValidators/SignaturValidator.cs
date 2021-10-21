using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SignaturValidator : EntityValidatorBase
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public SignaturValidator(IList<EntityValidatorNode> entityValidatorTree)
            : base(entityValidatorTree)
        { }
        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ValidationRuleEnum.utfylt);
            //this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.signaturdato);
            //this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.signertAv);
            //this.AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.signertPaaVegneAv);
        }

        public ValidationResult Validate(SignaturValidationEntity signatur = null, string[] attachments = null)
        {
            ValidateEntityFields(signatur);

            return _validationResult;
        }

        public void ValidateEntityFields(SignaturValidationEntity signaturValEntity)
        {
            if (Helpers.ObjectIsNullOrEmpty(signaturValEntity))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (signaturValEntity.Signaturdato == null)
                {
                    //AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.signaturdato);
                }
                if (string.IsNullOrEmpty(signaturValEntity.SignertAv))
                {
                    //AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.signertAv);
                }
                if (string.IsNullOrEmpty(signaturValEntity.SignertPaaVegneAv))
                {
                    //AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.signertPaaVegneAv);
                }
            }
        }



    }
}
