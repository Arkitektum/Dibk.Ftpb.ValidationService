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

            ValidateEntityFields(matrikkel);

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule("kommunenummer_utfylt", xPathForEntity, "kommunenummer");
            AddValidationRule("gaardsnummer_utfylt", xPathForEntity, "gaardsnummer");
            AddValidationRule("bruksnummer_utfylt", xPathForEntity, "bruksnummer");
            AddValidationRule("festenummer_utfylt", xPathForEntity, "festenummer");
            AddValidationRule("seksjonsnummer_utfylt", xPathForEntity, "seksjonsnummer");
        }

        public void ValidateEntityFields(MatrikkelValidationEntity matrikkel)
        {
            var xPath = matrikkel.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Kommunenummer))
                AddMessageFromRule("kommunenummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Gaardsnummer))
                AddMessageFromRule("gaardsnummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Bruksnummer))
                AddMessageFromRule("bruksnummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Festenummer))
                AddMessageFromRule("festenummer_utfylt", xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Seksjonsnummer))
                AddMessageFromRule("seksjonsnummer_utfylt", xPath);
        }
    }
}
