using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Reporter.DataBase
{
    public class ArbeidstilsyneDB
    {
        private List<ValidationMessageStorageEntry> _validationMessageStorageEntry;
        public ArbeidstilsyneDB()
        {
            _validationMessageStorageEntry = new List<ValidationMessageStorageEntry>();
        }

        public void AddRuleToValidationMessageStorageEntry(string dataFormatVersion, ValidationRuleEnum id, string xPath, string message, ValidationResultSeverityEnum validationResultSeverity = ValidationResultSeverityEnum.WARNING, string languageCode = null, string checklistReference = null, string dataForm = null)
        {
            var validationMessageStorageEntry = new ValidationMessageStorageEntry()
            {
                Id = id,
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

        public List<ValidationMessageStorageEntry> InitiateMessageRepository()
        {
            var storage = new List<ValidationMessageStorageEntry>();
            storage.AddRange(InitiateArbeidstilsynetsSamtykke_41999());
            storage.AddRange(InitiateArbeidstilsynetsSamtykke2_45957());

            return storage;
        }

        private List<ValidationMessageStorageEntry> InitiateArbeidstilsynetsSamtykke_41999()
        {
            //Eiendombyggested
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendom_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}", "Eiendom må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bygningsnummer", "Bygningsnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bolignummer", "Bolignummer må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/kommunenavn", "Kommunenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/postnr", "Postnr {0} ligger ikke i {1} kommune", ValidationResultSeverityEnum.ERROR, null, "2.3");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse", "Eiendommens adresse må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje1", "Eiendommens adresselinje1 må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/landkode", "Eiendommens landkode må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_poststed_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_gatenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/gatenavn", "Eiendommens gatenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_husnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/husnr", "Eiendommens husnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_bokstav_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsadresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_kommunenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon", "Eiendommen må være utfyllt i Matrikkelen", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_gaardsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/gaardsnummer", "Eiendommens GNR i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_bruksnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/bruksnummer", "Eiendommens BNR i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_festenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/festenummer", "Eiendommens FNR i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.eiendomsidentifikasjon_seksjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/seksjonsnummer", "Eiendommens SNR i Matrikkelen må være utfyllt");


            //arbeidsplasser
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Arbeidsplasser må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_type_arbeid_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser/antallVirksomheter", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_utleieBygg, "ArbeidstilsynetsSamtykke/arbeidsplasser/utleieBygg", "Det skal angis hvormange ansatte som bygget dimensjoneres for.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.arbeidsplasser_beskrivelse, "ArbeidstilsynetsSamtykke/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");

            //Tiltakshaver
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver", "Informasjon om tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse", "Adresse bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje2", "Tru'kje'ru e go', sjå på adresse 2! For tiltakshaver.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/landkode", "Ugyldig landkode for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummer for tiltakshaver bør angis.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret til tiltakshaver ble ikke validert.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Tiltakshaver er ein gærning!!!");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Tiltakshavers postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_telmob_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver", "Mobilnummer eller telefonnummer for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_epost_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/epost", "Epost for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/navn", "Navnet til tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Fødselsnummer må angis når tiltakshaver er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_dekryptering, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Tiltakshaver Fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Tiltakshaver Fødselsnummeret er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_foedselnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Tiltakshaver Fødselsnummeret har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_organisasjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummer for tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_organisasjonsnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.aktoer_organisasjonsnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            //Partstype
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Kodeverdien for 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for foretak. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");

            //Kontaktpersjon
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.kontaktperson_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/kontaktperson/navn", "Navnet til kontaktperson for tiltakshaver bør fylles ut.");

            //fakturamottaker
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.fakturamottaker_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker", "Fakturainformasjon må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje2", "E'ru'kje go', du bør fylle ute adresse 2. For fakturamottaker.");
            //AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for fakturamottaker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret til fakturamottaker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresse bør fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresselinje 1 skal fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/landkode", "Ugyldig landkode for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Poststed for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Fakturamottakers postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry("41999", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Fakturamottaker er ein gærning!!!");

            //TODO "ArbeidstilsynetsSamtykke" to "ArbeidstilsynetsSamtykkeV2"/"ArbeidstilsynetsSamtykkeDfv45957"??  rule may need to have dfv in the first "node" in order to connect the text to the correct version and correct schema.

            return _validationMessageStorageEntry;
        }

        private List<ValidationMessageStorageEntry> InitiateArbeidstilsynetsSamtykke2_45957()
        {
            //Eiendombyggested
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendom_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}", "Eiendom må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_bygningsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bygningsnummer", "Bygningsnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_bolignummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bolignummer", "Bolignummer må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_kommunenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/kommunenavn", "Kommunenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_tillatte_postnr_i_kommune, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/postnr", "Postnr {0} ligger ikke i {1} kommune", ValidationResultSeverityEnum.ERROR, null, "2.3");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse", "Eiendommens adresse må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje1", "Eiendommens adresselinje1 må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/landkode", "Eiendommens landkode må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_poststed_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_gatenavn_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/gatenavn", "Eiendommens gatenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_husnr_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/husnr", "Eiendommens husnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_bokstav_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsadresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_kommunenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon", "Eiendommen må være utfyllt i Matrikkelen", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_gaardsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/gaardsnummer", "Eiendommens GNR i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_bruksnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/bruksnummer", "Eiendommens BNR i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_festenummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/festenummer", "Eiendommens FNR i Matrikkelen må være utfyllt");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.eiendomsidentifikasjon_seksjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/seksjonsnummer", "Eiendommens SNR i Matrikkelen må være utfyllt");


            //arbeidsplasser
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Arbeidsplasser må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_framtidige_eller_eksisterende_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_faste_eller_midlertidige_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_type_arbeid_utfylt, "ArbeidstilsynetsSamtykke/arbeidsplasser/antallVirksomheter", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_utleieBygg, "ArbeidstilsynetsSamtykke/arbeidsplasser/utleieBygg", "Det skal angis hvormange ansatte som bygget dimensjoneres for.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.arbeidsplasser_beskrivelse, "ArbeidstilsynetsSamtykke/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");

            //Tiltakshaver
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver", "Informasjon om tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse", "Adresse bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/landkode", "Ugyldig landkode for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummer for tiltakshaver bør angis.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Postnummeret til tiltakshaver ble ikke validert.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Tiltakshaver er ein gærning!!!");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/tiltakshaver/adresse/postnr", "Tiltakshavers postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_telmob_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/mobilnummer", "Telefon- eller mobilnummer for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_epost_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/epost", "Epost for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/navn", "Navnet til tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Fødselsnummer må angis når tiltakshaver er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_dekryptering, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummer for tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            
            //Tiltakshavers partstype
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Kodeverdien for tiltakshavers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/tiltakshaver/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");

            //Tiltakshavers kontaktpersjon
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kontaktperson_navn_utfylt, "ArbeidstilsynetsSamtykke/tiltakshaver/kontaktperson/navn", "Navnet til kontaktperson for tiltakshaver bør fylles ut.");

            //Ansvarlig søker
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker", "Informasjon om ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse", "Adresse bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/landkode", "Ugyldig landkode for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummer for ansvarlig søker bør angis.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for ansvarlig søker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Postnummeret til ansvarlig søker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Ansvarlig søker er ein gærning!!!");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/adresse/postnr", "Ansvarlig søkers postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_telmob_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/mobilnummer", "Telefon- eller mobilnummer for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_epost_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/epost", "Epost for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_navn_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/navn", "Navnet til ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Fødselsnummer må angis når ansvarlig søker er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_dekryptering, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_foedselnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummer for ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_kontrollsiffer, "ArbeidstilsynetsSamtykke/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.aktoer_organisasjonsnummer_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            //Ansvarlig søkers partstype
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeliste_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/partstype/kodeverdi", "Kodeverdien for ansvarlig søkers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kodeverdi_ugyldig, "ArbeidstilsynetsSamtykke/ansvarligSoeker/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");

            //Ansvarlig søkers kontaktpersjon
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.kontaktperson_navn_utfylt, "ArbeidstilsynetsSamtykke/ansvarligSoeker/kontaktperson/navn", "Navnet til kontaktperson for ansvarlig søker bør fylles ut.");


            //fakturamottaker
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.fakturamottaker_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker", "Fakturainformasjon må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje2_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje3_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_kontrollsiffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for fakturamottaker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ugyldig, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_ikke_validert, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummeret til fakturamottaker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresse bør fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_adresselinje1_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse", "Adresselinje 1 skal fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_landkode_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/landkode", "Ugyldig landkode for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_utfylt, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Postnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_stemmerIkke, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Poststed for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_4siffer, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Fakturamottakers postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.adresse_postnr_til_galningar, "ArbeidstilsynetsSamtykke/fakturamottaker/adresse/postnr", "Fakturamottaker er ein gærning!!!");

            //sjekklistekrav
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_utfylt, "ArbeidstilsynetsSamtykke/krav{0}", "Krav må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_utfylt, "ArbeidstilsynetsSamtykke/krav{0}/erKravOppfylt", "Sjekklistekrav 'erKravOppfylt' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_oppfylt, "ArbeidstilsynetsSamtykke/krav{0}/erKravOppfylt", "Sjekklistekrav 'erKravOppfylt' må være utfylt med 'true' eller 'false'", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må utfylles med kodeverdi", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_gyldig, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må ha tillatt kodeverdi", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("45957", ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt, "ArbeidstilsynetsSamtykke/krav{0}/sjekklistepunkt/kodebeskrivelse", "Kravets sjekklistepunkt må ha beskrivelse", ValidationResultSeverityEnum.ERROR);


            //TODO "ArbeidstilsynetsSamtykke" to "ArbeidstilsynetsSamtykkeV2"/"ArbeidstilsynetsSamtykkeDfv45957"??  rule may need to have dfv in the first "node" in order to connect the text to the correct version and correct schema.

            //Test
            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                DataFormatVersion = "45957",
                Id = ValidationRuleEnum.Parameter_Test,
                LanguageCode = "NO",
                XPath = "Unit{0}/Test/Parameter",
                Message = "Parameter 1 kommer har '{0}' og parameter 2 kommer har ({1})"
            });

            return _validationMessageStorageEntry;
        }
    }
}


