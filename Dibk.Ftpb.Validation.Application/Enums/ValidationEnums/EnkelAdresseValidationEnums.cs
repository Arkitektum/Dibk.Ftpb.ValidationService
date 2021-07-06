using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums.ValidationEnums
{
    public enum EnkelAdresseValidationEnums
    {
        adresse_utfylt,
        adresselinje1_utfylt,
        adresselinje2_utfylt,
        adresselinje3_utfylt,
        landkode_utfylt,
        landkode_ugyldug,
        postnr_utfylt,
        poststed_utfylt,
        postnr_4siffer,
        postnr_kontrollSiffer,
        postnr_ugyldig,
        postnr_stemmerIkke,
        postnr_ikkeValidert,
        tillatte_postnr_i_kommune,
    }
}
