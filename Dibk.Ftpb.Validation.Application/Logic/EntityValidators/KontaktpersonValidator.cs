using System;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class KontaktpersonValidator : EntityValidatorBase, IKontaktpersonValidator
    {
        public override string ruleXmlElement { get { return "kontaktperson"; } set { ruleXmlElement = value; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KontaktpersonValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, EntityValidatorEnum parentValidator)
            : base(entityValidatorOrchestrator, parentValidator)
        {
        }
        protected override void InitializeValidationRules(string xPathToEntity)
        {
            AddValidationRule(ValidationRuleEnum.kontaktperson_navn_utfylt, xPathToEntity, "navn");
        }

        public ValidationResult Validate(KontaktpersonValidationEntity kontaktperson = null)
        {

            if (string.IsNullOrEmpty(kontaktperson.ModelData?.Navn))
                //TODO fill upp {0} parent Node/class/context... in message
                AddMessageFromRule(ValidationRuleEnum.kontaktperson_navn_utfylt, kontaktperson.DataModelXpath);

            return _validationResult;
        }
    }
}
