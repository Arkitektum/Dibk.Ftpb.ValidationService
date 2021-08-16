using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class MatrikkelValidator : EntityValidatorBase, IMatrikkelValidator
    {
        private readonly IMunicipalityValidator _municipalityValidator;
        ValidationResult IMatrikkelValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public MatrikkelValidator(IList<EntityValidatorNode> entityValidatorTree, int? nodeId, IMunicipalityValidator municipalityValidator)
            : base(entityValidatorTree, nodeId)
        {
            _municipalityValidator = municipalityValidator;
        }
        protected override void InitializeValidationRules()
        {
            //AddValidationRule(MatrikkelValidationEnum.utfylt);
            //AddValidationRule(MatrikkelValidationEnum.kommunenummer_utfylt, "kommunenummer");
            //AddValidationRule(MatrikkelValidationEnum.kommunenummer_ugyldig, "kommunenummer");   OBS <---- konsistens
            //AddValidationRule(MatrikkelValidationEnum.kommunenummer_gammel, "kommunenummer");
            //AddValidationRule(MatrikkelValidationEnum.gaardsnummer_utfylt, "gaardsnummer");
            //AddValidationRule(MatrikkelValidationEnum.bruksnummer_utfylt, "bruksnummer");
            //AddValidationRule(MatrikkelValidationEnum.festenummer_utfylt, "festenummer");
            //AddValidationRule(MatrikkelValidationEnum.seksjonsnummer_utfylt, "seksjonsnummer");

            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, "kommunenummer");
            AddValidationRule(ValidationRuleEnum.gyldig, "kommunenummer");
            AddValidationRule(ValidationRuleEnum.kommunenummer_utgått, "kommunenummer");
            AddValidationRule(ValidationRuleEnum.utfylt, "gaardsnummer");
            AddValidationRule(ValidationRuleEnum.utfylt, "bruksnummer");
            AddValidationRule(ValidationRuleEnum.utfylt, "festenummer");
            AddValidationRule(ValidationRuleEnum.utfylt, "seksjonsnummer");


        }

        public ValidationResult Validate(MatrikkelValidationEntity matrikkel)
        {
            base.ResetValidationMessages();
            var xPath = matrikkel.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(matrikkel?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);

            }
            else
            {
                ValidateEntityFields(matrikkel);
            }



            return _validationResult;
        }

        public void ValidateEntityFields(MatrikkelValidationEntity matrikkel)
        {

            var xPath = matrikkel.DataModelXpath;

            var kommunenummer = matrikkel?.ModelData?.Kommunenummer;
            var kommunenummerStatus = _municipalityValidator.Validate_kommunenummerStatus(kommunenummer);
            if (kommunenummerStatus.Status != MunicipalityValidationEnum.Ok)
            {
                switch (kommunenummerStatus.Status)
                {
                    case MunicipalityValidationEnum.Empty:
                        AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);
                        break;
                    case MunicipalityValidationEnum.Invalid:
                        AddMessageFromRule(ValidationRuleEnum.gyldig, xPath, new[] { kommunenummer });
                        break;
                    case MunicipalityValidationEnum.Expired:
                        AddMessageFromRule(ValidationRuleEnum.kommunenummer_utgått, xPath, new[] { kommunenummer, kommunenummerStatus.Status.ToString() });
                        break;
                }
            }


            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Gaardsnummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Bruksnummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Festenummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Seksjonsnummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);
        }
    }
}
