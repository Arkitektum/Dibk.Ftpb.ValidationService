using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public interface IValidationMessageComposer
    {
        ValidationResult ComposeValidationResult(string xPathRoot, string dataFormatVersion, ValidationResult validationResult, string languageCode);
    }
}
