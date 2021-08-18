using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

namespace Dibk.Ftpb.Validation.Application.Reporter.DataBase
{
    public class ArbeidstilsyneDB
    {
        private List<ValidationMessageStorageEntry> _validationMessageStorageEntry;
        public ArbeidstilsyneDB()
        {
            _validationMessageStorageEntry = new List<ValidationMessageStorageEntry>();
        }

        public void AddRuleToValidationMessageStorageEntry(string dataFormatVersion, object rule, string xPath, string message, ValidationResultSeverityEnum validationResultSeverity = ValidationResultSeverityEnum.WARNING, string languageCode = null, string checklistReference = null, string dataForm = null)
        {
            var validationMessageStorageEntry = new ValidationMessageStorageEntry()
            {
                Rule = rule.ToString(),
                XPath = xPath,
                Message = message,
                ChecklistReference = checklistReference,
                Messagetype = validationResultSeverity
            };
            validationMessageStorageEntry.LanguageCode = string.IsNullOrEmpty(languageCode) ? "NO" : languageCode;
            //validationMessageStorageEntry.DataForm = string.IsNullOrEmpty(dataForm) ? "ArbeidstilsynetsSamtykkeV2" : dataForm;
            validationMessageStorageEntry.DataFormatVersion = dataFormatVersion;
            _validationMessageStorageEntry.Add(validationMessageStorageEntry);
        }
        //**
        public List<ValidationMessageStorageEntry> InitiateMessageRepository()
        {
            var storage = new List<ValidationMessageStorageEntry>();
            //storage.AddRange(InitiateArbeidstilsynetsSamtykke_41999());
            //storage.AddRange(InitiateArbeidstilsynetsSamtykke2_45957());
            storage.AddRange(InitiateATIL());

            return storage;
        }

