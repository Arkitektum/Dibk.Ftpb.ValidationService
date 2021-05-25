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
            AddValidationRule("adresse_utfylt", xPathForEntity);
            AddValidationRule("enkelAdress_adresseLinje1_Utfylt", xPathForEntity, "adresselinje1");
            AddValidationRule("enkelAdress_landkode_utfylt", xPathForEntity, "landkode");
            AddValidationRule("enkelAdress_postnr_utfylt", xPathForEntity, "postnr");
            AddValidationRule("enkelAdress_postnr_kontrollSiffer", xPathForEntity, "postnr");
            AddValidationRule("enkelAdress_postnr_ugyldig", xPathForEntity, "postnr");
            AddValidationRule("enkelAdress_postnr_stemmerIkke", xPathForEntity, "postnr");
            AddValidationRule("enkelAdress_postnr_ikkeValidert", xPathForEntity, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            InitializeValidationRules(enkelAdresse.DataModelXpath);
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule("adresse_Utfylt");
            }
            else
            {
                //
            }

            return _validationResult;
        }

    }
}
