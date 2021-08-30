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
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "3")] kodeverdi_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "4")] kodeverdi_gyldig,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "5")] kode_KanIkkeValidere,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "6")] kodeliste_gyldig,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "7")] kommunenummer_utgått,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "8")] sjekklistepunkt_mangler,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "9")] sjekklistepunktsvar_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "10")] sjekklistepunkt_dokumentasjon_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "11")] sjekklistepunkt_1_17_dokumentasjon_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "12")] postnr_4siffer,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "13")] postnr_kontrollsiffer,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "14")] postnr_stemmerIkke,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "15")] postnr_ikke_validert,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "16")] tillatte_postnr_i_kommune,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "17")] kontrollsiffer,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "18")] dekryptering,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "19")] telmob_utfylt,

        //arbeidsplasser
        //arbeidsplasser_utfylt,
        //arbeidsplasser_framtidige_eller_eksisterende_utfylt,
        //arbeidsplasser_faste_eller_midlertidige_utfylt,
        //arbeidsplasser_type_arbeid_utfylt,
        //arbeidsplasser_utleieBygg,
        //arbeidsplasser_beskrivelse,




        //Sjekklistekrav
        //krav_utfylt,
        //krav_sjekklistekrav_sjekklistepunktsvar_utfylt,
        //krav_sjekklistekrav_sjekklistepunktsvar_oppfylt,
        //krav_sjekklistekrav_sjekklistepunkt_kode_utfylt,
        //krav_sjekklistekrav_sjekklistepunkt_kode_gyldig,
        //krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt,
        //krav_sjekklistekrav_dokumentasjon_utfylt,

        //Partstype
        //kodeliste_utfylt,
        //kodeverdi_ugyldig,

        //BeskrivelseAvTiltak
        //beskrivelseAvTiltak_utfylt,
        //beskrivelseAvTiltak_formaaltype_utfylt,

        //beskrivelseAvTiltak_anleggstype_kode_utfylt,
        //beskrivelseAvTiltak_anleggstype_gyldig_kode,
        //beskrivelseAvTiltak_anleggstype_beskrivelse_utfylt,
        
        //beskrivelseAvTiltak_naeringsgruppe_kode_utfylt,
        //beskrivelseAvTiltak_naeringsgruppe_gyldig_kode,
        //beskrivelseAvTiltak_naeringsgruppe_beskrivelse_utfylt,
        
        //beskrivelseAvTiltak_bygningstype_kode_utfylt,
        //beskrivelseAvTiltak_bygningstype_gyldig_kode,
        //beskrivelseAvTiltak_bygningstype_beskrivelse_utfylt,
        
        //beskrivelseAvTiltak_tiltakformaal_kode_utfylt,
        //beskrivelseAvTiltak_tiltakformaal_gyldig_kode,
        //beskrivelseAvTiltak_tiltakformaal_beskrivelse_utfylt,
        
        //beskrivelseAvTiltak_tiltakstype_finnes_ikke,
        //beskrivelseAvTiltak_tiltakstype_kode_utfylt,
        //beskrivelseAvTiltak_tiltakstype_gyldig_kode,
        //beskrivelseAvTiltak_tiltakstype_beskrivelse_utfylt,
        
        //beskrivelseAvTiltak_beskrivPlanlagtFormaal_utfylt,
        
        //

    }



}
