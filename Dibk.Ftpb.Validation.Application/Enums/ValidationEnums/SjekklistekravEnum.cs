using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums.ValidationEnums
{
    public enum SjekklistekravEnum
    {
        //sjekklistepunkt_utfylt,
        //sjekklistepunktSvar_utfylt,
        utfylt,
        kodeverdi_mangler,
        kodeverdi_gyldig,

        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.13")] pkt_1_13_kodeverdi_utfylt,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.13")] pkt_1_13_sjekklistepunktsvar_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.13")] pkt_1_13_kodebeskrivelse_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.13")] pkt_1_13_dokumentasjon_utfylt,

        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.16")] pkt_1_16_kodeverdi_utfylt,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.16")] pkt_1_16_sjekklistepunktsvar_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.16")] pkt_1_16_kodebeskrivelse_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "1.16")] pkt_1_16_dokumentasjon_utfylt,

        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.1")] pkt_10_1_kodeverdi_utfylt,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.1")] pkt_10_1_sjekklistepunktsvar_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.1")] pkt_10_1_kodebeskrivelse_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.1")] pkt_10_1_dokumentasjon_utfylt,

        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.2")] pkt_10_2_kodeverdi_utfylt,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.2")] pkt_10_2_sjekklistepunktsvar_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.2")] pkt_10_2_kodebeskrivelse_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.2")] pkt_10_2_dokumentasjon_utfylt,

        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.3")] pkt_10_3_kodeverdi_utfylt,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.3")] pkt_10_3_sjekklistepunktsvar_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.3")] pkt_10_3_kodebeskrivelse_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.3")] pkt_10_3_dokumentasjon_utfylt,

        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.4")] pkt_10_4_kodeverdi_utfylt,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.4")] pkt_10_4_sjekklistepunktsvar_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.4")] pkt_10_4_kodebeskrivelse_gyldig,
        [SjekklistekravEnumerationAttribute(SjekklistepunktVerdi = "10.4")] pkt_10_4_dokumentasjon_utfylt,




    }
}
