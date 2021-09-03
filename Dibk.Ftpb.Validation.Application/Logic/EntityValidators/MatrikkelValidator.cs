using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class MatrikkelValidator : EntityValidatorBase, IMatrikkelValidator
    {
        private readonly IMunicipalityValidator _municipalityValidator;
        ValidationResult IMatrikkelValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public MatrikkelValidator(IList<EntityValidatorNode> entityValidatorTree, IMunicipalityValidator municipalityValidator)
            : base(entityValidatorTree)
        {
            _municipalityValidator = municipalityValidator;
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kommunenummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.kommunenummer);
            AddValidationRule(ValidationRuleEnum.utgått, FieldNameEnum.kommunenummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.gaardsnummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.bruksnummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.festenummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.seksjonsnummer);


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
                var kommunenummer = matrikkel?.ModelData?.Kommunenummer;
                var kommunenummerStatus = _municipalityValidator.Validate_kommunenummerStatus(kommunenummer);
                if (kommunenummerStatus.Status != MunicipalityValidationEnum.Ok)
                {
                    switch (kommunenummerStatus.Status)
                    {
                        case MunicipalityValidationEnum.Empty:
                            AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.kommunenummer}");
                            break;
                        case MunicipalityValidationEnum.Invalid:
                            AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/{FieldNameEnum.kommunenummer}", new[] { kommunenummer });
                            break;
                        case MunicipalityValidationEnum.Expired:
                            AddMessageFromRule(ValidationRuleEnum.utgått, $"{xPath}/{FieldNameEnum.kommunenummer}", new[] { kommunenummer, kommunenummerStatus.Status.ToString() });
                            break;
                    }
                }

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Gaardsnummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.gaardsnummer}");

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Bruksnummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.bruksnummer}");

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Festenummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.festenummer}");

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.ModelData.Seksjonsnummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.seksjonsnummer}");
            }
            return _validationResult;
        }
    }
}
