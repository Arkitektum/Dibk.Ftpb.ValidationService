using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

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
            AddValidationRule(ValidationRuleEnum.utfylt, "adresselinje1");
            AddValidationRule(ValidationRuleEnum.utfylt, "adresselinje2");
            AddValidationRule(ValidationRuleEnum.utfylt, "adresselinje3");
            AddValidationRule(ValidationRuleEnum.utfylt, "landkode");
            AddValidationRule(ValidationRuleEnum.utfylt, "postnr");
            AddValidationRule(ValidationRuleEnum.utfylt, "poststed");
            AddValidationRule(ValidationRuleEnum.utfylt, "gatenavn");
            AddValidationRule(ValidationRuleEnum.utfylt, "husnr");
            AddValidationRule(ValidationRuleEnum.utfylt, "bokstav");
            AddValidationRule(ValidationRuleEnum.postnr_4siffer, "postnr");
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
                if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje1))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/adresselinje1");

                if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Landkode))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/landkode");

                if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Postnr))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/postnr");

                if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Poststed))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/poststed");

                if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Gatenavn))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/gatenavn");

                if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Husnr))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/husnr");

                if (!StringIs4digitNumber(eiendomsAdresseValidationEntity.ModelData.Postnr))
                    AddMessageFromRule(ValidationRuleEnum.postnr_4siffer, $"{xPath}/postnr");
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
