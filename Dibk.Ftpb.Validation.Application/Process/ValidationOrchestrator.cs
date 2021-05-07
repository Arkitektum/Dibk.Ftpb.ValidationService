using Dibk.Ftpb.Validation.Application.Logic;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Process
{
    /// <summary>
    ///…InstantiateValidationMessageContainer(……)
    ///…VerifyInputData(….)
    ///….ValidateFormEntity(….)
    ///…CompileValidationReport(….)    /// </summary>
    public class ValidationOrchestrator : IValidationOrchestrator
    {
        private readonly IServiceProvider _services;
        public List<ValidationRule> Messages { get; set; }

        public ValidationOrchestrator(IServiceProvider services)
        {
            _services = services;
        }

        public async Task<List<ValidationRule>> ExecuteAsync(string dataFormatVersion, string xmlData, List<string> errorMessages)
        {
            Messages = new List<ValidationRule>();

            if (errorMessages.Count > 0)
            {
                foreach (var message in errorMessages)
                {
                    Messages.Add(new ValidationRule() { Id = "generic", Message = message, ValidationResult = ValidationResultEnum.ValidationFailed });
                }
            }

            //TODO Validate Xml structure 
            List<ValidationRule> validationXmlMessages = new List<ValidationRule>();
            Messages.Concat(validationXmlMessages);

            IFormValidator formValidator = GetValidator(dataFormatVersion); //45957

            var validationMessages = Messages.Concat(formValidator.StartValidation(xmlData));

            return validationMessages.ToList();
        }

        public IFormValidator GetValidator(string dataFormatVersion)
        {
            //Retrieves classes implementing IForm, having FormDataFormatAttribute and filtering by its DataFormatId
            object formLogicInstance = null;
            
            try
            {
                var type = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsDefined(typeof(FormDataAttribute), true))
                    .Where(t => t.GetCustomAttribute<FormDataAttribute>().DataFormatVersion == dataFormatVersion)
                    .SingleOrDefault();

                formLogicInstance = _services.GetService(type);
            }
            catch (Exception ex)
            {
                throw;
            }

            return formLogicInstance as IFormValidator;
        }
    }
}
