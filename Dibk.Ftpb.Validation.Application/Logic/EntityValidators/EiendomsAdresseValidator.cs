﻿using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomsAdresseValidator : EntityValidatorBase, IEiendomsAdresseValidator
    {
        //public override string ruleXmlElement { get { return "adresse"; } set { ruleXmlElement = value; } }

        ValidationResult IEiendomsAdresseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        //public EiendomsAdresseValidator(FormValidatorConfiguration formValidatorConfiguration, EntityValidatorEnum parentValidator) 
        //    : base(formValidatorConfiguration, parentValidator)
        //{
        //}

        public EiendomsAdresseValidator(IList<EntityValidatorNode> entityValidationGroup) 
            : base(entityValidationGroup)
        {
        }


        //IList<EntityValidatorNode> entityValidationGroup
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
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_utfylt);
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_adresselinje2_utfylt, "adresselinje2");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_adresselinje3_utfylt, "adresselinje3");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_landkode_utfylt, "landkode");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_postnr_utfylt, "postnr");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_poststed_utfylt, "poststed");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_gatenavn_utfylt, "gatenavn");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_husnr_utfylt, "husnr");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_bokstav_utfylt, "bokstav");
            AddValidationRule(ValidationRuleEnum.eiendomsadresse_postnr_4siffer, "postnr");
        }
        private bool ValidateModelExists(EiendomsAdresseValidationEntity modelEntity)
        {
            var xPath = modelEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(modelEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_utfylt, xPath);
                return false;
            }
            return true;
        }
        private void ValidateEntityFields(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            var xPath = eiendomsAdresseValidationEntity.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje1))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_adresselinje1_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje2))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_adresselinje2_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje3))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_adresselinje3_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Landkode))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_landkode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_postnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Poststed))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_poststed_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Gatenavn))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_gatenavn_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Husnr))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_husnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Bokstav))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_bokstav_utfylt, xPath);

            if (!StringIs4digitNumber(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.eiendomsadresse_postnr_4siffer, xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
