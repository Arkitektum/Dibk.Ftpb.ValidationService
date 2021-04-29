using Dibk.Ftpb.Validation.Application.Models;
using Microsoft.AspNetCore.Http;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IInputDataService
    {
        InputData GetInputData(string xmlString);
        InputData GetInputData(IFormFile xmlFile);
    }
}
