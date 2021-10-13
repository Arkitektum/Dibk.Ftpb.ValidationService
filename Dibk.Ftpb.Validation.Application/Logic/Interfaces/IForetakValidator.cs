using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IForetakValidator
    {
        ValidationResult ValidationResult { get; set; }
        ValidationResult Validate(Foretak foretak = null);
        void ValidateEntityFields(Foretak foretak);
    }
}
