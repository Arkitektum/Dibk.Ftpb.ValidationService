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

        public ValidationResult Validate(Matrikkel matrikkel)
        {
            base.ResetValidationMessages();

            if (Helpers.ObjectIsNullOrEmpty(matrikkel))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                var kommunenummer = matrikkel?.Kommunenummer;
                var kommunenummerStatus = _municipalityValidator.Validate_kommunenummerStatus(kommunenummer);
                if (kommunenummerStatus.Status != MunicipalityValidationEnum.Ok)
                {
                    switch (kommunenummerStatus.Status)
                    {
                        case MunicipalityValidationEnum.Empty:
                            AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.kommunenummer);
                            break;
                        case MunicipalityValidationEnum.Invalid:
                        case MunicipalityValidationEnum.TooSoon:
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.kommunenummer, new[] { kommunenummer });
                            break;
                        case MunicipalityValidationEnum.Expired:
                            AddMessageFromRule(ValidationRuleEnum.utgått, FieldNameEnum.kommunenummer, new[] { kommunenummer, kommunenummerStatus.Status.ToString() });
                            break;
                    }
                }

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.Gaardsnummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.gaardsnummer);

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.Bruksnummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.bruksnummer);

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.Festenummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.festenummer);

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.Seksjonsnummer))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.seksjonsnummer);
            }
            return _validationResult;
        }
    }
}
