using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum ValidationRuleEnum
    {
        adresse_bygningsnummer_utfylt,
        adresse_bolignummer_utfylt,
        adresse_kommunenavn_utfylt,
        tillatte_postnr_i_kommune,
        
        adresse_utfylt,
        adresse_adresselinje1_utfylt,
        adresse_adresselinje2_utfylt,
        adresse_adresselinje3_utfylt,
        adresse_landkode_utfylt,
        adresse_postnr_utfylt,
        adresse_poststed_utfylt,
        adresse_gatenavn_utfylt,
        adresse_husnr_utfylt,
        adresse_bokstav_utfylt,
        adresse_postnr_4siffer,
        adresse_postnr_kontrollSiffer,
        adresse_postnr_ugyldig,
        adresse_postnr_stemmerIkke,
        adresse_postnr_ikkeValidert,

        kommunenummer_utfylt,

        //Matrikkel
        gaardsnummer_utfylt,
        bruksnummer_utfylt,
        festenummer_utfylt,
        seksjonsnummer_utfylt,
        
        //Kun for testing
        Parameter_Test,

        //arbeidsplasser
        arbeidsplasser_utfylt,
        framtidige_eller_eksisterende_utfylt,
        faste_eller_midlertidige_utfylt,
        type_arbeid_utfylt,
        utleieBygg,
        arbeidsplasser_beskrivelse,

        //Tiltakshaver
        tiltakshaver_utfylt,

        //Aktør
        TelMob_Utfylt,
        epost_Utfylt,
        Navn_Utfylt,
        foedselnummer_utfylt,
        foedselnummer_Dekryptering,
        foedselnummer_ugyldig,
        foedselnummer_kontrollsiffer,
        organisasjonsnummer_utfylt,
        organisasjonsnummer_kontrollsiffer,
        organisasjonsnummer_ugyldig,


        //Partstype
        partstype_utfylt,
        Kodeverdien_ugyldig,

        //Kontaktperson
        kontaktpersonNavn_utfylt,

        //fakturamottaker
        fakturamottaker_utfylt,
        //
        XsdValidationErrors

    }



}
