using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.Web;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    public interface IValidationHandler
    {
        Task<ValidationResult> ValidateAsync(string dataFormatId, string dataFormatVersion, List<string> errorMessages, ValidationInput validationInput);
        Task<ValidationRule[]> GetformRulesAsync(string dataFormatId, string dataFormatVersion);
    }
}