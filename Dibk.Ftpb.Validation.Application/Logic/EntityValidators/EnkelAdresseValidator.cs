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
        private string _xPath;
        private const string _entityName = "adresse";

        public EnkelAdresseValidator(string parentContext)
        {
            _xPath = $"{parentContext}/{_entityName}";
            InitializeValidationRules();
        }
        public override void InitializeValidationRules(string context = null)
        {
            AddValidationRule("adresse_utfylt", _xPath);
            AddValidationRule("EnkelAdress_adresseLinje1_Utfylt", _xPath, "adresselinje1");
            AddValidationRule("enkelAdress_landkode_utfylt", _xPath, "landkode");
            AddValidationRule("enkelAdress_postnr_utfylt", _xPath, "postnr");
            AddValidationRule("enkelAdress_postnr_kontrollSiffer", _xPath, "postnr");
            AddValidationRule("enkelAdress_postnr_ugyldig", _xPath, "postnr");
            AddValidationRule("enkelAdress_postnr_stemmerIkke", _xPath, "postnr");
            AddValidationRule("enkelAdress_postnr_ikkeValidert", _xPath, "postnr");
        }

        public ValidationResult Validate(string xPath = null, EnkelAdresse enkelAdresse = null)
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
