using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SjekklistekravValidator : EntityValidatorBase, ISjekklistekravValidator
    {
        private readonly ISjekklistepunktValidator _sjekklistepunktValidator;

        //public override string ruleXmlElement { get { return "krav{0}"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; }

        //public SjekklistekravValidator(FormValidatorConfiguration formValidatorConfiguration, ISjekklistepunktValidator sjekklistepunktValidator)
        //    : base(formValidatorConfiguration)
        //{
        //    _sjekklistepunktValidator = sjekklistepunktValidator;
        //}

        public SjekklistekravValidator(IList<EntityValidatorNode> entityValidatorTree, ISjekklistepunktValidator sjekklistepunktValidator)
            : base(entityValidatorTree)
        {
            _sjekklistepunktValidator = sjekklistepunktValidator;
        }


        public ValidationResult Validate(IEnumerable<SjekklistekravValidationEntity> sjekklistekrav)
        {
            if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav) || sjekklistekrav.Count() == 0)
            {
                AddMessageFromRuleIfCollectionIsEmpty(ValidationRuleEnum.krav_utfylt);
            }
            else
            {
                foreach (var krav in sjekklistekrav)
                {
                    ValidateEntityFields(krav);

                    var sjekklistepunktValidationResult = _sjekklistepunktValidator.Validate(krav.ModelData.Sjekklistepunkt);
                    _validationResult.ValidationMessages.AddRange(sjekklistepunktValidationResult.ValidationMessages);
                }
            }

            return _validationResult;
        }

        protected override void InitializeValidationRules()
        {
            this.AddValidationRule(ValidationRuleEnum.krav_utfylt);
            this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, "sjekklistepunktsvar");
            this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "dokumentasjon");
        }

        public void ValidateEntityFields(SjekklistekravValidationEntity sjekklistekravValidationEntity)
        {
            var xpath = sjekklistekravValidationEntity.DataModelXpath;
            var sjekklistekrav = sjekklistekravValidationEntity.ModelData;

            if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav.Sjekklistepunktsvar))
            {
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, xpath, new [] { sjekklistekrav.Sjekklistepunkt.ModelData.Kodeverdi });
            }
            if (!sjekklistekrav.Sjekklistepunktsvar == true)
            {
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, xpath, new[] { sjekklistekrav.Sjekklistepunkt.ModelData.Kodeverdi });
            }
            if (string.IsNullOrEmpty(sjekklistekrav.Dokumentasjon))
            {
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, xpath);
            }

        }

    }
}
