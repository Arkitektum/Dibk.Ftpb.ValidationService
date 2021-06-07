using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidator : EntityValidatorBase, IEnkelAdresseValidator
    {
        public override string ruleXmlElement { get { return "/adresse"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EnkelAdresseValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator) 
            : base(entityValidatorOrchestrator, parentValidator)
        {
            InitializeValidationRules(EntityXPath);
        }
        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.adresse_utfylt, xPathForEntity);
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje1_utfylt, xPathForEntity, "adresselinje1");
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje2_utfylt, xPathForEntity, "adresselinje2");
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje3_utfylt, xPathForEntity, "adresselinje3");
            AddValidationRule(ValidationRuleEnum.adresse_landkode_utfylt, xPathForEntity, "landkode");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_utfylt, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_kontrollsiffer, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_ugyldig, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_stemmerIkke, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_ikke_validert, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_4siffer, xPathForEntity, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.adresse_utfylt);
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
