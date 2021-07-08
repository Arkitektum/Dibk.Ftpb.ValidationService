﻿using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
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
            AddValidationRule(EnkelAdresseValidationEnums.utfylt);
            AddValidationRule(EnkelAdresseValidationEnums.adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(EnkelAdresseValidationEnums.adresselinje2_utfylt, "adresselinje2");
            AddValidationRule(EnkelAdresseValidationEnums.adresselinje3_utfylt, "adresselinje3");
            AddValidationRule(EnkelAdresseValidationEnums.landkode_utfylt, "landkode");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_utfylt, "postnr");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_kontrollsiffer, "postnr");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_gyldig, "postnr");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_stemmerIkke, "postnr");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_ikke_validert, "postnr");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_4siffer, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            var xpath = enkelAdresse.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(EnkelAdresseValidationEnums.utfylt, xpath);
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
                AddMessageFromRule(EnkelAdresseValidationEnums.adresselinje1_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Adresselinje2))
                AddMessageFromRule(EnkelAdresseValidationEnums.adresselinje2_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Adresselinje3))
                AddMessageFromRule(EnkelAdresseValidationEnums.adresselinje3_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Landkode))
                AddMessageFromRule(EnkelAdresseValidationEnums.landkode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(EnkelAdresseValidationEnums.postnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Poststed))
                AddMessageFromRule(EnkelAdresseValidationEnums.poststed_utfylt, xPath);

            if (!StringIs4digitNumber(adresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(EnkelAdresseValidationEnums.postnr_4siffer, xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
