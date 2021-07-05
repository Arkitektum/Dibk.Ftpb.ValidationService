﻿using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidatorV2 : EntityValidatorBase, IEnkelAdresseValidator
    {
        public override string ruleXmlElement { get { return "adresse"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EnkelAdresseValidatorV2(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator)
            : base(formValidatorConfiguration, parentValidator)
        {
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.adresse_utfylt);
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje2_utfylt, "adresselinje2");
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje3_utfylt, "adresselinje3");
            AddValidationRule(ValidationRuleEnum.adresse_landkode_utfylt, "landkode");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_utfylt, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_kontrollsiffer, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_ugyldig, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_stemmerIkke, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_ikke_validert, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_4siffer, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            var xpath = enkelAdresse.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.adresse_utfylt, xpath);
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
                AddMessageFromRule(ValidationRuleEnum.adresse_adresselinje1_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Adresselinje2))
                AddMessageFromRule(ValidationRuleEnum.adresse_adresselinje2_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Adresselinje3))
                AddMessageFromRule(ValidationRuleEnum.adresse_adresselinje3_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Landkode))
                AddMessageFromRule(ValidationRuleEnum.adresse_landkode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.adresse_postnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresseValidationEntity.ModelData.Poststed))
                AddMessageFromRule(ValidationRuleEnum.adresse_poststed_utfylt, xPath);

            if (!StringIs4digitNumber(adresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.adresse_postnr_4siffer, xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
