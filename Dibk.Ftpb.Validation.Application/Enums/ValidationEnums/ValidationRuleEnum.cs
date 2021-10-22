namespace Dibk.Ftpb.Validation.Application.Enums.ValidationEnums
{
    public enum ValidationRuleEnum
    {
        //Generelle test av nye aug-21
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "0")] XsdValidationErrors,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "1")] utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "2")] gyldig, // @Helge forklar hva som er forskjellen mellom gyldig og tillatt 
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "3")] validert,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "4")] utgått,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "5")] status,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "6")] tillatt, // @Helge forklar hva som er forskjellen mellom gyldig og tillatt


        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "16")] tillatte_postnr_i_kommune,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "17")] kontrollsiffer,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "18")] dekryptering,

        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "20")] beskrivelse,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "21")] framtidige_eller_eksisterende_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "22")] faste_eller_midlertidige_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "23")] ehf_eller_papir,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "24")] numerisk,
        

        //not used
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "8")] sjekklistepunkt_mangler,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "9")] sjekklistepunktsvar_utfylt,
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "10")] sjekklistepunkt_dokumentasjon_utfylt,

        // potential for improvement
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "11")] sjekklistepunkt_1_18_dokumentasjon_utfylt, // utfylt
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "19")] telmob_utfylt, //utfylt
        [ValidationRuleTypeEnumerationAttribute(ValidationRuleTypeId = "14")] postnr_stemmerIkke, //stemmerIkke
        



    }



}
