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
            AddValidationRule(MatrikkelValidationEnum.utfylt);
            AddValidationRule(MatrikkelValidationEnum.kommunenummer_utfylt, "kommunenummer");
            AddValidationRule(MatrikkelValidationEnum.gaardsnummer_utfylt, "gaardsnummer");
            AddValidationRule(MatrikkelValidationEnum.bruksnummer_utfylt, "bruksnummer");
            AddValidationRule(MatrikkelValidationEnum.festenummer_utfylt, "festenummer");
            AddValidationRule(MatrikkelValidationEnum.seksjonsnummer_utfylt, "seksjonsnummer");
        }

        private bool ValidateModelExists(MatrikkelValidationEntity modelEntity)
        {
            var xPath = modelEntity.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(modelEntity.ModelData))
            {
                AddMessageFromRule(MatrikkelValidationEnum.utfylt, xPath);
                return false;
            }
            return true;
        }

        public void ValidateEntityFields(MatrikkelValidationEntity matrikkel)
        {
            var xPath = matrikkel.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Kommunenummer))
                AddMessageFromRule(MatrikkelValidationEnum.kommunenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Gaardsnummer))
                AddMessageFromRule(MatrikkelValidationEnum.gaardsnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Bruksnummer))
                AddMessageFromRule(MatrikkelValidationEnum.bruksnummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Festenummer))
                AddMessageFromRule(MatrikkelValidationEnum.festenummer_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Seksjonsnummer))
                AddMessageFromRule(MatrikkelValidationEnum.seksjonsnummer_utfylt, xPath);
        }
    }
}
