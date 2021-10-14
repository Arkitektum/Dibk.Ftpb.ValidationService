using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomsAdresseValidatorV2 : EntityValidatorBase, IEiendomsAdresseValidator
    {
        ValidationResult IEiendomsAdresseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomsAdresseValidatorV2(IList<EntityValidatorNode> entityValidatorTree, int? nodeId = null)
            : base(entityValidatorTree, nodeId)
        {
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje1);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.gatenavn);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.husnr);
        }

        public ValidationResult Validate(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            base.ResetValidationMessages();

            if (!Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.Adresselinje1))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje1);

                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.Gatenavn))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.gatenavn);

                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.Husnr))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.husnr);
            }
            return _validationResult;
        }
    }
}
