using System;
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

            var result = new MunicipalityValidationResult();
            if (municipality == null)
            {
                result.Status = MunicipalityValidationEnum.Invalid;
                result.Message = string.Empty;
            }
            else if (municipality.ValidTo.HasValue && DateTime.Now > municipality.ValidTo.Value)
            {
                result.Status = MunicipalityValidationEnum.Expired;
                result.Message = municipality.NewMunicipalityCode;
            }
            else if (municipality.ValidFrom.HasValue && DateTime.Now < municipality.ValidFrom.Value)
            {
                result.Status = MunicipalityValidationEnum.TooSoon;
                result.Message = municipality.ValidFrom.Value.ToShortDateString();
            }
            else
            {
                result.Status = MunicipalityValidationEnum.Ok;
                result.Message = "ok";
            }
            return result ;
        }
    }
}
