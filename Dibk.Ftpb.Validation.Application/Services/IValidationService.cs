using Dibk.Ftpb.Validation.Application.Reporter;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IValidationService
    {
        ValidationResult Validate(string xmlString);
        List<string> Validate(IFormFile xmlFile);
    }
}
