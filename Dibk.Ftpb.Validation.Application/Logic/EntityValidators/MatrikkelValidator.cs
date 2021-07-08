using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class MatrikkelValidator : EntityValidatorBase, IMatrikkelValidator
    {
        ValidationResult IMatrikkelValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public MatrikkelValidator(IList<EntityValidatorNode> entityValidatorTree, int? nodeId = null)
            : base(entityValidatorTree, nodeId)
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

        protected override void InitializeValidationRules()
        {
            AddValidationRule(MatrikkelValidationEnums.utfylt);
            AddValidationRule(MatrikkelValidationEnums.kommunenummer_utfylt, "kommunenummer");
            AddValidationRule(MatrikkelValidationEnums.gaardsnummer_utfylt, "gaardsnummer");
            AddValidationRule(MatrikkelValidationEnums.bruksnummer_utfylt, "bruksnummer");
            AddValidationRule(MatrikkelValidationEnums.festenummer_utfylt, "festenummer");
            AddValidationRule(MatrikkelValidationEnums.seksjonsnummer_utfylt, "seksjonsnummer");
        }

        private bool ValidateModelExists(MatrikkelValidationEntity modelEntity)
        {
            var xPath = modelEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(modelEntity.ModelData))
            {
                AddMessageFromRule(MatrikkelValidationEnums.utfylt, xPath);
                return false;
            }
            return true;
        }

        public void ValidateEntityFields(MatrikkelValidationEntity matrikkel)
        {
            var xPath = matrikkel.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Kommunenummer))
                AddMessageFromRule(MatrikkelValidationEnums.kommunenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Gaardsnummer))
                AddMessageFromRule(MatrikkelValidationEnums.gaardsnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Bruksnummer))
                AddMessageFromRule(MatrikkelValidationEnums.bruksnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Festenummer))
                AddMessageFromRule(MatrikkelValidationEnums.festenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Seksjonsnummer))
                AddMessageFromRule(MatrikkelValidationEnums.seksjonsnummer_utfylt, xPath);
        }
    }
}
