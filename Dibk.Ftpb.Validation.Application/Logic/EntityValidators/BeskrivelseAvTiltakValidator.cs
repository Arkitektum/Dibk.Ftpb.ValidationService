using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using System;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class BeskrivelseAvTiltakValidator : EntityValidatorBase, IBeskrivelseAvTiltakValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        private readonly IFormaaltypeValidator _formaaltypeValidator;
        private readonly IKodelisteValidator _tiltakstypeValidator;

        public BeskrivelseAvTiltakValidator(IList<EntityValidatorNode> entityValidatorTree, IFormaaltypeValidator formaaltypeValidator, IKodelisteValidator tiltakstypeValidator)
            : base(entityValidatorTree)
        {
            _formaaltypeValidator = formaaltypeValidator;
            _tiltakstypeValidator = tiltakstypeValidator;
        }
        protected override void InitializeValidationRules()
        {
            //AddValidationRule(BeskrivelseAvTiltakValidationEnum.utfylt, null);
            //AddValidationRule(BeskrivelseAvTiltakValidationEnum.bra_utfylt, "BRA");

            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.BRA);


        }

        public ValidationResult Validate(BeskrivelseAvTiltakValidationEntity beskrivelseAvTiltakValidationEntity = null)
        {
            var xpath = beskrivelseAvTiltakValidationEntity?.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltakValidationEntity?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
            }
            else
            {
                var formaaltypeValidationResult = _formaaltypeValidator.Validate(beskrivelseAvTiltakValidationEntity?.ModelData?.Formaaltype);
                UpdateValidationResultWithSubValidations(formaaltypeValidationResult);

                if (string.IsNullOrEmpty(beskrivelseAvTiltakValidationEntity?.ModelData?.BRA))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/{FieldNameEnum.BRA}");
                }

                var tiltakstypes = beskrivelseAvTiltakValidationEntity?.ModelData?.Tiltakstype?.ToArray();
                int index = Helpers.ObjectIsNullOrEmpty(beskrivelseAvTiltakValidationEntity?.ModelData?.Tiltakstype) ? 1 : (tiltakstypes ?? Array.Empty<KodelisteValidationEntity>()).Count();

                for (int i = 0; i < index; i++)
                {
                    var tiltakstype = Helpers.ObjectIsNullOrEmpty(tiltakstypes) ? null: tiltakstypes[i];

                    var tiltakstypeValidationResult = _tiltakstypeValidator.Validate(tiltakstype);
                    UpdateValidationResultWithSubValidations(tiltakstypeValidationResult);
                }

            }
            return ValidationResult;
        }
    }
}
