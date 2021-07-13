using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class BeskrivelseAvTiltakValidator : EntityValidatorBase, IBeskrivelseAvTiltakValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly FormaaltypeValidator _formaaltypeValidator;
        private readonly TiltakstypeValidator _tiltakstypeValidator;

        public BeskrivelseAvTiltakValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, FormaaltypeValidator formaaltypeValidator, TiltakstypeValidator tiltakstypeValidator)
            : base(entityValidatorTree, nodeId)
        {
            _formaaltypeValidator = formaaltypeValidator;
            _tiltakstypeValidator = tiltakstypeValidator;
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(BeskrivelseAvTiltakValidationEnum.utfylt, null);
            AddValidationRule(BeskrivelseAvTiltakValidationEnum.bra_utfylt, "BRA");
        }

        public ValidationResult Validate(BeskrivelseAvTiltakValidationEntity beskrivelseAvTiltakValidationEntity = null)
        {
            var xpath = beskrivelseAvTiltakValidationEntity?.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltakValidationEntity?.ModelData))
            {
                AddMessageFromRule(BeskrivelseAvTiltakValidationEnum.utfylt, xpath);
            }
            else
            {
                var formaaltypeValidationResult = _formaaltypeValidator.Validate(beskrivelseAvTiltakValidationEntity?.ModelData?.Formaaltype);
                UpdateValidationResultWithSubValidations(formaaltypeValidationResult);

                if (string.IsNullOrEmpty(beskrivelseAvTiltakValidationEntity?.ModelData?.BRA))
                {
                    AddMessageFromRule(BeskrivelseAvTiltakValidationEnum.bra_utfylt, xpath);

                }
                
                if (!Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltakValidationEntity?.ModelData?.Tiltakstype) && !beskrivelseAvTiltakValidationEntity.ModelData.Tiltakstype.Any())
                {
                    foreach (var tiltakstypeValidationEntity in beskrivelseAvTiltakValidationEntity.ModelData.Tiltakstype)
                    {
                        var tiltakstypeValidationResult = _tiltakstypeValidator.Validate(tiltakstypeValidationEntity);
                        UpdateValidationResultWithSubValidations(tiltakstypeValidationResult);
                    }
                }
            }
            return ValidationResult;
        }
    }
}
