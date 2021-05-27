using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEntityValidator
    {
        //void InitializeValidationRules(string xPathForEntity);
        ValidationResult ResetValidationMessages();
    }
}
