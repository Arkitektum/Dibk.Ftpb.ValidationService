﻿using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomByggestedValidator : EntityValidatorBase, IEiendomByggestedValidator
    {
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private readonly IMatrikkelValidator _matrikkelValidator;

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomByggestedValidator(IList<EntityValidatorNode> entityValidatorTree, IEiendomsAdresseValidator eiendomsAdresseValidator, IMatrikkelValidator matrikkelValidator)
            : base(entityValidatorTree)
        {
            _eiendomsAdresseValidator = eiendomsAdresseValidator;
            _matrikkelValidator = matrikkelValidator;
        }


        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.bygningsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.bygningsnummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.bolignummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kommunenavn);
        }

        public ValidationResult Validate(IEnumerable<EiendomValidationEntity> eiendomValidationEntities)
        {
            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntities) || eiendomValidationEntities.Count() == 0)
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {

                foreach (var eiendomValidationEntity in eiendomValidationEntities)
                {
                    ValidateEntityFields(eiendomValidationEntity);

                    var matrikkelValidationResult = _matrikkelValidator.Validate(eiendomValidationEntity.ModelData.Matrikkel);
                    _validationResult.ValidationMessages.AddRange(matrikkelValidationResult.ValidationMessages);

                    var eiendomsAdresseValidationResult = _eiendomsAdresseValidator.Validate(eiendomValidationEntity.ModelData.Adresse);
                    _validationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResult.ValidationMessages);

                    ValidateDataRelations(eiendomValidationEntity);
                }
            }

            return _validationResult;
        }

        private void ValidateDataRelations(EiendomValidationEntity eiendomValidationEntity)
        {
            var xPath = eiendomValidationEntity.DataModelXpath;

            //TODO Implement Matrikkel services, if Arbeidstilsynet har tilgang til Matrikkel API

        }

        private void ValidateEntityFields(EiendomValidationEntity eiendomValidationEntity)
        {
            var xPath = eiendomValidationEntity.DataModelXpath;
            if (!string.IsNullOrEmpty(eiendomValidationEntity?.ModelData?.Bygningsnummer))
            {
                long bygningsnrLong = 0;
                if (!long.TryParse(eiendomValidationEntity.ModelData?.Bygningsnummer, out bygningsnrLong))
                {
                    AddMessageFromRule(ValidationRuleEnum.numerisk, $"{xPath}/{FieldNameEnum.bygningsnummer}", new[] { eiendomValidationEntity.ModelData?.Bygningsnummer });
                }
                else
                {
                    if (bygningsnrLong <= 0)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/{FieldNameEnum.bygningsnummer}", new[] { bygningsnrLong.ToString("N") });
                    }
                }
            }

            if (string.IsNullOrEmpty(eiendomValidationEntity?.ModelData?.Bolignummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.bolignummer}");

            if (string.IsNullOrEmpty(eiendomValidationEntity?.ModelData?.Kommunenavn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.kommunenavn}");
        }
    }
}
