using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class FormaaltypeValidator : EntityValidatorBase, IFormaaltypeValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }
        private readonly AnleggstypeValidator _anleggstypeValidator;
        private readonly NaeringsgruppeValidator _naeringsgruppeValidator;
        private readonly BygningstypeValidator _bygningstypeValidator;
        private readonly TiltaksformaalValidator _tiltaksformaalValidator;

        public FormaaltypeValidator(IList<EntityValidatorNode> entityValidatorTree,
            AnleggstypeValidator anleggstypeValidator, NaeringsgruppeValidator naeringsgruppeValidator, BygningstypeValidator bygningstypeValidator, TiltaksformaalValidator tiltaksformaalValidator)
            : base(entityValidatorTree)
        {
            _anleggstypeValidator = anleggstypeValidator;
            _naeringsgruppeValidator = naeringsgruppeValidator;
            _bygningstypeValidator = bygningstypeValidator;
            _tiltaksformaalValidator = tiltaksformaalValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.beskrivPlanlagtFormaal);
        }

        public ValidationResult Validate(FormaaltypeValidationEntity formaaltypeValEntity = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity?.ModelData))
            {
                //AddMessageFromRule(ValidationRuleEnum.beskrivelseAvTiltak_formaaltype_utfylt, formaaltypeValEntity?.DataModelXpath);
                AddMessageFromRule(ValidationRuleEnum.utfylt, formaaltypeValEntity?.DataModelXpath);
            }
            else
            {
                var anleggstypeValidationResult = _anleggstypeValidator.Validate(formaaltypeValEntity?.ModelData?.Anleggstype);
                UpdateValidationResultWithSubValidations(anleggstypeValidationResult);

                var naeringsgruppeValidationResult = _naeringsgruppeValidator.Validate(formaaltypeValEntity?.ModelData?.Naeringsgruppe);
                UpdateValidationResultWithSubValidations(naeringsgruppeValidationResult);

                var bygningstypeValidationResult = _bygningstypeValidator.Validate(formaaltypeValEntity?.ModelData?.Bygningstype);
                UpdateValidationResultWithSubValidations(bygningstypeValidationResult);

                if (!Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity?.ModelData?.Tiltaksformaal))
                {
                    foreach (var tiltaksformaal in formaaltypeValEntity.ModelData.Tiltaksformaal)
                    {
                        var tiltaksformaalValidationResult = _tiltaksformaalValidator.Validate(tiltaksformaal);
                        UpdateValidationResultWithSubValidations(tiltaksformaalValidationResult);
                    }
                }
                ValidateEntityFields(formaaltypeValEntity);
            }

            return _validationResult;
        }
        private void ValidateEntityFields(FormaaltypeValidationEntity formaaltypeValEntity)
        {
            var xPath = formaaltypeValEntity.DataModelXpath;
            var formaaltype = formaaltypeValEntity.ModelData;

            if (string.IsNullOrEmpty(formaaltype.BeskrivPlanlagtFormaal))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.beskrivPlanlagtFormaal}");
            }
        }
    }
}
