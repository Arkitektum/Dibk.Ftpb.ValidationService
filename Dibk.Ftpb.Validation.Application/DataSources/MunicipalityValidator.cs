using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.DataSources
{
    public interface IMunicipalityValidator
    {
        MunisipalituValidationResult Validate_kommunenummerStatus(string kommunenummer);
    }
    public class MunisipalituValidationResult
    {
        public MunicipalityValidationEnum Status { get; set; }
        public string Message { get; set; }
    }
    public class MunicipalityValidator: IMunicipalityValidator
    {
        public MunisipalituValidationResult Validate_kommunenummerStatus(string kommunenummer)
        {
            var municipality = new MunicipalityApiService().GetMunicipality(kommunenummer);
            
            return new MunisipalituValidationResult();
        }
    }
}
