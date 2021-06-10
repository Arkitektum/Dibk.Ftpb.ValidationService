using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class MatrikkelValidator : EntityValidatorBase, IMatrikkelValidator
    {
        public override string ruleXmlElement { get { return "eiendomsidentifikasjon"; } set { ruleXmlElement = value; } }

        ValidationResult IMatrikkelValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public MatrikkelValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator) 
            : base(entityValidatorOrchestrator, parentValidator)
        {
        }

        public ValidationResult Validate(MatrikkelValidationEntity matrikkel)
        {
            base.ResetValidationMessages();

            if (ValidateModelExists(matrikkel))
            {
                ValidateEntityFields(matrikkel);
            }

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathToEntity)
        {
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_utfylt, xPathToEntity);
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_kommunenummer_utfylt, xPathToEntity, "kommunenummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_gaardsnummer_utfylt, xPathToEntity, "gaardsnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_bruksnummer_utfylt, xPathToEntity, "bruksnummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_festenummer_utfylt, xPathToEntity, "festenummer");
            AddValidationRule(ValidationRuleEnum.eiendomsidentifikasjon_seksjonsnummer_utfylt, xPathToEntity, "seksjonsnummer");
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
