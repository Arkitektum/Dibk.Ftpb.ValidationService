using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum ValidationResultEnum
    {
        Unused,
        ValidationOk,
        ValidationFailed
        //Error,
        //Warning,
    }
}
