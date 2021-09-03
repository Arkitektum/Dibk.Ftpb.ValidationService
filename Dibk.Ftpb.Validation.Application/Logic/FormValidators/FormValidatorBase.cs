using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public abstract class FormValidatorBase
    {
        protected IList<EntityValidatorNode> EntityValidatorTree;
        private readonly IValidationMessageComposer _validationMessageComposer;
        protected IEnumerable<Sjekklistekrav> Sjekklistekrav;


        //protected IEiendomByggestedValidator _eiendomByggestedValidator;
        //protected IArbeidsplasserValidator _arbeidsplasserValidator;

        //private void _eiendomByggestedValidator_RulesAdded(object sender, ValidationResult e)
        //{
        //    AddValidationRules(e.ValidationRules);
        //}

        //private void _arbeidsplasserValidator_RulesAdded(object sender, ValidationResult e)
        //{
        //    AddValidationRules(e.ValidationRules);
        //}

        //private void AddValidationRules(List<ValidationRule> validationRules)
        //{
        //    ValidationReport.ValidationResult.ValidationRules.AddRange(validationRules);
        //}


        protected ValidationReport ValidationReport;
        protected abstract string XPathRoot { get; }
        protected abstract void InitializeValidatorConfig();
        public FormValidatorBase(IValidationMessageComposer validationMessageComposer)
        {
            _validationMessageComposer = validationMessageComposer;
            ValidationReport = new ValidationReport();

            //_arbeidsplasserValidator.RulesAdded += _arbeidsplasserValidator_RulesAdded;
            //_eiendomByggestedValidator.RulesAdded += _eiendomByggestedValidator_RulesAdded;
        }

        public virtual ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            InitializeValidatorConfig();
            InstantiateValidators();
            DefineValidationRules();
            Validate(validationInput);

            ValidationResult = _validationMessageComposer.ComposeValidationResult(XPathRoot, dataFormatVersion, ValidationResult, "NO");
            //ValidationReport.ValidationResult = ValidationResult;

            return ValidationResult;
        }

        protected abstract void InstantiateValidators();
        protected abstract void Validate(ValidationInput validationInput);
        protected abstract void DefineValidationRules();

        protected ValidationResult ValidationResult;
        //private readonly IValidationMessageComposer validationMessageComposer;

        protected void AccumulateValidationRules(List<ValidationRule> validationRules)
        {
            ValidationResult ??= new ValidationResult();
            ValidationResult.ValidationRules ??= new List<ValidationRule>();

            var whereNotAlreadyExists = validationRules.Where(x => !ValidationResult.ValidationRules.Any(y => y.Xpath == x.Xpath && y.Rule == x.Rule));
            ValidationResult.ValidationRules.AddRange(whereNotAlreadyExists);
        }

        protected void AccumulateValidationMessages(List<ValidationMessage> validationMessages)
        {
            ValidationResult ??= new ValidationResult();
            ValidationResult.ValidationMessages ??= new List<ValidationMessage>();
            ValidationResult.ValidationMessages.AddRange(validationMessages);
        }
    }
}
