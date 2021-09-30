using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidatorV2 : EntityValidatorBase, IEnkelAdresseValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }


        public EnkelAdresseValidatorV2(IList<EntityValidatorNode> entityValidatorTree, int nodeId)
            : base(entityValidatorTree, nodeId)
        {
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje1);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje2);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje3);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.landkode);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.postnr_stemmerIkke, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.validert, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.postnr);
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                ValidateEntityFields(enkelAdresse);
            }

            return _validationResult;
        }

        public void ValidateEntityFields(EnkelAdresseValidationEntity adresseValidationEntity)
        {

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.Adresselinje1))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje1);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.Adresselinje2))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje2);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.Adresselinje3))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje3);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.Landkode))
                AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.landkode);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.Postnr))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.postnr);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.Poststed))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.poststed);

            if (!StringIs4digitNumber(adresseValidationEntity.Postnr))
                AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.postnr);
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
