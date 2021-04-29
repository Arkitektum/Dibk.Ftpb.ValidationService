using Dibk.Ftpb.Validation.Application.Models;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public interface IXsdValidationService
    {
        List<string> Validate(InputData inputData);
    }
}
