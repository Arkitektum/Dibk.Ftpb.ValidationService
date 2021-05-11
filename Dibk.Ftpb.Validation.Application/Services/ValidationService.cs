using Dibk.Ftpb.Validation.Application.Process;
using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IInputDataService _inputDataService;
        private readonly IXsdValidationService _xsdValidationService;
        private readonly IValidationOrchestrator _validationOrchestrator;

        public ValidationService(
            IInputDataService inputDataService,
            IXsdValidationService xsdValidationService,
            IValidationOrchestrator validationOrchestrator)
        {
            _inputDataService = inputDataService;
            _xsdValidationService = xsdValidationService;
            _validationOrchestrator = validationOrchestrator;
        }

        public ValidationResult Validate(ValidationInput validationInput)
        {
            var inputData = _inputDataService.GetInputData(validationInput.FormData);
            //inputData.Config.DataFormatVersion
            //inputData.Data
            //inputData.IsValid
            var errorMessages = _xsdValidationService.Validate(inputData);
            
            if (!Helpers.ObjectIsNullOrEmpty(inputData?.Config?.DataFormatVersion))
            {
                var validationResult = _validationOrchestrator.ExecuteAsync(inputData?.Config?.DataFormatVersion, errorMessages, validationInput);
                return validationResult.Result;
            }
            else
            {
                var validationResult = new ValidationResult();
                validationResult.ValidationMessages = new List<ValidationMessage>
                {
                    new() {Message = "Can't Get DataFormatId"}
                };
                return null;
            }
        }

        public List<string> Validate(IFormFile xmlFile)
        {
            using var inputData = _inputDataService.GetInputData(xmlFile);
            var messages = _xsdValidationService.Validate(inputData);

            return messages;
        }
    }
}
