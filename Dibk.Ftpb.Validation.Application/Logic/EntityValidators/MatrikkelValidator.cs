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
            AddValidationRule(MatrikkelValidationEnum.utfylt);
            AddValidationRule(MatrikkelValidationEnum.kommunenummer_utfylt, "kommunenummer");
            AddValidationRule(MatrikkelValidationEnum.kommunenummer_ugyldig, "kommunenummer");
            AddValidationRule(MatrikkelValidationEnum.kommunenummer_gammel, "kommunenummer");
            AddValidationRule(MatrikkelValidationEnum.gaardsnummer_utfylt, "gaardsnummer");
            AddValidationRule(MatrikkelValidationEnum.bruksnummer_utfylt, "bruksnummer");
            AddValidationRule(MatrikkelValidationEnum.festenummer_utfylt, "festenummer");
            AddValidationRule(MatrikkelValidationEnum.seksjonsnummer_utfylt, "seksjonsnummer");
        }

        public ValidationResult Validate(MatrikkelValidationEntity matrikkel)
        {
            base.ResetValidationMessages();
            var xPath = matrikkel.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(matrikkel?.ModelData))
            {
                AddMessageFromRule(MatrikkelValidationEnum.utfylt, xPath);

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
                        AddMessageFromRule(MatrikkelValidationEnum.kommunenummer_utfylt, xPath);
                        break;
                    case MunicipalityValidationEnum.Invalid:
                        AddMessageFromRule(MatrikkelValidationEnum.kommunenummer_ugyldig, xPath, new[] { kommunenummer });
                        break;
                    case MunicipalityValidationEnum.Expired:
                        AddMessageFromRule(MatrikkelValidationEnum.kommunenummer_gammel, xPath, new[] { kommunenummer, kommunenummerStatus.Status.ToString() });
                        break;
                }
            }


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
