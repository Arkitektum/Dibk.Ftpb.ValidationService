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
        private readonly IKodelisteValidator _anleggstypeValidator;
        private readonly IKodelisteValidator _naeringsgruppeValidator;
        private readonly IKodelisteValidator _bygningstypeValidator;
        private readonly IKodelisteValidator _tiltaksformaalValidator;

        public FormaaltypeValidator(IList<EntityValidatorNode> entityValidatorTree,
            IKodelisteValidator anleggstypeValidator, IKodelisteValidator naeringsgruppeValidator, IKodelisteValidator bygningstypeValidator, IKodelisteValidator tiltaksformaalValidator)
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
            ValidateEntityFields(formaaltypeValEntity);

            if (!Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity))
            {

                if (!Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity.Anleggstype))
                {
                    var anleggstypeValidationResult = _anleggstypeValidator.Validate(formaaltypeValEntity?.Anleggstype);
                    UpdateValidationResultWithSubValidations(anleggstypeValidationResult);
                }

                if (!Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity.Naeringsgruppe))
                {
                    var naeringsgruppeValidationResult = _naeringsgruppeValidator.Validate(formaaltypeValEntity?.Naeringsgruppe);
                    UpdateValidationResultWithSubValidations(naeringsgruppeValidationResult);
                }

                if (!Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity.Bygningstype))
                {
                    var bygningstypeValidationResult = _bygningstypeValidator.Validate(formaaltypeValEntity?.Bygningstype);
                    UpdateValidationResultWithSubValidations(bygningstypeValidationResult);
                }

                if (!Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity?.Tiltaksformaal))
                {

                    var index = GetArrayIndex(formaaltypeValEntity?.Tiltaksformaal);
                    for (int i = 0; i < index; i++)
                    {
                        var tiltaksformaal = formaaltypeValEntity?.Tiltaksformaal[i];

                        var tiltaksformaalValidationResult = _tiltaksformaalValidator.Validate(tiltaksformaal);
                        UpdateValidationResultWithSubValidations(tiltaksformaalValidationResult,i);

                        var tiltaksformaalXpath = Helpers.ReplaceCurlyBracketInXPath(i, $"{_tiltaksformaalValidator._entityXPath}/{FieldNameEnum.kodeverdi}");
                        if (!IsAnyValidationMessagesWithXpath(tiltaksformaalXpath))
                        {
                            if (tiltaksformaal.Kodeverdi is "Annet")
                            {
                                if (string.IsNullOrEmpty(formaaltypeValEntity.BeskrivPlanlagtFormaal))
                                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.beskrivPlanlagtFormaal, null, tiltaksformaalXpath);
                            }
                        }
                    }
                }
            }

            return _validationResult;
        }
        public void ValidateEntityFields(FormaaltypeValidationEntity formaaltypeValEntity)
        {
            if (Helpers.ObjectIsNullOrEmpty(formaaltypeValEntity))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                //               
            }
        }
    }
}
