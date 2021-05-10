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
        public ValidationResult ValidationResult;
        public EntityValidatorBase()
        {
            ValidationResult = new();
            ValidationResult.ValidationRules = new List<ValidationRule>();
            ValidationResult.ValidationMessages = new List<ValidationMessage>();
        }

        public abstract void InitializeValidationRules(string context);

        public void AddValidationRule(string id, string xPath)
        {
            ValidationResult.ValidationRules.Add(new ValidationRule()
            {
                Id = id,
                Xpath = xPath
            });
        }

        public void AddMessageFromRule(string id, string xPath, List<string> messageParameters)
        {
            ValidationResult.ValidationMessages.Add(new ValidationMessage()
            {
                Reference = id,
                Xpath = xPath,
                MessageParameters = messageParameters
            });
        }

        public void AddMessageFromRule(string id, List<string> messageParameters)
        {
            AddMessageFromRule(id, null, messageParameters);
        }
        public void AddMessageFromRule(string id, string xPath)
        {
            AddMessageFromRule(id, xPath, null);
        }
        public void AddMessageFromRule(string id)
        {
            AddMessageFromRule(id, null, null);
        }
    }
}
