using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Enums
{
    public enum ValidationRuleEnum
    {
        //eiendom
        eiendom_utfylt,

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
        adresse_postnr_kontrollsiffer,
        adresse_postnr_ugyldig,
        adresse_postnr_stemmerIkke,
        adresse_postnr_ikke_validert,
        adresse_postnr_til_galningar,

        eiendomsidentifikasjon_kommunenummer_utfylt,

        //Matrikkel
        eiendomsidentifikasjon_utfylt,
        eiendomsidentifikasjon_gaardsnummer_utfylt,
        eiendomsidentifikasjon_bruksnummer_utfylt,
        eiendomsidentifikasjon_festenummer_utfylt,
        eiendomsidentifikasjon_seksjonsnummer_utfylt,
        
        //Kun for testing
        Parameter_Test,

        //arbeidsplasser
        arbeidsplasser_utfylt,
        arbeidsplasser_framtidige_eller_eksisterende_utfylt,
        arbeidsplasser_faste_eller_midlertidige_utfylt,
        arbeidsplasser_type_arbeid_utfylt,
        arbeidsplasser_utleieBygg,
        arbeidsplasser_beskrivelse,

        //Tiltakshaver og ansvarligSoeker
        aktoer_utfylt,
        aktoer_partstype_utfylt,
        aktoer_adresse_utfylt,
        aktoer_kontaktperson_utfylt,
        aktoer_navn_utfylt,
        aktoer_telmob_utfylt,
        aktoer_epost_utfylt,
        aktoer_foedselnummer_utfylt,
        aktoer_foedselnummer_dekryptering,
        aktoer_foedselnummer_ugyldig,
        aktoer_foedselnummer_kontrollsiffer,
        aktoer_organisasjonsnummer_utfylt,
        aktoer_organisasjonsnummer_kontrollsiffer,
        aktoer_organisasjonsnummer_ugyldig,


        //Sjekklistekrav
        krav_utfylt,
        krav_sjekklistekrav_utfylt,
        krav_sjekklistekrav_oppfylt,
        krav_sjekklistekrav_sjekklistepunkt_kode_utfylt,
        krav_sjekklistekrav_sjekklistepunkt_kode_gyldig,
        krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt,


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
