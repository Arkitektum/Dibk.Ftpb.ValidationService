using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Models.Web;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IValidationService
    {
        //ValidationResult Validate(string xmlString, List<string> attachments = null, List<string> subForms = null);
        ValidationResult GetValidationResult(ValidationInput validationInput);
        ValidationResult GetValidationResultWithChecklistAnswers(ValidationInput validationInput);
        List<string> ValidateXmlFile(IFormFile xmlFile);
        //IEnumerable<PrefillDemo> PrefillDemo();
        string PrefillDemo();
    }
}