        private List<ValidationMessageStorageEntry> InitiateArbeidstilsynetsSamtykke_41999()
        {
            //Eiendombyggested
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendom_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}", "Eiendom må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bygningsnummer", "Bygningsnr må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bolignummer", "Bolignummer må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/kommunenavn", "Kommunenavn må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/postnr", "Postnr {0} ligger ikke i {1} kommune", ValidationResultSeverityEnum.ERROR, null, "2.3");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse", "Eiendommens adresse må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje1", "Eiendommens adresselinje1 må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt", ValidationResultSeverityEnum.WARNING);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/landkode", "Eiendommens landkode må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_poststed_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_gatenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/gatenavn", "Eiendommens gatenavn må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_husnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/husnr", "Eiendommens husnr må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_bokstav_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_kommunenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon", "Eiendommen må være utfyllt i Matrikkelen", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_gaardsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/gaardsnummer", "Eiendommens GNR i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_bruksnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/bruksnummer", "Eiendommens BNR i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_festenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/festenummer", "Eiendommens FNR i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_seksjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/seksjonsnummer", "Eiendommens SNR i Matrikkelen må være utfyllt");


            ////arbeidsplasser
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "arbeidsplasser må fylles ut", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_type_arbeid_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser/antallVirksomheter", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_utleieBygg, "ArbeidstilsynetsSamtykke/arbeidsplasser/utleieBygg", "Det skal angis hvormange ansatte som bygget dimensjoneres for.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_beskrivelse, "ArbeidstilsynetsSamtykke/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");

            ////Tiltakshaver
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver", "Informasjon om tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse", "Adresse bør fylles ut for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje2", "Tru'kje'ru e go', sjå på adresse 2! For tiltakshaver.");
            ////AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/landkode", "Ugyldig landkode for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummer for tiltakshaver bør angis.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver har ikke gyldig kontrollsiffer.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret til tiltakshaver ble ikke validert.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "tiltakshaver er ein gærning!!!");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "tiltakshavers postnr må bestå av 4 siffer");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_telmob_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver", "Mobilnummer eller telefonnummer for tiltakshaver bør fylles ut.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_epost_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/epost", "Epost for tiltakshaver bør fylles ut.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/navn", "Navnet til tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Fødselsnummer må angis når tiltakshaver er privatperson.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_dekryptering, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "tiltakshaver Fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "tiltakshaver Fødselsnummeret er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "tiltakshaver Fødselsnummeret har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_organisasjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummer for tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_organisasjonsnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_organisasjonsnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            ////Partstype
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Kodeverdien for 'partstype' for foretak må fylles ut.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for foretak. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");

            ////Kontaktpersjon
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.kontaktperson_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/kontaktperson/navn", "Navnet til kontaktperson for tiltakshaver bør fylles ut.");

            ////fakturamottaker
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.fakturamottaker_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker", "Fakturainformasjon må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje2", "E'ru'kje go', du bør fylle ute adresse 2. For fakturamottaker.");
            ////AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for fakturamottaker har ikke gyldig kontrollsiffer.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret til fakturamottaker ble ikke validert.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresse bør fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresselinje 1 skal fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/landkode", "Ugyldig landkode for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Poststed for fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "fakturamottakers postnr må bestå av 4 siffer");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "fakturamottaker er ein gærning!!!");

            //TODO "ArbeidstilsynetsSamtykke" to "ArbeidstilsynetsSamtykkeV2"/"ArbeidstilsynetsSamtykkeDfv45957"??  rule may need to have dfv in the first "node" in order to connect the text to the correct version and correct schema.

            return _validationMessageStorageEntry;
        }

        private List<ValidationMessageStorageEntry> InitiateArbeidstilsynetsSamtykke2_45957()
        {
            ////Eiendombyggested
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendom_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}", "Eiendom må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bygningsnummer", "Bygningsnr må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bolignummer", "Bolignummer må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/kommunenavn", "Kommunenavn må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/postnr", "Postnr {0} ligger ikke i {1} kommune", ValidationResultSeverityEnum.ERROR, null, "2.3");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse", "Eiendommens adresse må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje1", "Eiendommens adresselinje1 må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt", ValidationResultSeverityEnum.WARNING);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/landkode", "Eiendommens landkode må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_poststed_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_gatenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/gatenavn", "Eiendommens gatenavn må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_husnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/husnr", "Eiendommens husnr må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_bokstav_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_kommunenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon", "Eiendommen må være utfyllt i Matrikkelen", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_gaardsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/gaardsnummer", "Eiendommens GNR i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_bruksnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/bruksnummer", "Eiendommens BNR i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_festenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/festenummer", "Eiendommens FNR i Matrikkelen må være utfyllt");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_seksjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/seksjonsnummer", "Eiendommens SNR i Matrikkelen må være utfyllt");


            ////arbeidsplasser
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "arbeidsplasser må fylles ut", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_type_arbeid_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser/antallVirksomheter", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_utleieBygg, "ArbeidstilsynetsSamtykke/arbeidsplasser/utleieBygg", "Det skal angis hvormange ansatte som bygget dimensjoneres for.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_beskrivelse, "ArbeidstilsynetsSamtykke/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");

            ////Tiltakshaver
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver", "Informasjon om tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse", "Adresse bør fylles ut for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/landkode", "Ugyldig landkode for tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummer for tiltakshaver bør angis.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver har ikke gyldig kontrollsiffer.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret til tiltakshaver ble ikke validert.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "tiltakshaver er ein gærning!!!");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "tiltakshavers postnr må bestå av 4 siffer");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_telmob_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/mobilnummer", "Telefon- eller mobilnummer for tiltakshaver bør fylles ut.");
            
            //AddRuleToValidationMessageStorageEntry("45957", AktoerValidationEnums.telmob_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/mobilnummer", "Telefon- eller mobilnummer for tiltakshaver bør fylles ut.");
            
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_epost_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/epost", "Epost for tiltakshaver bør fylles ut.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/navn", "Navnet til tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Fødselsnummer må angis når tiltakshaver er privatperson.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_dekryptering, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "tiltakshavers fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "tiltakshavers fødselsnummer er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "tiltakshavers fødselsnummer har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummer for tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            
            ////Tiltakshavers partstype
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Kodeverdien for tiltakshavers 'partstype' for foretak må fylles ut.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");

            ////Tiltakshavers kontaktpersjon
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kontaktperson_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/kontaktperson/navn", "Navnet til kontaktperson for tiltakshaver bør fylles ut.");

            ////Ansvarlig søker
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker", "Informasjon om ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse", "Adresse bør fylles ut for ansvarlig søker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for ansvarlig søker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for ansvarlig søker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for ansvarlig søker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/landkode", "Ugyldig landkode for ansvarlig søker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummer for ansvarlig søker bør angis.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for ansvarlig søker har ikke gyldig kontrollsiffer.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret til ansvarlig søker ble ikke validert.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Ansvarlig søker er ein gærning!!!");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Ansvarlig søkers postnr må bestå av 4 siffer");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_telmob_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/mobilnummer", "Telefon- eller mobilnummer for ansvarlig søker bør fylles ut.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_epost_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/epost", "Epost for ansvarlig søker bør fylles ut.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_navn_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/navn", "Navnet til ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Fødselsnummer må angis når ansvarlig søker er privatperson.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_dekryptering, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummer for ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            ////Ansvarlig søkers partstype
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/partstype/kodeverdi", "Kodeverdien for ansvarlig søkers 'partstype' for foretak må fylles ut.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");

            ////Ansvarlig søkers kontaktpersjon
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kontaktperson_navn_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/kontaktperson/navn", "Navnet til kontaktperson for ansvarlig søker bør fylles ut.");


            ////fakturamottaker
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.fakturamottaker_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker", "Fakturainformasjon må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for fakturamottaker har ikke gyldig kontrollsiffer.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret til fakturamottaker ble ikke validert.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresse bør fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresselinje 1 skal fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/landkode", "Ugyldig landkode for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Poststed for fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "fakturamottakers postnr må bestå av 4 siffer");
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "fakturamottaker er ein gærning!!!");

            ////sjekklistekrav
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_utfylt, "ArbeidstilsynetsSamtykke/krav{0}", "Krav må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt med 'true' eller 'false'", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må utfylles med kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_gyldig, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må ha tillatt kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunkt/kodebeskrivelse", "Kravets sjekklistepunkt må ha beskrivelse", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "ArbeidstilsynetsSamtykke/krav{0}/dokumentasjon", "Kravet må være dokumentert", ValidationResultSeverityEnum.ERROR);
            
            ////Beskrivelse av tiltak
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.beskrivelseAvTiltak_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak", "Krav må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.beskrivelseAvTiltak_anleggstype_kode_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/anleggstype", "Anleggstype må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.beskrivelseAvTiltak_naeringsgruppe_kode_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/naeringsgruppe", "Næringsgruppe må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.beskrivelseAvTiltak_bygningstype_kode_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/bygningstype", "Bygningstype må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.beskrivelseAvTiltak_tiltakformaal_kode_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/tiltaksformaal", "Kode for tiltakets formål må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.beskrivelseAvTiltak_beskrivPlanlagtFormaal_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/beskrivPlanlagtFormaal", "Tiltakets formål må være beskrevet", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kode for anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kode for anleggstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kode for næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kode for næringsgruppe må være gyldig", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kode for bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kode for bygningstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kode for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kode for tiltaksformaal må være gyldig", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kode for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kode for tiltaksformaal må være gyldig", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/type/kodeverdi", "Kode for tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/beskrivelseAvTiltak/type/kodeverdi", "Kode for tiltakstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            
                
            ////TODO "ArbeidstilsynetsSamtykke" to "ArbeidstilsynetsSamtykkeV2"/"ArbeidstilsynetsSamtykkeDfv45957"??  rule may need to have dfv in the first "node" in order to connect the text to the correct version and correct schema.

            ////Test
            //_validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            //{
            //    DataFormatVersion = "45957",
            //    Id = ValidationRuleEnum.Parameter_Test,
            //    LanguageCode = "NO",
            //    XPath = "Unit{0}/Test/Parameter",
            //    Message = "Parameter 1 kommer har '{0}' og parameter 2 kommer har ({1})"
            //});

            return _validationMessageStorageEntry;
        }




        private List<ValidationMessageStorageEntry> InitiateATIL
            ()
        {
            //Eiendombyggested
            AddRuleToValidationMessageStorageEntry(null, EiendomValidationEnum.utfylt, "/eiendomByggested{0}", "Eiendom må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EiendomValidationEnum.bygningsnummer_utfylt, "/eiendomByggested{0}/bygningsnummer", "Bygningsnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomValidationEnum.bolignummer_utfylt, "/eiendomByggested{0}/bolignummer", "Bolignummer må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomValidationEnum.kommunenavn_utfylt, "/eiendomByggested{0}/kommunenavn", "Kommunenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomValidationEnum.tillatte_postnr_i_kommune, "/eiendomByggested{0}/postnr", "Postnr {0} ligger ikke i {1} kommune", ValidationResultSeverityEnum.ERROR, null, "2.3");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.utfylt, "/eiendomByggested{0}/adresse", "Eiendommens adresse må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.adresselinje1_utfylt, "/eiendomByggested{0}/adresse/adresselinje1", "Eiendommens adresselinje1 må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.adresselinje2_utfylt, "/eiendomByggested{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.adresselinje3_utfylt, "/eiendomByggested{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.landkode_utfylt, "/eiendomByggested{0}/adresse/landkode", "Eiendommens landkode må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.postnr_utfylt, "/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.poststed_utfylt, "/eiendomByggested{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.gatenavn_utfylt, "/eiendomByggested{0}/adresse/gatenavn", "Eiendommens gatenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.husnr_utfylt, "/eiendomByggested{0}/adresse/husnr", "Eiendommens husnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.bokstav_utfylt, "/eiendomByggested{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, EiendomsAdresseValidationEnum.postnr_4siffer, "/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon", "Eiendommen må være utfyllt i Matrikkelen", ValidationResultSeverityEnum.ERROR, null, "1.2");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.3");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være gyldig", ValidationResultSeverityEnum.ERROR, null, "1.3");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/gaardsnummer", "Eiendommens GNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.4");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/bruksnummer", "Eiendommens BNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.5");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/festenummer", "Eiendommens FNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.555555");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/seksjonsnummer", "Eiendommens SNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.6");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kommunenummer_utgått, "/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen er utgått", ValidationResultSeverityEnum.ERROR, null, "1.3");


            //arbeidsplasser
            AddRuleToValidationMessageStorageEntry(null, ArbeidsplasserValidationEnum.utfylt, "/arbeidsplasser", "Arbeidsplasser må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ArbeidsplasserValidationEnum.framtidige_eller_eksisterende_utfylt, "/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            AddRuleToValidationMessageStorageEntry(null, ArbeidsplasserValidationEnum.faste_eller_midlertidige_utfylt, "/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            AddRuleToValidationMessageStorageEntry(null, ArbeidsplasserValidationEnum.type_arbeid_utfylt, "/arbeidsplasser/antallVirksomheter", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            AddRuleToValidationMessageStorageEntry(null, ArbeidsplasserValidationEnum.utleieBygg, "/arbeidsplasser/utleieBygg", "Det skal angis hvormange ansatte som bygget dimensjoneres for.");
            AddRuleToValidationMessageStorageEntry(null, ArbeidsplasserValidationEnum.beskrivelse, "/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");

            //Tiltakshaver
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.utfylt, "/tiltakshaver", "Informasjon om tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.utfylt, "/tiltakshaver/adresse", "Adresse bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje1_utfylt, "/tiltakshaver/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje2_utfylt, "/tiltakshaver/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje3_utfylt, "/tiltakshaver/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.landkode_utfylt, "/tiltakshaver/adresse/landkode", "Ugyldig landkode for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_utfylt, "/tiltakshaver/adresse/postnr", "Postnummer for tiltakshaver bør angis.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_gyldig, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_kontrollsiffer, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_stemmerIkke, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_ikke_validert, "/tiltakshaver/adresse/postnr", "Postnummeret til tiltakshaver ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_4siffer, "/tiltakshaver/adresse/postnr", "tiltakshavers postnr må bestå av 4 siffer");

            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.telmob_utfylt, "/tiltakshaver/mobilnummer", "Telefon- eller mobilnummer for tiltakshaver bør fylles ut.");

            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.epost_utfylt, "/tiltakshaver/epost", "Epost for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.navn_utfylt, "/tiltakshaver/navn", "Navnet til tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_utfylt, "/tiltakshaver/foedselsnummer", "Fødselsnummer må angis når tiltakshaver er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_dekryptering, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_gyldig, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_kontrollsiffer, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.organisasjonsnummer_utfylt, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummer for tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.organisasjonsnummer_kontrollsiffer, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.organisasjonsnummer_gyldig, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            //Tiltakshavers partstype
            AddRuleToValidationMessageStorageEntry(null, KodeListValidationEnum.utfylt, "/tiltakshaver/partstype", "Tiltakshavers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, KodeListValidationEnum.kodeverdi_utfylt, "/tiltakshaver/partstype", "Tiltakshavers kodeverdi for 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype/kodeverdi", "Kodeverdien for tiltakshavers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/tiltakshaver/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype/kodebeskrivelse", "Beskrivelse for tiltakshavers 'partstype' for foretak må fylles ut.");
            //AddRuleToValidationMessageStorageEntry(null, KodeListValidationEnum.kodebeskrivelse_gyldig, "/tiltakshaver/partstype/kodebeskrivelse", "Ugyldig beskrivelse '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kodeliste_gyldig, "/tiltakshaver/partstype", "Ugyldig kodeliste for tiltakshaver.");

            //Tiltakshavers kontaktpersjon
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson/navn", "Navnet til kontaktperson for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson/telefonnummer", "Telefonnummeret til kontaktperson for tiltakshaver bør fylles ut.");

            //Ansvarlig søker
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.utfylt, "/ansvarligSoeker", "Informasjon om ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.utfylt, "/ansvarligSoeker/adresse", "Adresse bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje1_utfylt, "/ansvarligSoeker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje2_utfylt, "/ansvarligSoeker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje3_utfylt, "/ansvarligSoeker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.landkode_utfylt, "/ansvarligSoeker/adresse/landkode", "Ugyldig landkode for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_utfylt, "/ansvarligSoeker/adresse/postnr", "Postnummer for ansvarlig søker bør angis.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_gyldig, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for ansvarlig søker er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_kontrollsiffer, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for ansvarlig søker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_stemmerIkke, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_ikke_validert, "/ansvarligSoeker/adresse/postnr", "Postnummeret til ansvarlig søker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_4siffer, "/ansvarligSoeker/adresse/postnr", "Ansvarlig søkers postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.telmob_utfylt, "/ansvarligSoeker/mobilnummer", "Telefon- eller mobilnummer for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.epost_utfylt, "/ansvarligSoeker/epost", "Epost for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.navn_utfylt, "/ansvarligSoeker/navn", "Navnet til ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_utfylt, "/ansvarligSoeker/foedselsnummer", "Fødselsnummer må angis når ansvarlig søker er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_dekryptering, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_gyldig, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.foedselnummer_kontrollsiffer, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.organisasjonsnummer_utfylt, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummer for ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.organisasjonsnummer_kontrollsiffer, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.organisasjonsnummer_gyldig, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            //Ansvarlig søkers partstype
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype", "Ansvarlig søkers 'partstype' for foretak må fylles ut.");
            //AddRuleToValidationMessageStorageEntry(null, KodeListValidationEnum.kodeverdi_utfylt, "/ansvarligSoeker/partstype", "Ansvarlig søkers kodeverdi for 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype/kodeverdi", "Kodeverdien for ansvarlig søkers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype/kodebeskrivelse", "Beskrivelse for ansvarlig søkers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype/kodebeskrivelse", "Ugyldig beskrivelse '{0}' i henhold til kodeliste for 'partstype' for ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kodeliste_gyldig, "/ansvarligSoeker/partstype", "Ugyldig kodeliste for ansvarlig søker.");
            
            //Ansvarlig søkers kontaktpersjon
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/kontaktperson/navn", "Navnet til kontaktperson for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/kontaktperson/telefonnummer", "Telefonnummer til kontaktperson for ansvarlig søker bør fylles ut.");


            //fakturamottaker
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker", "Fakturainformasjon må fylles ut.", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.utfylt, "/fakturamottaker/adresse", "Adresse bør fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje1_utfylt, "/fakturamottaker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje2_utfylt, "/fakturamottaker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje3_utfylt, "/fakturamottaker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_kontrollsiffer, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for fakturamottaker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_gyldig, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_stemmerIkke, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_ikke_validert, "/fakturamottaker/adresse/postnr", "Postnummeret til fakturamottaker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.adresselinje1_utfylt, "/fakturamottaker/adresse", "Adresselinje 1 skal fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.landkode_utfylt, "/fakturamottaker/adresse/landkode", "Ugyldig landkode for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_utfylt, "/fakturamottaker/adresse/postnr", "Postnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_stemmerIkke, "/fakturamottaker/adresse/postnr", "Poststed for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, EnkelAdresseValidationEnum.postnr_4siffer, "/fakturamottaker/adresse/postnr", "Fakturamottakers postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, AktoerValidationEnum.organisasjonsnummer_kontrollsiffer, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for fakturamottaker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for fakturamottaker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            //sjekklistekrav
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_utfylt, "/krav{0}", "Krav må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, "k/rav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, "/krav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt med 'true' eller 'false'", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må utfylles med kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må ha tillatt kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Kravets sjekklistepunkt må ha beskrivelse", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Kravet må være dokumentert", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.utfylt, "/krav{0}", "Kravet må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.sjekklistepunkt_mangler, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '{0}' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.kodevbeskrivelse_mangler, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Sjekklistepunktet '{0}' må ha kodebeskrivelse utfylt", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_14_kodeverdi_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '1.14' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_14_kodeverdi_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '1.14' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_14_kodebeskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Beskrivelse for '1.14' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_14_sjekklistepunktsvar_utfylt, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '1.14' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_14_sjekklistepunktsvar_gyldig, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '1.14' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_14_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Dokumentasjon for sjekklistepunkt '1.14' må være utfylt", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_17_kodeverdi_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '1.17' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_17_kodeverdi_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '1.17' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_17_kodebeskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Beskrivelse for '1.17' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_17_sjekklistepunktsvar_utfylt, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '1.17' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_17_sjekklistepunktsvar_gyldig, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '1.17' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_1_17_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Dokumentasjon for sjekklistepunkt '1.17' må være utfylt", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_1_kodeverdi_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.1' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_1_kodeverdi_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.1' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_1_kodebeskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Beskrivelse for '10.1' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_1_sjekklistepunktsvar_utfylt, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.1' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_1_sjekklistepunktsvar_gyldig, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.1' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_1_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Dokumentasjon for sjekklistepunkt '10.1' må være utfylt", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_2_kodeverdi_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.2' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_2_kodeverdi_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.2' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_2_kodebeskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Beskrivelse for '10.2' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_2_sjekklistepunktsvar_utfylt, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.2' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_2_sjekklistepunktsvar_gyldig, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.2' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_2_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Dokumentasjon for sjekklistepunkt '10.2' må være utfylt", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_3_kodeverdi_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.3' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_3_kodeverdi_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.3' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_3_kodebeskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Beskrivelse for '10.3' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_3_sjekklistepunktsvar_utfylt, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.3' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_3_sjekklistepunktsvar_gyldig, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.3' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_3_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Dokumentasjon for sjekklistepunkt '10.3' må være utfylt", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_4_kodeverdi_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.4' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_4_kodeverdi_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Sjekklistepunktet '10.4' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_4_kodebeskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Beskrivelse for '10.4' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_4_sjekklistepunktsvar_utfylt, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.4' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_4_sjekklistepunktsvar_gyldig, "/krav{0}/sjekklistepunktsvar", "Svaret for sjekklistepunkt '10.4' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ATILSjekklistekravEnum.pkt_10_4_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Dokumentasjon for sjekklistepunkt '10.4' må være utfylt", ValidationResultSeverityEnum.ERROR);




            //Beskrivelse av tiltak
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak", "Krav må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk", "Bruk for beskrivelse av tiltak må være utfyllt", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/beskrivPlanlagtFormaal", "Tiltakets formål må være beskrevet", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype", "Anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kodeliste_gyldig, "/beskrivelseAvTiltak/bruk/anleggstype", "Ugyldig kodeliste for anleggstype.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kode for anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kode for anleggstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype/kodebeskrivelse", "Beskrivelse for anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe", "Næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kodeliste_gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe", "Ugyldig kodeliste for næringsgruppe.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kode for næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kode for næringsgruppe må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodebeskrivelse", "Beskrivelse for næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype", "Bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kodeliste_gyldig, "/beskrivelseAvTiltak/bruk/bygningstype", "Ugyldig kodeliste for bygningstype.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kode for bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kode for bygningstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype/kodebeskrivelse", "Beskrivelse for bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Kode for tiltakets formål må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kodeliste_gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Ugyldig kodeliste for tiltaksformaal.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kode for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kode for tiltaksformaal må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodebeskrivelse", "Beskrivelse for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type", "Tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kodeliste_gyldig, "/beskrivelseAvTiltak/type", "Ugyldig kodeliste for tiltakstype.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type/kodeverdi", "Kode for tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/type/kodeverdi", "Kode for tiltakstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type/kodebeskrivelse", "Beskrivelse for tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/BRA", "Tiltakets bruttoareal må være utfylt", ValidationResultSeverityEnum.ERROR);


            //TODO "ArbeidstilsynetsSamtykke" to "ArbeidstilsynetsSamtykkeV2"/"ArbeidstilsynetsSamtykkeDfv45957"??  rule may need to have dfv in the first "node" in order to connect the text to the correct version and correct schema.

            //Test
            //_validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            //{
            //    DataFormatVersion = null,
            //    Id = ValidationRuleEnum.Parameter_Test,
            //    LanguageCode = "NO",
            //    XPath = "/Unit{0}/Test/Parameter",
            //    Message = "Parameter 1 kommer har '{0}' og parameter 2 kommer har ({1})"
            //});

            return _validationMessageStorageEntry;
        }

    }
}


