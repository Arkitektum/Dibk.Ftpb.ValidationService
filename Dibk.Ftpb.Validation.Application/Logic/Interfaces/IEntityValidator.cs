using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEntityValidator
    {
        ValidationResult ResetValidationMessages();
        ValidationResult ValidationResult { get; set; }
        
    }
}
