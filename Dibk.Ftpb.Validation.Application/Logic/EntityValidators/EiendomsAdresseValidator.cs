﻿using System.Collections.Generic;
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
            AddValidationRule(EiendomsAdresseValidationEnum.utfylt);
            AddValidationRule(EiendomsAdresseValidationEnum.adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(EiendomsAdresseValidationEnum.adresselinje2_utfylt, "adresselinje2");
            AddValidationRule(EiendomsAdresseValidationEnum.adresselinje3_utfylt, "adresselinje3");
            AddValidationRule(EiendomsAdresseValidationEnum.landkode_utfylt, "landkode");
            AddValidationRule(EiendomsAdresseValidationEnum.postnr_utfylt, "postnr");
            AddValidationRule(EiendomsAdresseValidationEnum.poststed_utfylt, "poststed");
            AddValidationRule(EiendomsAdresseValidationEnum.gatenavn_utfylt, "gatenavn");
            AddValidationRule(EiendomsAdresseValidationEnum.husnr_utfylt, "husnr");
            AddValidationRule(EiendomsAdresseValidationEnum.bokstav_utfylt, "bokstav");
            AddValidationRule(EiendomsAdresseValidationEnum.postnr_4siffer, "postnr");
        }
        private bool ValidateModelExists(EiendomsAdresseValidationEntity modelEntity)
        {
            var xPath = modelEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(modelEntity.ModelData))
            {
                AddMessageFromRule(EiendomsAdresseValidationEnum.utfylt, xPath);
                return false;
            }
            return true;
        }
        private void ValidateEntityFields(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            var xPath = eiendomsAdresseValidationEntity.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje1))
                AddMessageFromRule(EiendomsAdresseValidationEnum.adresselinje1_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje2))
                AddMessageFromRule(EiendomsAdresseValidationEnum.adresselinje2_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje3))
                AddMessageFromRule(EiendomsAdresseValidationEnum.adresselinje3_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Landkode))
                AddMessageFromRule(EiendomsAdresseValidationEnum.landkode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(EiendomsAdresseValidationEnum.postnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Poststed))
                AddMessageFromRule(EiendomsAdresseValidationEnum.poststed_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Gatenavn))
                AddMessageFromRule(EiendomsAdresseValidationEnum.gatenavn_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Husnr))
                AddMessageFromRule(EiendomsAdresseValidationEnum.husnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Bokstav))
                AddMessageFromRule(EiendomsAdresseValidationEnum.bokstav_utfylt, xPath);

            if (!StringIs4digitNumber(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(EiendomsAdresseValidationEnum.postnr_4siffer, xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
