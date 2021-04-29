using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IInputDataService _inputDataService;
        private readonly IXsdValidationService _xsdValidationService;

        public ValidationService(
            IInputDataService inputDataService,
            IXsdValidationService xsdValidationService)
        {
            _inputDataService = inputDataService;
            _xsdValidationService = xsdValidationService;
        }

        public List<string> Validate(string xmlString)
        {
            var inputData = _inputDataService.GetInputData(xmlString);
            var messages = _xsdValidationService.Validate(inputData);

            return messages;
        }

        public List<string> Validate(IFormFile xmlFile)
        {
            using var inputData = _inputDataService.GetInputData(xmlFile);
            var messages = _xsdValidationService.Validate(inputData);

            return messages;
        }
    }
}
