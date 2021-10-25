using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums;
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

        public void AddRuleToValidationMessageStorageEntry(string dataFormatId, string dataFormatVersion, object rule, string xPath, string message, ValidationResultSeverityEnum validationResultSeverity = ValidationResultSeverityEnum.WARNING, string languageCode = null, string checklistReference = null, string dataForm = null)
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
            validationMessageStorageEntry.DataFormatId = dataFormatId;
            validationMessageStorageEntry.DataFormatVersion = dataFormatVersion;
            _validationMessageStorageEntry.Add(validationMessageStorageEntry);
        }
        //**
        public List<ValidationMessageStorageEntry> InitiateMessageRepository()
        {
            var storage = new List<ValidationMessageStorageEntry>();
            storage.AddRange(InitiateATIL());

            return storage;
        }

        private List<ValidationMessageStorageEntry> InitiateATIL()
        {
            //Eiendombyggested
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}", "Du må oppgi hvilken eiendom/hvilke eiendommer byggesøknaden gjelder.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.numerisk, "/eiendomByggested/eiendom{0}/bygningsnummer", "Du har oppgitt følgende bygningsnummer for eiendom/byggested: ‘{0}'. Bygningsnummeret må være et tall.", ValidationResultSeverityEnum.ERROR,null,"1.6");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/eiendomByggested/eiendom{0}/bygningsnummer", "Bygningsnummer for eiendom/byggested må være større enn '0'.",ValidationResultSeverityEnum.ERROR, null,"1.6");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/eiendomByggested/eiendom{0}/bygningsnummer", "Når bygningsnummer [{0}] er oppgitt for eiendom/byggested, bør det være gyldig i matrikkelen på aktuelt matrikkelnummer. Du kan sjekke riktig bygningsnummer på https://seeiendom.no", ValidationResultSeverityEnum.WARNING, null,"1.6"); //Kommer
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/eiendomByggested/eiendom{0}/bolignummer", "Når bruksenhetsnummer/bolignummer er fylt ut for eiendom/byggested, må det følge riktig format (for eksempel H0101). Se https://www.kartverket.no/eiendom/adressering/bruksenhetsnummer/ ", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/kommunenavn", "Navnet på kommunen bør fylles ut for eiendom/byggested.");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon", "Du må oppgitt eiendomsidentifikasjon for eiendom/byggested.", ValidationResultSeverityEnum.ERROR, null, "1.2");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/kommunenummer", "Kommunenummer må fylles ut for eiendom/byggested.", ValidationResultSeverityEnum.ERROR, null, "1.2");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/kommunenummer", "En teknisk feil gjør at vi ikke kan bekrefte Kommunenummer du har oppgitt. Vi anbefaler at du sjekke følgende Kommunenummer:'{0}' er riktig. Du kan sjekke riktig kommunenummer på https://register.geonorge.no/sosi-kodelister/kommunenummer", ValidationResultSeverityEnum.WARNING, null, "1.2");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/kommunenummer", "Kommunenummeret '{0}' for eiendom/byggested finnes ikke i kodelisten. Du kan sjekke riktig kommunenummer på https://register.geonorge.no/sosi-kodelister/kommunenummer", ValidationResultSeverityEnum.ERROR, null, "1.2");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.status, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/kommunenummer", "Kommunenummeret '{0}' for eiendom/byggested har ugyldig status ({1}). Du kan sjekke status på https://register.geonorge.no/sosi-kodelister/kommunenummer ", ValidationResultSeverityEnum.ERROR, null, "1.2");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/gaardsnummer", "Gårdsnummer må fylles ut for eiendom/byggested.", ValidationResultSeverityEnum.ERROR, null, "1.3");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/gaardsnummer", "Gårdsnummer '{0}' for eiendom/byggested må være '0' eller større.", ValidationResultSeverityEnum.ERROR, null, "1.3");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/bruksnummer", "Bruksnummer må fylles ut for eiendom/byggested.", ValidationResultSeverityEnum.ERROR, null, "1.4");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/bruksnummer", "Bruksnummer '{0}' for eiendom/byggested må være '0' eller større.", ValidationResultSeverityEnum.ERROR, null, "1.4");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse", "Postadresse for eiendom/byggested bør fylles ut.", ValidationResultSeverityEnum.WARNING, null, "1.8");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/adresselinje1", "Adresselinje 1 for eiendom/byggested bør fylles ut.", ValidationResultSeverityEnum.WARNING, null, "1.8");
            //AddRuleToValidationMessageStorageEntry("10000", ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/gatenavn", "Du bør oppgi gatenavn, husnummer og eventuell bokstav for eiendom/byggested slik at adressen kan valideres mot matrikkelen. Du kan sjekke riktig adresse på https://seeiendom.no", ValidationResultSeverityEnum.WARNING, null, "1.8");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/gatenavn", "Du bør oppgi gatenavn for eiendom/byggested slik at adressen kan valideres mot matrikkelen. Du kan sjekke riktig adresse på https://seeiendom.no ", ValidationResultSeverityEnum.WARNING, null, "1.8");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/husnr", "Du bør oppgi husnummer og eventuell bokstav for eiendom/byggested slik at adressen kan valideres mot matrikkelen. Du kan sjekke riktig adresse på https://seeiendom.no",ValidationResultSeverityEnum.WARNING,null,"1.8");

            //**Not in standard validation from FTB
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/eiendomByggested/eiendom{0}/adresse/landkode", "Ugyldig landkode for eiendom.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/eiendomByggested/eiendom{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.tillatte_postnr_i_kommune, "/eiendomByggested/eiendom{0}/postnr", "Postnr {0} ligger ikke i {1} kommune", ValidationResultSeverityEnum.ERROR, null, "2.3");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/festenummer", "Eiendommens FNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.555555");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/seksjonsnummer", "Eiendommens SNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.6");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utgått, "/eiendomByggested/eiendom{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen er utgått", ValidationResultSeverityEnum.ERROR, null, "1.3");
            //**



            //arbeidsplasser
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/arbeidsplasser", "Arbeidsplasser må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.framtidige_eller_eksisterende_utfylt, "/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.faste_eller_midlertidige_utfylt, "/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/arbeidsplasser/antallVirksomheter", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/arbeidsplasser/antallAnsatte", "Det skal angis hvor mange ansatte som bygget dimensjoneres for.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/arbeidsplasser/veiledning", "Veileding bør være utfylt.");

            //Betaling
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/betaling", "Betaling må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/betaling/beskrivelse", "Beskrivelse må fylles ut", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/betaling/sum", "Sum må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/betaling/sum", "Sum må være større enn 0", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.numerisk, "/betaling/sum", "Sum må være en numerisk verdi", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/betaling/gebyrkategori", "Gebyrkategori må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/betaling/gebyrkategori", "Gebyrkategori må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/betaling/skalFaktureres", "SkalFaktureres må fylles ut", ValidationResultSeverityEnum.ERROR);


            //Tiltakshaver
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver", "Informasjon om tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse", "Adresse bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/tiltakshaver/adresse/landkode", "Ugyldig landkode for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/postnr", "Postnummer for tiltakshaver bør angis.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.postnr_stemmerIkke, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/tiltakshaver/adresse/postnr", "Postnummeret til tiltakshaver ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/adresse/postnr", "tiltakshavers postnr må bestå av 4 siffer");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.telmob_utfylt, "/tiltakshaver", "Telefon- eller mobilnummer for tiltakshaver bør fylles ut.");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/epost", "Tiltakshavers epostadresse bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/navn", "Tiltakshavers navn må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/telefonnummer", "Tiltakshavers telefonnummer må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/foedselsnummer", "Fødselsnummer må angis når tiltakshaver er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.dekryptering, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummer for tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);

            //Tiltakshavers partstype
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype", "Tiltakshavers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype/kodeverdi", "Tiltakshavers kodeverdi for 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/tiltakshaver/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/tiltakshaver/partstype/kodeverdi", "Kodeverdi for 'partstype' for tiltakshaver kunne ikke valideres.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype/kodebeskrivelse", "Beskrivelse for tiltakshavers 'partstype' for foretak må fylles ut.");
            //AddRuleToValidationMessageStorageEntry(null,null, KodeListValidationEnum.kodebeskrivelse_gyldig, "/tiltakshaver/partstype/kodebeskrivelse", "Ugyldig beskrivelse '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/tiltakshaver/partstype", "Ugyldig kodeliste for tiltakshaver.");

            //Tiltakshavers kontaktpersjon
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson", "Kontaktperson for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson/navn", "Navnet til kontaktperson for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson/telefonnummer", "Telefonnummeret til kontaktperson for tiltakshaver bør fylles ut.");

            //Ansvarlig søker
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarligSoeker", "Du må fylle ut informasjon om ansvarlig søker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry("10000", "1",ValidationRuleEnum.tillatt, "/ansvarligSoeker/partstype/kodeverdi", "Partstypen for ansvarlig søker må være et foretak eller en organisasjon.", ValidationResultSeverityEnum.ERROR);

            //Ansvarlig søkers partstype
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype", "Du må oppgi ‘partstype’. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype.",ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype/kodeverdi", "Kodeverdien for ‘partstype’ til ansvarlig søker må fylles ut.Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype",ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype/kodeverdi", "'{0}' er en ugyldig kodeverdi for partstypen til ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.validert, "/ansvarligSoeker/partstype/kodeverdi", "En teknisk feil gjør at vi ikke kan valider informasjon for ‘partstype’ til ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype", ValidationResultSeverityEnum.WARNING);

            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/navn", "Navnet til ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/epost", "E-postadressen til ansvarlig søker bør fylles ut.", ValidationResultSeverityEnum.WARNING);

            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.telmob_utfylt, "/ansvarligSoeker/telefonnummer", "Telefonnummeret eller mobilnummeret til ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/mobilnummer", "Mobilnummeret til ansvarlig søkers kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/telefonnummer", "Telefonnummeret til ansvarlig søkers kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse", "Adressen til ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/adresse/landkode", "Ugyldig landkode for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse/postnr", "Postnummer for ansvarlig søker bør angis.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.kontrollsiffer, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' til ansvarlig søker har ikke gyldig kontrollsiffer. Du kan sjekke riktig postnummer på http://adressesok.bring.no/", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' til ansvarlig søker er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.postnr_stemmerIkke, "/ansvarligSoeker/adresse/poststed", "Postnummeret '{0}' til ansvarlig søker stemmer ikke overens med poststedet '{1}'. Postnummeret er fra '{2}'. Du kan sjekke riktig postnummer/poststed på http://adressesok.bring.no/", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.validert, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' til ansvarlig søker ble ikke validert. Postnummeret kan være riktig, men en teknisk feil gjør at vi ikke kan bekrefte det.");
            //Ansvarlig søkers kontaktpersjon
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/kontaktperson", "Kontaktpersonen til ansvarlig søker bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/kontaktperson/navn", "Navnet til kontaktpersonen for ansvarlig søker bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.telmob_utfylt, "/ansvarligSoeker/kontaktperson/telefonnummer", "Telefonnummeret eller mobilnummeret til ansvarlig søkers kontaktperson bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/kontaktperson/telefonnummer", "Telefonnummeret til ansvarlig søkers kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/kontaktperson/mobilnummer", "Mobilnummeret til ansvarlig søkers kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/kontaktperson/epost", "E-postadressen til ansvarlig søkers kontaktperson bør fylles ut.");
            //Ansvarlig organisasjonsnummer
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret til ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.kontrollsiffer, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') til ansvarlig søker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') til ansvarlig søker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            //** Check Standard
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype/kodebeskrivelse", "Beskrivelse for ansvarlig søkers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype/kodebeskrivelse", "Ugyldig beskrivelse '{0}' i henhold til kodeliste for 'partstype' for ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype", "Ugyldig kodeliste for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/foedselsnummer", "Fødselsnummer må angis når ansvarlig søker er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.dekryptering, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.kontrollsiffer, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            //**

            //fakturamottaker
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker", "Fakturainformasjon må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/navn", "Fakturamottakers navn må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.ehf_eller_papir, "/fakturamottaker", "Det må angis om det ønskes faktura som EHF eller på papir.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/bestillerReferanse", "'BestillerReferanse' for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/fakturareferanser", "Fakturareferanser for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for fakturamottaker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for fakturamottaker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse", "Adresse bør fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for fakturamottaker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.postnr_stemmerIkke, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/fakturamottaker/adresse/postnr", "Postnummeret til fakturamottaker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse", "Adresselinje 1 skal fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/fakturamottaker/adresse/landkode", "Ugyldig landkode for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/postnr", "Postnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.postnr_stemmerIkke, "/fakturamottaker/adresse/postnr", "Poststed for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/fakturamottaker/adresse/postnr", "Fakturamottakers postnr må bestå av 4 siffer");


            //sjekklistekrav
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.krav_utfylt, "/krav{0}", "Krav må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, "k/rav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, "/krav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt med 'true' eller 'false'", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må utfylles med kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må ha tillatt kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Kravets sjekklistepunkt må ha beskrivelse", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Kravet må være dokumentert", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/krav/sjekklistekrav{0}", "Kravet '{0}' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/krav/sjekklistekrav{0}/sjekklistepunkt", "Kravet må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/krav/sjekklistekrav{0}/sjekklistepunkt", "Kodelisten for sjekklistepunkt må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/krav/sjekklistekrav{0}/sjekklistepunkt/kodeverdi", "Kodeverdien for sjekklistepunkt '{0}' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/krav/sjekklistekrav{0}/sjekklistepunkt/kodeverdi", "Kodeverdien for sjekklistepunkt '{0}' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/krav/sjekklistekrav{0}/sjekklistepunkt/kodeverdi", "Kodeverdien for sjekklistepunkt '{0}' kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/krav/sjekklistekrav{0}/sjekklistepunkt/kodebeskrivelse", "Sjekklistepunktet '{0}' må ha kodebeskrivelse utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/krav/sjekklistekrav{0}/sjekklistepunktsvar", "Sjekklistepunktet '{0}' må være besvart med ja/nei", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/krav/sjekklistekrav{0}/dokumentasjon", "Dokumentasjon er påkrevd for sjekklistekravet '{0}'", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.sjekklistepunkt_1_18_dokumentasjon_utfylt, "/krav/sjekklistekrav{0}/dokumentasjon", "Dokumentasjon er påkrevd for sjekklistekravet '{0}'", ValidationResultSeverityEnum.ERROR);
            
            //Beskrivelse av tiltak
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak", "Tiltak må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk", "Bruk for beskrivelse av tiltak må være utfyllt", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/beskrivPlanlagtFormaal", "Tiltakets formål må være beskrevet", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype", "Anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/anleggstype", "Ugyldig kodeliste for anleggstype.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kodeverdi for anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kodeverdi for anleggstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kodeverdi for anleggstype kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype/kodebeskrivelse", "Beskrivelse for anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/anleggstype/kodebeskrivelse", "Beskrivelse for anleggstype må være gyldig", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe", "Næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe", "Ugyldig kodeliste for næringsgruppe.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kodeverdi for næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kodeverdi for næringsgruppe må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kodeverdi for næringsgruppe kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodebeskrivelse", "Beskrivelse for næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodebeskrivelse", "Beskrivelse for næringsgruppe må være gyldig", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype", "Bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/bygningstype", "Ugyldig kodeliste for bygningstype.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kodeverdi for bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kodeverdi for bygningstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kodeverdi for bygningstype kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype/kodebeskrivelse", "Beskrivelse for bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/bygningstype/kodebeskrivelse", "Beskrivelse for bygningstype må være gyldig", ValidationResultSeverityEnum.ERROR);

            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Kode for tiltakets formål må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Ugyldig kodeliste for tiltaksformaal.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kodeverdi for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kodeverdi for tiltaksformaal må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kodeverdi for tiltaksformaal kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodebeskrivelse", "Beskrivelse for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodebeskrivelse", "Beskrivelse for tiltaksformaal må være gyldig", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type{0}", "Tiltakstype må være utfylt", ValidationResultSeverityEnum.CRITICAL);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/type{0}", "Ugyldig kodeliste for tiltakstype.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/type{0}/kodeverdi", "Kunne ikke validere kode for tiltakstype", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type{0}/kodeverdi", "Kodeverdi for tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/type{0}/kodeverdi", "Kodeverdi for tiltakstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type{0}/kodebeskrivelse", "Beskrivelse for tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/BRA", "Tiltakets bruttoareal må være utfylt", ValidationResultSeverityEnum.ERROR);

            //Metadata
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata", "Søknadens metadata må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/erNorskSvenskDansk", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/fraSluttbrukersystem", "Må fylle ut fraSluttbrukersystem med samme navn som brukt i registrering for Altinn API.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/unntattOffentlighet", "Det må besvares om søknad skal unntas offentlighet", ValidationResultSeverityEnum.ERROR);

            //Metadata ussikert om skal valideres
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/ftbId", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/prosjektnavn", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/sluttbrukersystemUrl", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/hovedinnsendingsnummer", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/metadata/klartForSigneringFraSluttbrukersystem", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);


            //arbeidstilsynetsSaksnummer
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/arbeidstilsynetsSaksnummer", "Informasjon om 'ArbeidstilsynetsSaksnummer' bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/arbeidstilsynetsSaksnummer/saksaar", "Arbeidstilsynets saksår bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/arbeidstilsynetsSaksnummer/sakssekvensnummer", "Arbeidstilsynets sakssekvensnummer bør fylles ut.", ValidationResultSeverityEnum.WARNING);

            //KommunensSaksnummer
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/kommunensSaksnummer", "Hvis du har mottatt kommunens saksnummer, må du oppgi dette.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/kommunensSaksnummer/saksaar", "Du må oppgi kommunens saksnummer med saksår.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/kommunensSaksnummer/sakssekvensnummer", "Du må oppgi kommunens saksnummer med sekvensnummer.", ValidationResultSeverityEnum.ERROR);

            //TODO "ArbeidstilsynetsSamtykke" to "ArbeidstilsynetsSamtykkeV2"/"ArbeidstilsynetsSamtykkeDfv45957"??  rule may need to have dfv in the first "node" in order to connect the text to the correct version and correct schema.

            //**ANSAKO
            // ErklaeringAnsvarsrett
            
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/fraSluttbrukersystem", "Feltet 'soeknadssystemetsReferanse' må være fylt ut. Ta kontakt med søknadssystemet du bruker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.gyldig, "/fraSluttbrukersystem", "Feltet 'soeknadssystemetsReferanse' må være en GUID. Ta kontakt med søknadssystemet du bruker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/prosjektnavn", "Hvis det er et prosjektnavn på byggesøknaden, bør du oppgi dette.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, null, ValidationRuleEnum.utfylt, "/prosjektnr", "Hvis det er et prosjektnummer på byggesøknaden, bør du oppgi dette.", ValidationResultSeverityEnum.WARNING);

            //Foretak
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak", "Du må fylle ut informasjon om det ansvarlige foretaket.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.tillatt, "/ansvarsrett/foretak/partstype/kodeverdi", "Kodeverdien for ‘partstype’ til foretaket må fylles ut. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype.", ValidationResultSeverityEnum.ERROR);
            //foretak partstype
            //Kan ikke validere hvis det er bare 'Foretal' bare det?
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/partstype", "Du må oppgi ‘partstype’ for foretak. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/partstype/kodeverdi", "Kodeverdien for ‘partstype’ til foretaket må fylles ut. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/partstype/kodeverdi", "'{0}' er en ugyldig kodeverdi for partstypen til foretaket. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/ansvarsrett/foretak/partstype/kodeverdi", "En teknisk feil gjør at vi ikke kan valider informasjon for ‘partstype’ til foretaket. Du kan sjekke riktig kodeverdi på  ", ValidationResultSeverityEnum.WARNING);

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/organisasjonsnummer", "Organisasjonsnummeret til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/organisasjonsnummer", "Organisasjonsnummeret ('{0}') til foretaket er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/ansvarsrett/foretak/organisasjonsnummer", "Organisasjonsnummeret ('{0}') til foretaket har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/navn", "Navnet til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/epost", "E-postadressen til foretaket bør fylles ut.");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.telmob_utfylt, "/ansvarsrett/foretak/telefonnummer", "Telefonnummeret eller mobilnummeret til foretaket bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/telefonnummer", "Telefonnummeret til foretaket må kun inneholde tall og '+'.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/mobilnummer", "Mobilnummeret til foretaket må kun inneholde tall og '+'.");


            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/adresse", "Adressen til foretaket bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for foretaket.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/adresse/landkode", "Ugyldig landkode til foretaket");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/adresse/postnr", "Postnummeret til foretaket bør fylles ut.");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.kontrollsiffer, "/ansvarsrett/foretak/adresse/postnr", "Postnummeret '{0}' til foretaket har ikke gyldig kontrollsiffer. Du kan sjekke riktig postnummer på http://adressesok.bring.no/", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/adresse/postnr", "Postnummeret '{0}' til foretaket er ikke gyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.postnr_stemmerIkke, "/ansvarsrett/foretak/adresse/poststed", "Postnummeret '{0}' til foretaket stemmer ikke overens med poststedet '{1}'. Postnummeret er fra '{2}'. Du kan sjekke riktig postnummer/poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/ansvarsrett/foretak/adresse/postnr", "Postnummeret '{0}' til foretaket ble ikke validert. Postnummeret kan være riktig, men en teknisk feil gjør at vi ikke kan bekrefte det.");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/kontaktperson", "Kontaktpersonen til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/kontaktperson/navn", "Navnet til kontaktpersonen for foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/foretak/kontaktperson/epost ", "E-postadressen til foretakets kontaktperson bør fylles ut.");
            
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.telmob_utfylt, "/ansvarsrett/foretak/kontaktperson/telefonnummer", "Telefonnummeret eller mobilnummeret til foretakets kontaktperson bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/kontaktperson/telefonnummer", "Telefonnummeret  til foretakets kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/foretak/kontaktperson/mobilnummer", "Mobilnummer til foretakets kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);
            //ansvarsområde
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}", "Du må definere minst ett ansvarsområde.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/beskrivelseAvAnsvarsomraade", "Du må definere minst ett ansvarsområde.", ValidationResultSeverityEnum.ERROR);
            //KodeverdiValidatorV2 - funksjon
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon", "Du må oppgi en funksjon for ansvarsområdet. Du kan sjekke gyldige funksjoner på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "Kodeverdien for ‘funksjon’ for ansvarsområdet må fylles ut. Du kan sjekke gyldige funksjoner på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "Kodeverdien for ‘funksjon’ kan ikke valideres", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "'{0}' er en ugyldig kodeverdi for funksjon. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodebeskrivelse", "Når funksjon er valgt, må kodebeskrivelse fylles ut.Du kan sjekke riktig godebeskrivelse på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodebeskrivelse", "Kodebeskrivelsen '{0}' stemmer ikke med den valgte kodeverdien for funksjon. Du kan sjekke riktig kodebeskrivelse på https://register.geonorge.no/byggesoknad/funksjon");

            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/beskrivelseAvAnsvarsomraade", "Du må fylle ut en beskrivelse av ansvarsområdet. Ansvarlig foretak kan endre beskrivelsen senere.");

            //KodeverdiValidatorV3 - tiltaksklasse
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.validert, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/tiltaksklasse/kodeverdi", "Kodeverdien for ‘tiltaksklasse’ kan ikke valideres", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/tiltaksklasse/kodeverdi", "'{0}' er en ugyldig kodeverdi for tiltaksklasse. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.utfylt, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/tiltaksklasse/kodebeskrivelse", "Når tiltaksklasse er valgt, må kodebeskrivelse fylles ut. Du kan sjekke riktig godebeskrivelse på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null,null, ValidationRuleEnum.gyldig, "/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/tiltaksklasse/kodebeskrivelse", "Kodebeskrivelsen '{0}' stemmer ikke med den valgte kodeverdien for tiltaksklasse. Du kan sjekke riktig kodebeskrivelse på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse");

            return _validationMessageStorageEntry;
        }

    }
}


