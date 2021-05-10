using Dibk.Ftpb.Validation.Application.Logic;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Enums;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<ValidationOrchestrator> _logger;

        //public List<ValidationRule> ValidationRules { get; set; }
        public ValidationResult ValidationResponse { get; set; }

        public ValidationOrchestrator(IServiceProvider services, ILogger<ValidationOrchestrator> logger)
        {
            _services = services;
            _logger = logger;
        }

        public async Task<ValidationResult> ExecuteAsync(string dataFormatVersion, string xmlData, List<string> errorMessages)
        {
            ValidationResponse = new ValidationResult();
            ValidationResponse.ValidationRules = new();
            ValidationResponse.ValidationMessages = new();

            if (errorMessages.Count > 0)
            {
                foreach (var message in errorMessages)
                {
                    ValidationResponse.ValidationMessages.Add(new ValidationMessage() { Reference = "XsdValidationErrors", Message = message });
                }
            }

            //TODO Validate Xml structure 
            List<ValidationRule> validationXmlMessages = new List<ValidationRule>();
            ValidationResponse.ValidationRules.Concat(validationXmlMessages);

            IFormValidator formValidator = GetValidator(dataFormatVersion); //45957

            ValidationResponse.ValidationRules.Concat(formValidator.StartValidation(xmlData).ValidationRules);
            ValidationResponse.ValidationMessages.Concat(formValidator.StartValidation(xmlData).ValidationMessages);

            return ValidationResponse;
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
                _logger.LogError(ex, "Error occurred when attempting to find validator for {DataFormatVersion}", dataFormatVersion);
                throw;
            }

            return formLogicInstance as IFormValidator;
        }
    }
}
