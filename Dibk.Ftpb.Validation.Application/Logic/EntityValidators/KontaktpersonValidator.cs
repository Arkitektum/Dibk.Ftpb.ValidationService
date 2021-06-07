using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class KontaktpersonValidator : EntityValidatorBase, IKontaktpersonValidator
    {
        public override string ruleXmlElement { get { return "/kontaktperson"; } }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KontaktpersonValidator(EntityValidatorOrchestrator entityValidatorOrchestrator, string parent)
            : base(entityValidatorOrchestrator)
        {
            InitializeValidationRules(EntityXPath);
        }
        protected override void InitializeValidationRules(string xPathForEntity)
        {
            AddValidationRule(ValidationRuleEnum.kontaktperson_navn_utfylt, xPathForEntity, "navn");
        }

        public ValidationResult Validate(KontaktpersonValidationEntity kontaktperson = null)
        {

            if (string.IsNullOrEmpty(kontaktperson.ModelData?.Navn))
                //TODO fill upp {0} parent Node/class/context... in message
                AddMessageFromRule(ValidationRuleEnum.kontaktperson_navn_utfylt);

            return _validationResult;
        }
    }
}
