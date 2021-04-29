using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IValidationService
    {
        List<string> Validate(string xmlString);
        List<string> Validate(IFormFile xmlFile);
    }
}
