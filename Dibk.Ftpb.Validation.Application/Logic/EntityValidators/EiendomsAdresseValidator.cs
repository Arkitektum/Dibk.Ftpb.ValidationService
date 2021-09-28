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
    public class EiendomsAdresseValidator : EntityValidatorBase, IEiendomsAdresseValidator
    {
        ValidationResult IEiendomsAdresseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomsAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, int? nodeId = null)
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
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.poststed);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.gatenavn);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.husnr);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.bokstav);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.postnr);
        }

        public ValidationResult Validate(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            base.ResetValidationMessages();
            var xPath = eiendomsAdresseValidationEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);
            }
            else
            {
                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje1))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.adresselinje1}");

                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Landkode))
                    AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/{FieldNameEnum.landkode}");

                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Postnr))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.postnr}");

                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Poststed))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.poststed}");

                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Gatenavn))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.gatenavn}");

                if (string.IsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Husnr))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.husnr}");

                if (!StringIs4digitNumber(eiendomsAdresseValidationEntity.ModelData.Postnr))
                    AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, $"{xPath}/{FieldNameEnum.postnr}");
            }
            return _validationResult;
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
