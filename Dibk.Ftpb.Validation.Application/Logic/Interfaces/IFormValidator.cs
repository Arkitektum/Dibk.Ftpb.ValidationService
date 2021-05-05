using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IFormValidator
    {
        List<ValidationRule> StartValidation(string xmlData);
    }

    public interface IDataModelMapper<T, U>
    {
        T MapDataModelToFormEntity(U dataModel);
    }
}
