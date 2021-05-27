using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum ValidationRuleEnum
    {
        //eiendomadresse
        eiendomsadresse_utfylt,
        eiendomsadresse_adresselinje1_utfylt,
        eiendomsadresse_adresselinje2_utfylt,
        eiendomsadresse_adresselinje3_utfylt,
        eiendomsadresse_landkode_utfylt,
        eiendomsadresse_postnr_utfylt,
        eiendomsadresse_poststed_utfylt,
        eiendomsadresse_gatenavn_utfylt,
        eiendomsadresse_husnr_utfylt,
        eiendomsadresse_bokstav_utfylt,
        eiendomsadresse_postnr_4siffer,
        eiendomsadresse_postnr_kontrollSiffer,
        eiendomsadresse_postnr_ugyldig,
        eiendomsadresse_postnr_stemmerIkke,
        eiendomsadresse_postnr_ikkeValidert,
        eiendomsadresse_bygningsnummer_utfylt,
        eiendomsadresse_bolignummer_utfylt,
        eiendomsadresse_kommunenavn_utfylt,
        eiendomsadresse_tillatte_postnr_i_kommune,
        
        //enkel adresse
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
        arbeidsplasser_framtidige_eller_eksisterende_utfylt,
        arbeidsplasser_faste_eller_midlertidige_utfylt,
        arbeidsplasser_type_arbeid_utfylt,
        arbeidsplasser_utleieBygg,
        arbeidsplasser_beskrivelse,

        //Tiltakshaver
        tiltakshaver_utfylt,
        tiltakshaver_adresse_utfylt,
        tiltakshaver_adresselinje1_utfylt,
        tiltakshaver_adresselinje2_utfylt,
        tiltakshaver_adresselinje3_utfylt,
        tiltakshaver_landkode_utfylt,
        tiltakshaver_postnr_utfylt,
        tiltakshaver_poststed_utfylt,
        tiltakshaver_postnr_4siffer,
        tiltakshaver_postnr_kontrollSiffer,
        tiltakshaver_postnr_ugyldig,
        tiltakshaver_postnr_stemmerIkke,
        tiltakshaver_postnr_ikkeValidert,
        tiltakshaver_kommunenavn_utfylt,

        tiltakshaver_navn_utfylt,
        tiltakshaver_telmob_utfylt,
        tiltakshaver_epost_utfylt,
        tiltakshaver_foedselnummer_utfylt,
        tiltakshaver_foedselnummer_dekryptering,
        tiltakshaver_foedselnummer_ugyldig,
        tiltakshaver_foedselnummer_kontrollsiffer,
        tiltakshaver_organisasjonsnummer_utfylt,
        tiltakshaver_organisasjonsnummer_kontrollsiffer,
        tiltakshaver_organisasjonsnummer_ugyldig,



        //Aktør
        //TelMob_Utfylt,
        //epost_Utfylt,
        //Navn_Utfylt,
        //foedselnummer_utfylt,
        //foedselnummer_Dekryptering,
        //foedselnummer_ugyldig,
        //foedselnummer_kontrollsiffer,
        //organisasjonsnummer_utfylt,
        //organisasjonsnummer_kontrollsiffer,
        //organisasjonsnummer_ugyldig,


        //Partstype
        partstype_utfylt,
        kodeverdi_ugyldig,

        //Kontaktperson
        kontaktperson_navn_utfylt,

        //fakturamottaker
        fakturamottaker_utfylt,
        
        //
        XsdValidationErrors

    }



}
