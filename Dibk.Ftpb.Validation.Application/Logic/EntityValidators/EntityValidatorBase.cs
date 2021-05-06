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
                id = id,
                xpath = xPath,
                validationResult = ValidationResultEnum.Unused
            });
        }

        public ValidationRule RuleToValidate(string id)
        {
            var validationRule = ValidationRules.FirstOrDefault(r => r.id.Equals(id)) ?? new ValidationRule()
            {
                id = id,
                message = $"Can't finde rule with id:'{id}'.-"
            };

            validationRule.validationResult = ValidationResultEnum.ValidationOk;

            return validationRule;
        }
    }
}
