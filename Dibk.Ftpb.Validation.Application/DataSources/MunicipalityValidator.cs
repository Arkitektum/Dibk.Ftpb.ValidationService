using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Municipality;
using Dibk.Ftpb.Validation.Application.Enums;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.DataSources
{
    public interface IMunicipalityValidator
    {
        MunicipalityValidationResult Validate_kommunenummerStatus(string kommunenummer);
    }
    public class MunicipalityValidationResult
    {
        public MunicipalityValidationEnum Status { get; set; }
        public string Message { get; set; }
    }

    public class MunicipalityValidator: IMunicipalityValidator
    {
        private readonly IMunicipalityApiService _municipalityApiService;

        public MunicipalityValidator(IMunicipalityApiService municipalityApiService)
        {
            _municipalityApiService = municipalityApiService;
        }
        public MunicipalityValidationResult Validate_kommunenummerStatus(string kommunenummer)
        {
            var municipality = Task.Run(() => _municipalityApiService.GetMunicipality(kommunenummer)).Result;
            
            return new MunicipalityValidationResult();
        }
    }
}
