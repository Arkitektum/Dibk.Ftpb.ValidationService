using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Logic;

namespace Dibk.Ftpb.Validation.Application.Enums.ValidationEnums
{
    public enum EiendomValidationEnum 
    {
        utfylt,
        bygningsnummer_ugyldig,
        bygningsnummer_utfylt,
        bolignummer_utfylt,
        kommunenavn_utfylt,
        tillatte_postnr_i_kommune,
    }
}
