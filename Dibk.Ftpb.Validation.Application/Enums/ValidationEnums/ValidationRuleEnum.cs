using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums.ValidationEnums
{
    public enum ValidationRuleEnum
    {
        //Generelle test av nye aug-21
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "0")] XsdValidationErrors,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "1")] utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "2")] gyldig,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "5")] kode_KanIkkeValidere,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "7")] utgått,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "8")] sjekklistepunkt_mangler,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "9")] sjekklistepunktsvar_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "10")] sjekklistepunkt_dokumentasjon_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "11")] sjekklistepunkt_1_17_dokumentasjon_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "12")] postnr_4siffer,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "14")] postnr_stemmerIkke,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "15")] postnr_ikke_validert,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "16")] tillatte_postnr_i_kommune,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "17")] kontrollsiffer,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "18")] dekryptering,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "19")] telmob_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "20")] beskrivelse,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "21")] framtidige_eller_eksisterende_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "22")] faste_eller_midlertidige_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "23")] ehf_eller_papir,


    }



}
