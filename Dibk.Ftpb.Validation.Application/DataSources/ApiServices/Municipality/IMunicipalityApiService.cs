using Dibk.Ftpb.Validation.Application.DataSources.Models;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Municipality
{
    public interface IMunicipalityApiService
    {
        Task<MunicipalityViewModel> GetMunicipality(string municipalityCode);
    }
}