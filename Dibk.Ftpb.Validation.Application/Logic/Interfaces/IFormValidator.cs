using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Models.Web;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IFormValidator
    {
        ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput);
    }

    public interface IDataModelMapper<T, U>
    {
        T MapDataModelToFormEntity(U dataModel);
    }
}
