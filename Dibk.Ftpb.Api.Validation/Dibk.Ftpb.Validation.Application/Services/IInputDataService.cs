using Dibk.Ftpb.Validation.Application.Models;
using System.IO;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IInputDataService
    {
        InputData GetInputData(string xmlString);
        InputData GetInputData(Stream xmlStream);
    }
}
