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
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.landkode);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.postnr_stemmerIkke, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.postnr_ikke_validert, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.postnr_4siffer, FieldNameEnum.postnr);
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            var xpath = enkelAdresse.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
            }
            else
            {
                ValidateEntityFields(enkelAdresse);
            }

            return _validationResult;
        }

        public void ValidateEntityFields(EnkelAdresseValidationEntity adresseValidationEntity)
        {
            var xPath = adresseValidationEntity.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Adresselinje1))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.adresselinje1}");

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Adresselinje2))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.adresselinje2}");

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Adresselinje3))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.adresselinje3}");

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Landkode))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.landkode}");

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.postnr}");

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Poststed))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.poststed}");

            if (!StringIs4digitNumber(adresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.postnr_4siffer, $"{xPath}/{FieldNameEnum.postnr}");
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
