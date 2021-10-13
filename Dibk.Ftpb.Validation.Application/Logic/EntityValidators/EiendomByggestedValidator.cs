using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
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

        public ValidationResult Validate(IEnumerable<Eiendom> eiendomValidationEntities)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Validate(Eiendom eiendom)
        {
            base.ResetValidationMessages();

            if (Helpers.ObjectIsNullOrEmpty(eiendom))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                ValidateEntityFields(eiendom);

                var matrikkelValidationResult = _matrikkelValidator.Validate(eiendom.Matrikkel);
                _validationResult.ValidationMessages.AddRange(matrikkelValidationResult.ValidationMessages);

                var eiendomsAdresseValidationResult = _eiendomsAdresseValidator.Validate(eiendom.Adresse);
                _validationResult.ValidationMessages.AddRange(eiendomsAdresseValidationResult.ValidationMessages);

                ValidateDataRelations(eiendom);
            }

            return _validationResult;
        }

        private void ValidateDataRelations(Eiendom eiendom)
        {
            //TODO Implement Matrikkel services, if Arbeidstilsynet har tilgang til Matrikkel API
        }

        private void ValidateEntityFields(Eiendom eiendom)
        {
            if (!string.IsNullOrEmpty(eiendom?.Bygningsnummer))
            {
                long bygningsnrLong = 0;
                if (!long.TryParse(eiendom.Bygningsnummer, out bygningsnrLong))
                {
                    AddMessageFromRule(ValidationRuleEnum.numerisk, FieldNameEnum.bygningsnummer, new[] { eiendom.Bygningsnummer });
                }
                else
                {
                    if (bygningsnrLong <= 0)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.bygningsnummer, new[] { bygningsnrLong.ToString("N") });
                    }
                }
            }

            if (string.IsNullOrEmpty(eiendom?.Bolignummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt,FieldNameEnum.bolignummer);

            if (string.IsNullOrEmpty(eiendom?.Kommunenavn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.kommunenavn);
        }
    }
}
