using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomsAdresseValidator : EntityValidatorBase
    {
        public EiendomsAdresseValidator() : base()
        {}

        public ValidationResult Validate(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            base.ResetValidationMessages();
            InitializeValidationRules(eiendomsAdresseValidationEntity.DataModelXpath);
            if (ValidateModelExists(eiendomsAdresseValidationEntity))
            {
                ValidateEntityFields(eiendomsAdresseValidationEntity);
            }

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.adresse_utfylt, xPathForEntity);
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje1_utfylt, xPathForEntity, "adresselinje1");
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje2_utfylt, xPathForEntity, "adresselinje2");
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje3_utfylt, xPathForEntity, "adresselinje3");
            AddValidationRule(ValidationRuleEnum.adresse_landkode_utfylt, xPathForEntity, "landkode");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_utfylt, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_poststed_utfylt, xPathForEntity, "poststed");
            AddValidationRule(ValidationRuleEnum.adresse_gatenavn_utfylt, xPathForEntity, "gatenavn");
            AddValidationRule(ValidationRuleEnum.adresse_husnr_utfylt, xPathForEntity, "husnr");
            AddValidationRule(ValidationRuleEnum.adresse_bokstav_utfylt, xPathForEntity, "bokstav");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_4siffer, xPathForEntity, "postnr");
        }
        private bool ValidateModelExists(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            var xPath = eiendomsAdresseValidationEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.adresse_utfylt, xPath);
                return false;
            }
            return true;
        }
        public void ValidateEntityFields(EiendomsAdresseValidationEntity eiendomsAdresseValidationEntity)
        {
            var xPath = eiendomsAdresseValidationEntity.DataModelXpath;



            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje1))
                AddMessageFromRule(ValidationRuleEnum.adresse_adresselinje1_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje2))
                AddMessageFromRule(ValidationRuleEnum.adresse_adresselinje2_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Adresselinje3))
                AddMessageFromRule(ValidationRuleEnum.adresse_adresselinje3_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Landkode))
                AddMessageFromRule(ValidationRuleEnum.adresse_landkode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.adresse_postnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Poststed))
                AddMessageFromRule(ValidationRuleEnum.adresse_poststed_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Gatenavn))
                AddMessageFromRule(ValidationRuleEnum.adresse_gatenavn_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Husnr))
                AddMessageFromRule(ValidationRuleEnum.adresse_husnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresseValidationEntity.ModelData.Bokstav))
                AddMessageFromRule(ValidationRuleEnum.adresse_bokstav_utfylt, xPath);

            if (!StringIs4digitNumber(eiendomsAdresseValidationEntity.ModelData.Postnr))
                AddMessageFromRule(ValidationRuleEnum.adresse_postnr_4siffer, xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if(int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
