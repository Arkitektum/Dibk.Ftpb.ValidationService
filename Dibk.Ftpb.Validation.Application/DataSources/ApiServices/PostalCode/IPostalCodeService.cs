using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.Models;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode
{
    public interface IPostalCodeService
    {
        Task<PostalCodeValidationResult> ValidatePostnr(string pnr, string country);
    }
}