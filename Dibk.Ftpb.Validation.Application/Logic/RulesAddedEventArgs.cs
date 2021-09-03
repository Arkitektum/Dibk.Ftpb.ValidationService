using Dibk.Ftpb.Validation.Application.Reporter;
using System;

namespace Dibk.Ftpb.Validation.Application.Logic
{
    class RulesAddedEventArgs : EventArgs
    {
        public ValidationResult validationResult { get; set; }
    }
}
