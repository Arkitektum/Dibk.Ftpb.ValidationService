using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using System.Linq;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class SjekklistepunktValidator : EntityValidatorBase, ISjekklistepunktValidator
    {
        public override string ruleXmlElement { get { return "/sjekklistepunkt"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public SjekklistepunktValidator(EntityValidatorOrchestrator entityValidatorOrchestrator)
            : base(entityValidatorOrchestrator)
        {
            InitializeValidationRules(EntityXPath);
        }
        public ValidationResult Validate(SjekklistepunktValidationEntity sjekklistepunkt)
        {

            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt))
            {
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_krav_kode_utfylt);
            }
            else
            {
                ValidateEntityFields(sjekklistepunkt);
            }

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathForEntity)
        {
            this.AddValidationRule(ValidationRuleEnum.sjekklistekrav_krav_kode_utfylt, xPathForEntity, "kodeverdi");
            this.AddValidationRule(ValidationRuleEnum.sjekklistekrav_krav_kode_gyldig, xPathForEntity, "kodeverdi");
            this.AddValidationRule(ValidationRuleEnum.sjekklistekrav_krav_beskrivelse_utfylt, xPathForEntity, "kodebeskrivelse");
        }

        public void ValidateEntityFields(SjekklistepunktValidationEntity sjekklistepunktValidationEntity)
        {
            var xPath = sjekklistepunktValidationEntity.DataModelXpath;

            var sjekklistepunkt = sjekklistepunktValidationEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt))
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_krav_kode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt?.Kodeverdi))
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_krav_kode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt?.Kodebeskrivelse))
                AddMessageFromRule(ValidationRuleEnum.sjekklistekrav_krav_beskrivelse_utfylt, xPath);
        }
    }
}
