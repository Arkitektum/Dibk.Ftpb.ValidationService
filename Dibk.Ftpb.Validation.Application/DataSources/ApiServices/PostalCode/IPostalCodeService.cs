using Dibk.Ftpb.Validation.Application.DataSources.Models;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode
{
    public interface IPostalCodeService
    {
        PostalCodeValidationResult ValidatePostnr(string pnr, string country);
    }
}