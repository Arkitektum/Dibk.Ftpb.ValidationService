using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public abstract class FormValidatorBase
    {
        protected ValidationResult ValidationResult;
        protected IList<EntityValidatorNode> EntityValidatorTree;
        private readonly IValidationMessageComposer _validationMessageComposer;
        protected IEnumerable<Sjekklistekrav> Sjekklistekrav;
        private readonly IChecklistService _checklistService;


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


        //protected ValidationReport ValidationReport;
        protected abstract string XPathRoot { get; }
        protected abstract void InitializeValidatorConfig();
        protected abstract IEnumerable<string> GetFormTiltakstyper();
        public FormValidatorBase(IValidationMessageComposer validationMessageComposer, IChecklistService checklistService = null)
        {
            _validationMessageComposer = validationMessageComposer;
            _checklistService = checklistService;
            //ValidationReport = new ValidationReport();

            //_arbeidsplasserValidator.RulesAdded += _arbeidsplasserValidator_RulesAdded;
            //_eiendomByggestedValidator.RulesAdded += _eiendomByggestedValidator_RulesAdded;
        }

        protected string GetDataFormatVersion(Type t)
        {
            FormDataAttribute myAtt = (FormDataAttribute)Attribute.GetCustomAttribute(t, typeof(FormDataAttribute));
            return myAtt.DataFormatVersion;
        }
        public virtual ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            InitializeValidatorConfig();
            InstantiateValidators();
            DefineValidationRules();
            Validate(validationInput);
            ValidationResult = _validationMessageComposer.ComposeValidationResult(XPathRoot, dataFormatVersion, ValidationResult, "NO");

            if (!ValidationResult.ValidationMessages.Any(x => x.Messagetype.Equals(ValidationResultSeverityEnum.CRITICAL)))
            {
                FilterValidationMessagesOnTiltakstyper(dataFormatVersion);
            }

            //ValidationReport.ValidationResult = ValidationResult;

            return ValidationResult;
        }

        protected abstract void InstantiateValidators();
        protected abstract void Validate(ValidationInput validationInput);
        protected abstract void DefineValidationRules();

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

        private void FilterValidationMessagesOnTiltakstyper(string dataFormatVersion)
        {
            var tiltakstyper = GetFormTiltakstyper();
            var result = _checklistService.FilterValidationResult(dataFormatVersion, ValidationResult.ValidationMessages, tiltakstyper);
            ValidationResult.ValidationMessages = result.ToList();


        }

    }

    public class FormDataAttribute : Attribute
    {
        public string DataFormatVersion { get; set; }
    }
}
