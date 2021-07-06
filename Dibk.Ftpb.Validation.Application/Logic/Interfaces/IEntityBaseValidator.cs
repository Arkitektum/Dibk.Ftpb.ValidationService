using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Logic.Interfaces
{
    public interface IEntityBaseValidator
    {
        ValidationResult ValidationResult { get; set; }
    }
}
