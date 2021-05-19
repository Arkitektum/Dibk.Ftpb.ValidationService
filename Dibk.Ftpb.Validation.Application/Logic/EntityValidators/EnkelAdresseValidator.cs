using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidator : EntityValidatorBase
    {

        public EnkelAdresseValidator(string parentContext):base(parentContext,"adresse")
        {
            InitializeValidationRules();
        }
        public sealed override void InitializeValidationRules()
        {
            AddValidationRule("adresse_utfylt", EntityXPath);
            AddValidationRule("enkelAdress_adresseLinje1_Utfylt", EntityXPath, "adresselinje1");
            AddValidationRule("enkelAdress_landkode_utfylt", EntityXPath, "landkode");
            AddValidationRule("enkelAdress_postnr_utfylt", EntityXPath, "postnr");
            AddValidationRule("enkelAdress_postnr_kontrollSiffer", EntityXPath, "postnr");
            AddValidationRule("enkelAdress_postnr_ugyldig", EntityXPath, "postnr");
            AddValidationRule("enkelAdress_postnr_stemmerIkke", EntityXPath, "postnr");
            AddValidationRule("enkelAdress_postnr_ikkeValidert", EntityXPath, "postnr");
        }

        public ValidationResult Validate(EnkelAdresse enkelAdresse = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule("adresse_Utfylt");
            }
            else
            {
                //
            }

            return ValidationResult;
        }

    }
}
