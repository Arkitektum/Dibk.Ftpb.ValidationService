using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SjekklistekravValidator : EntityValidatorBase, ISjekklistekravValidator
    {
        private readonly ISjekklistepunktValidator _sjekklistepunktValidator;

        public override string ruleXmlElement { get { return "/krav{0}"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public SjekklistekravValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, ISjekklistepunktValidator sjekklistepunktValidator)
            : base(entityValidatorOrchestrator)
        {
            InitializeValidationRules(EntityXPath);
            _sjekklistepunktValidator = sjekklistepunktValidator;
        }
        public ValidationResult Validate(IEnumerable<SjekklistekravValidationEntity> sjekklistekrav)
        {

            if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav) || sjekklistekrav.Count() == 0)
            {
                //TODO
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_utfylt);
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

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            this.AddValidationRule(ValidationRuleEnum.sjekklistekrav_utfylt, xPathForEntity);
            this.AddValidationRule(ValidationRuleEnum.sjekklistekrav_krav_utfylt, xPathForEntity, "erKravOppfylt");
            this.AddValidationRule(ValidationRuleEnum.sjekklistekrav_krav_oppfylt, xPathForEntity, "erKravOppfylt");
        }


        public void ValidateEntityFields(SjekklistekravValidationEntity sjekklistekravValidationEntity)
        {
            var sjekklistekrav = sjekklistekravValidationEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav))
            {
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_utfylt);
            }
            if (Helpers.ObjectIsNullOrEmpty(sjekklistekrav.ErKravOppfylt))
            {
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_krav_utfylt);
            }
            if (!sjekklistekrav.ErKravOppfylt == true)
            {
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_krav_oppfylt);
            }


        }

    }
}
