using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class EntityValidatorBase : IEntityValidator
    {
        protected ValidationResult ValidationResponse;
        public EntityValidatorBase()
        {
            ValidationResponse = new();
            ValidationResponse.ValidationRules = new List<ValidationRule>();
            ValidationResponse.ValidationMessages = new List<ValidationMessage>();
        }

        public abstract void InitializeValidationRules(string context);
        //protected abstract void ValidateEntityFields<T>(List<T> validationEntities);
        //public abstract ValidationResponse Validate<T>(string context, List<T> validationEntities);

        //void ValidateEntityFields(object entityData)
        //{
        //    throw new NotImplementedException();
        //}

        public void AddValidationRule(string id, string xPath)
        {
            ValidationResponse.ValidationRules.Add(new ValidationRule()
            {
                Id = id,
                Xpath = xPath
                //ValidationResult = ValidationResultEnum.Unused
            });
        }


        public ValidationRule RuleToValidate(string id, string context)
        {
            var validationRule = ValidationResponse.ValidationRules.FirstOrDefault(r => r.Id.Equals(id) && r.Xpath.Equals(context)) ?? new ValidationRule()
            {
                Id = id,
                Message = $"Can't find rule with id:'{id}'.-"
            };

            //ValidationResponse.validationRule.ValidationResult = ValidationResultEnum.ValidationOk;

            return validationRule;
        }

        //public void UpdateValidationResult2Failed(ValidationRule rule, string[] ruleMessagesParameters = null)
        //{
        //    if (ValidationRules.Any(r => r.Id.Equals(rule.Id)))
        //    {
        //        ValidationRules.First(r => r.Id == rule.Id).ValidationResult = ValidationResultEnum.ValidationFailed;
        //        if (ruleMessagesParameters != null)
        //        {
        //            //TODO check change MessageParameters in rule to array to us string.format to change all parameters 
        //            ValidationRules.First(r => r.Id == rule.Id).MessageParameters = ruleMessagesParameters.ToList();
        //        }
        //    }
        //}

        //public void AddMessageFromRule(string id, string xPath, List<string> messageParameters)
        public void AddMessageFromRule(string id, string xPath, Nullable<int> elementNumber, List<string> messageParameters)
        {
            var rule = RuleToValidate(id, xPath);
            if (elementNumber != null)
            {
                //ArbeidstilsynetsSamtykke/eiendomByggested/Bygningsnummer  ==>  ArbeidstilsynetsSamtykke/eiendomByggested{0}/Bygningsnummer
                //ArbeidstilsynetsSamtykke/eiendomByggested  ==>  ArbeidstilsynetsSamtykke/eiendomByggested[2]
                //Pos:41

                int lastSlash = xPath.LastIndexOf('/');
                string beforeSlash = xPath.Substring(0, lastSlash);
                string afterSlash = xPath.Substring(lastSlash + 1);
                xPath = beforeSlash + "[" + elementNumber.ToString() + "]/" + afterSlash;
            }
            ValidationResponse.ValidationMessages.Add(new ValidationMessage() { Reference = id, 
                                                                                Xpath = xPath, 
                                                                                Message = rule.Message, 
                                                                                MessageParameters = messageParameters });
        }
        public void AddMessageFromRule(string id, string context, Nullable<int> elementNumber)
        {
            AddMessageFromRule(id, context, elementNumber, null);
        }
        public void AddMessageFromRule(string id, string context, List<string> messageParameters)
        {
            AddMessageFromRule(id, context, null, messageParameters);
        }
        public void AddMessageFromRule(string id, string context)
        {
            AddMessageFromRule(id, context, null, null);
        }
    }
}
