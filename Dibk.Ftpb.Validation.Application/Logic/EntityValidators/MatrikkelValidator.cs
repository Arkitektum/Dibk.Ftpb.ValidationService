using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class MatrikkelValidator : EntityValidatorBase
    {
        public override string ruleXmlElement { get { return "/eiendomsidentifikasjon"; } }

        public MatrikkelValidator(string parentXPath) : base(parentXPath)
        {
            InitializeValidationRules(EntityXPath);
        }

        public ValidationResult Validate(MatrikkelValidationEntity matrikkel)
        {
            base.ResetValidationMessages();
            //InitializeValidationRules(matrikkel.DataModelXpath);

            if (ValidateModelExists(matrikkel))
            {
                ValidateEntityFields(matrikkel);
            }

            return ValidationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_utfylt, xPathForEntity);
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_kommunenummer_utfylt, xPathForEntity, "kommunenummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_gaardsnummer_utfylt, xPathForEntity, "gaardsnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_bruksnummer_utfylt, xPathForEntity, "bruksnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_festenummer_utfylt, xPathForEntity, "festenummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_seksjonsnummer_utfylt, xPathForEntity, "seksjonsnummer");
        }

        private bool ValidateModelExists(MatrikkelValidationEntity modelEntity)
        {
            var xPath = modelEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(modelEntity.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.eiendomsidentifikasjon_utfylt, xPath);
                return false;
            }
            return true;
        }

        public void ValidateEntityFields(MatrikkelValidationEntity matrikkel)
        {
            var xPath = matrikkel.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Kommunenummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsidentifikasjon_kommunenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Gaardsnummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsidentifikasjon_gaardsnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Bruksnummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsidentifikasjon_bruksnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Festenummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsidentifikasjon_festenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Seksjonsnummer))
                AddMessageFromRule(ValidationRuleEnum.eiendomsidentifikasjon_seksjonsnummer_utfylt, xPath);
        }
    }
}
