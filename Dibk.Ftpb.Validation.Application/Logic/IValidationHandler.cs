using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.Web;

namespace Dibk.Ftpb.Validation.Application.Logic
{
    public interface IValidationHandler
    {
        Task<ValidationResult> ValidateAsync(string dataFormatVersion, List<string> errorMessages, ValidationInput validationInput);
    }
}