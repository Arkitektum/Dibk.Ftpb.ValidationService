using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidator : EntityValidatorBase, IEnkelAdresseValidator
    {
        //public override string ruleXmlElement { get { return "adresse"; } set { ruleXmlElement = value; } }

        ValidationResult IEntityBaseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EnkelAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId)
            : base(entityValidatorTree, nodeId)
        {
        }


        protected override void InitializeValidationRules()
        {
            AddValidationRule(EnkelAdresseValidationEnums.adresse_utfylt, "adresse");
            AddValidationRule(EnkelAdresseValidationEnums.adresselinje1_utfylt, "adresselinje1");
            AddValidationRule(EnkelAdresseValidationEnums.adresselinje2_utfylt, "adresselinje2");
            AddValidationRule(EnkelAdresseValidationEnums.landkode_utfylt, "landkode");
            AddValidationRule(EnkelAdresseValidationEnums.postnr_utfylt, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            var xpath = enkelAdresse.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.adresse_utfylt, xpath);
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
            var adresse = adresseValidationEntity.ModelData;

            if (Helpers.ObjectIsNullOrEmpty(adresse.Adresselinje1))
                AddMessageFromRule(EnkelAdresseValidationEnums.adresselinje1_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresse.Adresselinje2))
                AddMessageFromRule(EnkelAdresseValidationEnums.adresselinje2_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresse.Landkode))
                AddMessageFromRule(EnkelAdresseValidationEnums.landkode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresse.Postnr))
                AddMessageFromRule(EnkelAdresseValidationEnums.postnr_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(adresse.Poststed))
                AddMessageFromRule(EnkelAdresseValidationEnums.poststed_utfylt, xPath);

            if (HerBurDetGalningar(adresse.Postnr))
                AddMessageFromRule(EnkelAdresseValidationEnums.postnr_kontrollSiffer, xPath);
        }

        private bool HerBurDetGalningar(string input)
        {
            if (int.TryParse(input, out var number))
                return (number >= 0 && number <= 1111);

            return false;
        }
    }
}
