using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class MatrikkelValidator : EntityValidatorBase
    {
        public MatrikkelValidator() : base()
        {}
        public ValidationResult Validate(MatrikkelValidationEntity matrikkel)
        {
            base.ResetValidationMessages();
            InitializeValidationRules(matrikkel.DataModelXpath);
            
            if (ValidateModelExists(matrikkel))
            {
                ValidateEntityFields(matrikkel);
            }

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.eiendomsIdentifikasjon_utfylt, xPathForEntity);
            AddValidationRule(ValidationRuleEnum.eiendomsIdentifikasjon_kommunenummer_utfylt, xPathForEntity, "kommunenummer");
            AddValidationRule(ValidationRuleEnum.eiendomsIdentifikasjon_gaardsnummer_utfylt, xPathForEntity, "gaardsnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsIdentifikasjon_bruksnummer_utfylt, xPathForEntity, "bruksnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsIdentifikasjon_festenummer_utfylt, xPathForEntity, "festenummer");
            AddValidationRule(ValidationRuleEnum.eiendomsIdentifikasjon_seksjonsnummer_utfylt, xPathForEntity, "seksjonsnummer");
        }

        private bool ValidateModelExists(MatrikkelValidationEntity modelEntity)
        {
            var xPath = modelEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(modelEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.eiendomsIdentifikasjon_utfylt, xPath);
                return false;
            }
            return true;
        }

        public void ValidateEntityFields(MatrikkelValidationEntity matrikkel)
        {
            var xPath = matrikkel.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Kommunenummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsIdentifikasjon_kommunenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Gaardsnummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsIdentifikasjon_gaardsnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Bruksnummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsIdentifikasjon_bruksnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Festenummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsIdentifikasjon_festenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Seksjonsnummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsIdentifikasjon_seksjonsnummer_utfylt, xPath);
        }
    }
}
