using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomByggestedValidator : EntityValidatorBase, IEiendomByggestedValidator
    {
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private readonly IMatrikkelValidator _matrikkelValidator;

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomByggestedValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IEiendomsAdresseValidator eiendomsAdresseValidator, IMatrikkelValidator matrikkelValidator)
            : base(entityValidatorTree, nodeId)
        {
            _eiendomsAdresseValidator = eiendomsAdresseValidator;
            _matrikkelValidator = matrikkelValidator;
        }


        protected override void InitializeValidationRules()
        {

            //AddValidationRule(EiendomValidationEnums.utfylt, "adresse");

            AddValidationRule(ValidationRuleEnum.utfylt, null);
            AddValidationRule(ValidationRuleEnum.utfylt, "bygningsnummer");
            AddValidationRule(ValidationRuleEnum.gyldig, "bygningsnummer");
            AddValidationRule(ValidationRuleEnum.utfylt, "bolignummer");
            AddValidationRule(ValidationRuleEnum.utfylt, "kommunenavn");
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
            if (!Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Bygningsnummer))
            {
                long bygningsnrLong = 0;
                if (!long.TryParse(eiendomValidationEntity.ModelData?.Bygningsnummer, out bygningsnrLong))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/bygningsnummer", new[] { eiendomValidationEntity.ModelData?.Bygningsnummer });
                }
                else
                {
                    if (bygningsnrLong <= 0)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/bygningsnummer", new[] { bygningsnrLong.ToString("N") });
                    }
                }
            }

            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Bolignummer))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/bolignummer");

            if (Helpers.ObjectIsNullOrEmpty(eiendomValidationEntity.ModelData?.Kommunenavn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/kommunenavn");
        }
    }
}
