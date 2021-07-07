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
        ValidationResult IEntityBaseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomsAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, int? nodeId = null)
            : base(entityValidatorTree, nodeId)
        {
        }

        public ValidationResult Validate(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            base.ResetValidationMessages();
            if (ValidateModelExists(eiendomsAdresseValidationEntity))
            {
                ValidateEntityFields(eiendomsAdresseValidationEntity);
            }

            return _validationResult;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(EiendomsAdresseValidationEnums.utfylt);
            AddValidationRule(EiendomsAdresseValidationEnums.adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(EiendomsAdresseValidationEnums.adresselinje2_utfylt, "adresselinje2");
            AddValidationRule(EiendomsAdresseValidationEnums.adresselinje3_utfylt, "adresselinje3");
            AddValidationRule(EiendomsAdresseValidationEnums.landkode_utfylt, "landkode");
            AddValidationRule(EiendomsAdresseValidationEnums.postnr_utfylt, "postnr");
            AddValidationRule(EiendomsAdresseValidationEnums.poststed_utfylt, "poststed");
            AddValidationRule(EiendomsAdresseValidationEnums.gatenavn_utfylt, "gatenavn");
            AddValidationRule(EiendomsAdresseValidationEnums.husnr_utfylt, "husnr");
            AddValidationRule(EiendomsAdresseValidationEnums.bokstav_utfylt, "bokstav");
            AddValidationRule(EiendomsAdresseValidationEnums.postnr_4siffer, "postnr");
        }
        private bool ValidateModelExists(EiendomsAdresseValidationEntity modelEntity)
        {
            var xPath = modelEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(modelEntity.ModelData))
            {
                AddMessageFromRule(EiendomsAdresseValidationEnums.utfylt, xPath);
                return false;
            }
            return true;
        }
        private void ValidateEntityFields(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            var xPath = eiendomsAdresseValidationEntity.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje1))
                AddMessageFromRule(EiendomsAdresseValidationEnums.adresselinje1_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje2))
                AddMessageFromRule(EiendomsAdresseValidationEnums.adresselinje2_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje3))
                AddMessageFromRule(EiendomsAdresseValidationEnums.adresselinje3_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Landkode))
                AddMessageFromRule(EiendomsAdresseValidationEnums.landkode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(EiendomsAdresseValidationEnums.postnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Poststed))
                AddMessageFromRule(EiendomsAdresseValidationEnums.poststed_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Gatenavn))
                AddMessageFromRule(EiendomsAdresseValidationEnums.gatenavn_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Husnr))
                AddMessageFromRule(EiendomsAdresseValidationEnums.husnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Bokstav))
                AddMessageFromRule(EiendomsAdresseValidationEnums.bokstav_utfylt, xPath);

            if (!StringIs4digitNumber(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(EiendomsAdresseValidationEnums.postnr_4siffer, xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
