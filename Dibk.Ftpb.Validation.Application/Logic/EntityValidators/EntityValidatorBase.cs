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
        protected List<ValidationRule> ValidationRules;
        public EntityValidatorBase()
        {
            ValidationRules = new List<ValidationRule>();
        }

        public abstract void InitializeValidationRules(string context);
        public abstract void ValidateEntityFields(object data);

        //void ValidateEntityFields(object entityData)
        //{
        //    throw new NotImplementedException();
        //}

        public void AddValidationRule(string id, string xPath)
        {
            ValidationRules.Add(new ValidationRule()
            {
                Id = id,
                Xpath = xPath,
                ValidationResult = ValidationResultEnum.Unused
            });
        }

        public ValidationRule RuleToValidate(string id)
        {
            var validationRule = ValidationRules.FirstOrDefault(r => r.Id.Equals(id)) ?? new ValidationRule()
            {
                Id = id,
                Message = $"Can't find rule with id:'{id}'.-"
            };

            validationRule.ValidationResult = ValidationResultEnum.ValidationOk;

            return validationRule;
        }

        public void UpdateValidationResult2Failed(ValidationRule rule, string[] ruleMessagesParameters = null)
        {
            if (ValidationRules.Any(r => r.Id.Equals(rule.Id)))
            {
                ValidationRules.First(r => r.Id == rule.Id).ValidationResult = ValidationResultEnum.ValidationFailed;
                if (ruleMessagesParameters != null)
                {
                    //TODO check change MessageParameters in rule to array to us string.format to change all parameters 
                    ValidationRules.First(r => r.Id == rule.Id).MessageParameters = ruleMessagesParameters.ToList();
                }
            }
        }
    }
}
