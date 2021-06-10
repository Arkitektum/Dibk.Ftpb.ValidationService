﻿using Dibk.Ftpb.Validation.Application.Enums;
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

        public ValidationResult ValidationResult { get => _validationResult; }

        public SjekklistepunktValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator)
            : base(entityValidatorOrchestrator, parentValidator)
        {
        }
        public ValidationResult Validate(SjekklistepunktValidationEntity sjekklistepunkt)
        {
            var xpath = sjekklistepunkt.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt))
            {
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, xpath);
            }
            else
            {
                ValidateEntityFields(sjekklistepunkt);
            }

            return _validationResult;
        }

        protected override void InitializeValidationRules(string xPathToEntity)
        {
            this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, xPathToEntity, "kodeverdi");
            this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_gyldig, xPathToEntity, "kodeverdi");
            this.AddValidationRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt, xPathToEntity, "kodebeskrivelse");
        }

        public void ValidateEntityFields(SjekklistepunktValidationEntity sjekklistepunktValidationEntity)
        {
            var xPath = sjekklistepunktValidationEntity.DataModelXpath;

            var sjekklistepunkt = sjekklistepunktValidationEntity.ModelData;
            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt))
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt?.Kodeverdi))
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, xPath);

            if (Helpers.ObjectIsNullOrEmpty(sjekklistepunkt?.Kodebeskrivelse))
                AddMessageFromRule(ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt, xPath);
        }
    }
}