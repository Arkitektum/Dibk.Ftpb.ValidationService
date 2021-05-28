using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidator : EntityValidatorBase
    {

        public EnkelAdresseValidator():base()
        {}
        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.adresse_utfylt, xPathForEntity);
            AddValidationRule(ValidationRuleEnum.adresse_adresselinje1_utfylt, xPathForEntity, "adresselinje1");
            AddValidationRule(ValidationRuleEnum.adresse_landkode_utfylt, xPathForEntity, "landkode");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_utfylt, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_kontrollsiffer, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_ugyldig, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_stemmerIkke, xPathForEntity, "postnr");
            AddValidationRule(ValidationRuleEnum.adresse_postnr_ikke_validert, xPathForEntity, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            InitializeValidationRules(enkelAdresse.DataModelXpath);
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.adresse_utfylt);
            }
            else
            {
                //
            }

            return _validationResult;
        }

    }
}
