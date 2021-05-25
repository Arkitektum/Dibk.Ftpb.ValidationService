using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomsAdresseValidator : EntityValidatorBase
    {
        public EiendomsAdresseValidator() : base()
        {}

        public ValidationResult Validate(EiendomsAdresseValidationEntity eiendomsAdresse)
        {
            base.ResetValidationMessages();
            InitializeValidationRules(eiendomsAdresse.DataModelXpath);
            
            ValidateEntityFields(eiendomsAdresse);

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule("eiendomsAdresse_adresselinje1_utfylt", xPathForEntity, "adresselinje1");
            AddValidationRule("eiendomsAdresse_adresselinje2_utfylt", xPathForEntity, "adresselinje2");
            AddValidationRule("eiendomsAdresse_adresselinje3_utfylt", xPathForEntity, "adresselinje3");
            AddValidationRule("eiendomsAdresse_landkode_utfylt", xPathForEntity, "landkode");
            AddValidationRule("eiendomsAdresse_postnr_utfylt", xPathForEntity, "postnr");
            AddValidationRule("eiendomsAdresse_poststed_utfylt", xPathForEntity, "poststed");
            AddValidationRule("eiendomsAdresse_gatenavn_utfylt", xPathForEntity, "gatenavn");
            AddValidationRule("eiendomsAdresse_husnr_utfylt", xPathForEntity, "husnr");
            AddValidationRule("eiendomsAdresse_bokstav_utfylt", xPathForEntity, "bokstav");
            AddValidationRule("eiendomsAdresse_postnr_4siffer", xPathForEntity, "postnr");
        }

        public void ValidateEntityFields(EiendomsAdresseValidationEntity eiendomsAdresse)
        {
            var xPath = eiendomsAdresse.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Adresselinje1))
                AddMessageFromRule("eiendomsAdresse_adresselinje1_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Adresselinje2))
                AddMessageFromRule("eiendomsAdresse_adresselinje2_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Adresselinje3))
                AddMessageFromRule("eiendomsAdresse_adresselinje3_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Landkode))
                AddMessageFromRule("eiendomsAdresse_landkode_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Poststed))
                AddMessageFromRule("eiendomsAdresse_poststed_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Gatenavn))
                AddMessageFromRule("eiendomsAdresse_gatenavn_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Husnr))
                AddMessageFromRule("eiendomsAdresse_husnr_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(eiendomsAdresse.ModelData.Bokstav))
                AddMessageFromRule("eiendomsAdresse_bokstav_utfylt", xPath);

            if (!StringIs4digitNumber(eiendomsAdresse.ModelData.Postnr))
                AddMessageFromRule("eiendomsAdresse_postnr_4siffer", xPath);
        }

        private bool StringIs4digitNumber(string input)
        {
            if(int.TryParse(input, out var number))
                return (number >= 0 && number <= 9999);

            return false;
        }
    }
}
