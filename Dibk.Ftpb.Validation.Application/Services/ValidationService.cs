using Dibk.Ftpb.Validation.Application.Process;
using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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

        public List<ValidationRule> Validate(string xmlString)
        {
            var inputData = _inputDataService.GetInputData(xmlString);
            //inputData.Config.DataFormatVersion
            //inputData.Data
            //inputData.IsValid

            var errorMessages = _xsdValidationService.Validate(inputData);
            var validationRules = _validationOrchestrator.ExecuteAsync(inputData.Config.DataFormatVersion, xmlString, errorMessages);
            return validationRules.Result;
        }

        public List<string> Validate(IFormFile xmlFile)
        {
            using var inputData = _inputDataService.GetInputData(xmlFile);
            var messages = _xsdValidationService.Validate(inputData);

            return messages;
        }
    }
}
