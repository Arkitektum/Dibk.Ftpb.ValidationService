using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEntityValidator
    {
        void InitializeValidationRules(string context);
        //ValidationResponse Validate<T>(string context, List<T> validationEntities);
        //void ValidateEntityFields<T>(List<T> validationEntities);
    }
}
