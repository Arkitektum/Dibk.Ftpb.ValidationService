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
            storage.AddRange(InitiateATIL());

            return storage;
        }

        private List<ValidationMessageStorageEntry> InitiateATIL()
        {
            //Eiendombyggested
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}", "Eiendom må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/bygningsnummer", "Bygningsnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/eiendomByggested{0}/bygningsnummer", "Bygningsnr '{0}' må være gyldig");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/bolignummer", "Bolignummer må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/kommunenavn", "Kommunenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.tillatte_postnr_i_kommune, "/eiendomByggested{0}/postnr", "Postnr {0} ligger ikke i {1} kommune", ValidationResultSeverityEnum.ERROR, null, "2.3");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse", "Eiendommens adresse må være utfyllt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/adresselinje1", "Eiendommens adresselinje1 må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/eiendomByggested{0}/adresse/landkode", "Ugyldig landkode for eiendom.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/gatenavn", "Eiendommens gatenavn må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/husnr", "Eiendommens husnr må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon", "Eiendommen må være utfyllt i Matrikkelen", ValidationResultSeverityEnum.ERROR, null, "1.2");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.3");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være gyldig", ValidationResultSeverityEnum.ERROR, null, "1.3");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/gaardsnummer", "Eiendommens GNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.4");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/bruksnummer", "Eiendommens BNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.5");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/festenummer", "Eiendommens FNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.555555");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/eiendomByggested{0}/eiendomsidentifikasjon/seksjonsnummer", "Eiendommens SNR i Matrikkelen må være utfyllt", ValidationResultSeverityEnum.ERROR, null, "1.6");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utgått, "/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen er utgått", ValidationResultSeverityEnum.ERROR, null, "1.3");


            //arbeidsplasser
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/arbeidsplasser", "Arbeidsplasser må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.framtidige_eller_eksisterende_utfylt, "/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.faste_eller_midlertidige_utfylt, "/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/arbeidsplasser/antallVirksomheter", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/arbeidsplasser/antallAnsatte", "Det skal angis hvor mange ansatte som bygget dimensjoneres for.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/arbeidsplasser/veiledning", "Veileding bør være utfylt.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.sjekklistepunkt_1_17_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Kodeverdien for sjekklistepunkt '1.17' må være utfylt", ValidationResultSeverityEnum.ERROR);

            //Betaling
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/betaling", "Betaling må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/betaling/beskrivelse", "Beskrivelse må fylles ut", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/betaling/sum", "Sum må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/betaling/sum", "Sum må være større enn 0", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.numerisk, "/betaling/sum", "Sum må være en numerisk verdi", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/betaling/gebyrkategori", "Gebyrkategori må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/betaling/gebyrkategori", "Gebyrkategori må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/betaling/skalFaktureres", "SkalFaktureres må fylles ut", ValidationResultSeverityEnum.ERROR);


            //Tiltakshaver
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver", "Informasjon om tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse", "Adresse bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/tiltakshaver/adresse/landkode", "Ugyldig landkode for tiltakshaver.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/adresse/postnr", "Postnummer for tiltakshaver bør angis.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for tiltakshaver har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.postnr_stemmerIkke, "/tiltakshaver/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/tiltakshaver/adresse/postnr", "Postnummeret til tiltakshaver ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/adresse/postnr", "tiltakshavers postnr må bestå av 4 siffer");

            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.telmob_utfylt, "/tiltakshaver", "Telefon- eller mobilnummer for tiltakshaver bør fylles ut.");

            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/epost", "Tiltakshavers epostadresse bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/navn", "Tiltakshavers navn må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/telefonnummer", "Tiltakshavers telefonnummer må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/foedselsnummer", "Fødselsnummer må angis når tiltakshaver er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.dekryptering, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/foedselsnummer", "Tiltakshavers fødselsnummer har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummer for tiltakshaver må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/tiltakshaver/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for tiltakshaver har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);

            //Tiltakshavers partstype
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype", "Tiltakshavers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype/kodeverdi", "Tiltakshavers kodeverdi for 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/tiltakshaver/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/tiltakshaver/partstype/kodeverdi", "Kodeverdi for 'partstype' for tiltakshaver kunne ikke valideres.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/partstype/kodebeskrivelse", "Beskrivelse for tiltakshavers 'partstype' for foretak må fylles ut.");
            //AddRuleToValidationMessageStorageEntry(null, KodeListValidationEnum.kodebeskrivelse_gyldig, "/tiltakshaver/partstype/kodebeskrivelse", "Ugyldig beskrivelse '{0}' i henhold til kodeliste for 'partstype' for tiltakshaver. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/tiltakshaver/partstype", "Ugyldig kodeliste for tiltakshaver.");

            //Tiltakshavers kontaktpersjon
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson", "Kontaktperson for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson/navn", "Navnet til kontaktperson for tiltakshaver bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/tiltakshaver/kontaktperson/telefonnummer", "Telefonnummeret til kontaktperson for tiltakshaver bør fylles ut.");

            //Ansvarlig søker
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker", "Informasjon om ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse", "Adresse bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/adresse/landkode", "Ugyldig landkode for ansvarlig søker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/adresse/postnr", "Postnummer for ansvarlig søker bør angis.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for ansvarlig søker er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for ansvarlig søker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.postnr_stemmerIkke, "/ansvarligSoeker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/ansvarligSoeker/adresse/postnr", "Postnummeret til ansvarlig søker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/ansvarligSoeker/adresse/postnr", "Ansvarlig søkers postnr må bestå av 4 siffer");
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.telmob_utfylt, "/ansvarligSoeker", "Telefon- eller mobilnummer for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/epost", "Epost for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/navn", "Navnet til ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/foedselsnummer", "Fødselsnummer må angis når ansvarlig søker er privatperson.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.dekryptering, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummer kan ikke dekrypteres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/ansvarligSoeker/foedselsnummer", "Ansvarlig søker Fødselsnummeret har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummer for ansvarlig søker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for ansvarlig søker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            //Ansvarlig søkers partstype
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype", "Ansvarlig søkers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype/kodeverdi", "Kodeverdien for ansvarlig søkers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype/kodeverdi", "Ugyldig kodeverdi '{0}' i henhold til kodeliste for 'partstype' for ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/ansvarligSoeker/partstype/kodeverdi", "Kodeverdi for 'partstype' for ansvarlig søker kunne ikke valideres.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/partstype/kodebeskrivelse", "Beskrivelse for ansvarlig søkers 'partstype' for foretak må fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype/kodebeskrivelse", "Ugyldig beskrivelse '{0}' i henhold til kodeliste for 'partstype' for ansvarlig søker. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ansvarligSoeker/partstype", "Ugyldig kodeliste for ansvarlig søker.");
            
            //Ansvarlig søkers kontaktpersjon
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/kontaktperson/navn", "Navnet til kontaktperson for ansvarlig søker bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ansvarligSoeker/kontaktperson/telefonnummer", "Telefonnummer til kontaktperson for ansvarlig søker bør fylles ut.");

            //fakturamottaker
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker", "Fakturainformasjon må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/navn", "Fakturamottakers navn må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.ehf_eller_papir, "/fakturamottaker", "Det må angis om det ønskes faktura som EHF eller på papir.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/bestillerReferanse", "'BestillerReferanse' for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/fakturareferanser", "Fakturareferanser for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for fakturamottaker har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/fakturamottaker/organisasjonsnummer", "Organisasjonsnummeret ('{0}') for fakturamottaker er ikke gyldig.", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse", "Adresse bør fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/adresselinje2", "Adresselinje 2 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/adresselinje3", "Adresselinje 3 bør fylles ut for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for fakturamottaker har ikke gyldig kontrollsiffer.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {1} er ugyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.postnr_stemmerIkke, "/fakturamottaker/adresse/postnr", "Postnummeret '{0}' for {3} stemmer ikke overens med poststedet '{1}'. Riktig postnummer er '{2}'. Du kan sjekke riktig poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/fakturamottaker/adresse/postnr", "Postnummeret til fakturamottaker ble ikke validert.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse", "Adresselinje 1 skal fylles ut for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/fakturamottaker/adresse/landkode", "Ugyldig landkode for fakturamottaker.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/fakturamottaker/adresse/postnr", "Postnummer for fakturamottaker må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.postnr_stemmerIkke, "/fakturamottaker/adresse/postnr", "Poststed for fakturamottaker.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/fakturamottaker/adresse/postnr", "Fakturamottakers postnr må bestå av 4 siffer");
            

            //sjekklistekrav
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_utfylt, "/krav{0}", "Krav må være utfyllt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_utfylt, "k/rav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunktsvar_oppfylt, "/krav{0}/sjekklistepunktsvar", "Sjekklistepunktsvar for {0} må være utfylt med 'true' eller 'false'", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må utfylles med kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_kode_gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Kravets sjekklistepunkt må ha tillatt kodeverdi", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_sjekklistepunkt_beskrivelse_utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Kravets sjekklistepunkt må ha beskrivelse", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.krav_sjekklistekrav_dokumentasjon_utfylt, "/krav{0}/dokumentasjon", "Kravet må være dokumentert", ValidationResultSeverityEnum.ERROR);


            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/krav{0}", "Kravet må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/krav{0}/sjekklistepunkt", "Kravet må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/krav{0}/sjekklistepunkt", "Kodelisten for sjekklistepunkt må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/krav{0}/sjekklistepunkt/kodeverdi", "Kodeverdien for sjekklistepunkt '{0}' må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/krav{0}/sjekklistepunkt/kodeverdi", "Kodeverdien for sjekklistepunkt '{0}' må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/krav{0}/sjekklistepunkt/kodeverdi", "Kodeverdien for sjekklistepunkt '{0}' kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/krav{0}/sjekklistepunkt/kodebeskrivelse", "Sjekklistepunktet '{0}' må ha kodebeskrivelse utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/krav{0}/sjekklistepunktsvar", "Sjekklistepunktet '{0}' må være besvart med ja/nei", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/krav{0}/dokumentasjon", "Dokumentasjon er påkrevd for sjekklistekravet", ValidationResultSeverityEnum.ERROR);
            
 



            //Beskrivelse av tiltak
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak", "Tiltak må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk", "Bruk for beskrivelse av tiltak må være utfyllt", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/beskrivPlanlagtFormaal", "Tiltakets formål må være beskrevet", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype", "Anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/anleggstype", "Ugyldig kodeliste for anleggstype.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kodeverdi for anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kodeverdi for anleggstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/anleggstype/kodeverdi", "Kodeverdi for anleggstype kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/anleggstype/kodebeskrivelse", "Beskrivelse for anleggstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/anleggstype/kodebeskrivelse", "Beskrivelse for anleggstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe", "Næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe", "Ugyldig kodeliste for næringsgruppe.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kodeverdi for næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kodeverdi for næringsgruppe må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodeverdi", "Kodeverdi for næringsgruppe kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodebeskrivelse", "Beskrivelse for næringsgruppe må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/naeringsgruppe/kodebeskrivelse", "Beskrivelse for næringsgruppe må være gyldig", ValidationResultSeverityEnum.ERROR);
            
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype", "Bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/bygningstype", "Ugyldig kodeliste for bygningstype.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kodeverdi for bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kodeverdi for bygningstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/bygningstype/kodeverdi", "Kodeverdi for bygningstype kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/bygningstype/kodebeskrivelse", "Beskrivelse for bygningstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/bygningstype/kodebeskrivelse", "Beskrivelse for bygningstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Kode for tiltakets formål må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal", "Ugyldig kodeliste for tiltaksformaal.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kodeverdi for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kodeverdi for tiltaksformaal må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodeverdi", "Kodeverdi for tiltaksformaal kunne ikke valideres", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodebeskrivelse", "Beskrivelse for tiltaksformaal må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/bruk/tiltaksformaal/kodebeskrivelse", "Beskrivelse for tiltaksformaal må være gyldig", ValidationResultSeverityEnum.ERROR);
            
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type{0}", "Tiltakstype må være utfylt", ValidationResultSeverityEnum.CRITICAL);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/type{0}", "Ugyldig kodeliste for tiltakstype.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/beskrivelseAvTiltak/type{0}/kodeverdi", "Kunne ikke validere kode for tiltakstype", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type{0}/kodeverdi", "Kodeverdi for tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/beskrivelseAvTiltak/type{0}/kodeverdi", "Kodeverdi for tiltakstype må være gyldig", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/type{0}/kodebeskrivelse", "Beskrivelse for tiltakstype må være utfylt", ValidationResultSeverityEnum.ERROR);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/beskrivelseAvTiltak/BRA", "Tiltakets bruttoareal må være utfylt", ValidationResultSeverityEnum.ERROR);

            //Metadata
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata", "Søknadens metadata må fylles ut", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/erNorskSvenskDansk", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/fraSluttbrukersystem", "Må fylle ut fraSluttbrukersystem med samme navn som brukt i registrering for Altinn API.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/unntattOffentlighet", "Det må besvares om søknad skal unntas offentlighet", ValidationResultSeverityEnum.ERROR);

            //Metadata ussikert om skal valideres
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/ftbId", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/prosjektnavn", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/sluttbrukersystemUrl", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/hovedinnsendingsnummer", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);
            //AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/metadata/klartForSigneringFraSluttbrukersystem", "Søknaden og all relevant dokumentasjon må være skrevet/oversatt til norsk, svensk eller dansk", ValidationResultSeverityEnum.ERROR);


            //arbeidstilsynetsSaksnummer
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/arbeidstilsynetsSaksnummer", "Informasjon om 'ArbeidstilsynetsSaksnummer' bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/arbeidstilsynetsSaksnummer/saksaar", "Arbeidstilsynets saksår bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/arbeidstilsynetsSaksnummer/sakssekvensnummer", "Arbeidstilsynets sakssekvensnummer bør fylles ut.", ValidationResultSeverityEnum.WARNING);

            //KommunensSaksnummer
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/kommunensSaksnummer", "Informasjon om 'KommunensSaksnummer' bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/kommunensSaksnummer/saksaar", "Kommunens saksår bør fylles ut.", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/kommunensSaksnummer/sakssekvensnummer", "Kommunens sakssekvensnummer bør fylles ut.", ValidationResultSeverityEnum.WARNING);

            //TODO "ArbeidstilsynetsSamtykke" to "ArbeidstilsynetsSamtykkeV2"/"ArbeidstilsynetsSamtykkeDfv45957"??  rule may need to have dfv in the first "node" in order to connect the text to the correct version and correct schema.
            
            //**ANSAKO
            //Foretak
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak", "Du må fylle ut informasjon om det ansvarlige foretaket.", ValidationResultSeverityEnum.ERROR);
            //foretak partstype
            //Kan ikke validere hvis det er bare 'Foretal' bare det?
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/partstype", "‘partstype’ til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/partstype/kodeverdi", "Kodeverdien for ‘partstype’ til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/partstype/kodeverdi", "'{0}' er en ugyldig kodeverdi for partstypen til foretaket. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/byggesoknad/partstype ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/partstype/kodeverdi", "Ansvarlig foretak sin partstype må være 'Foretak'.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/partstype/kodeverdi", "Kodeverdien for ‘partstype’ kan ikke valideres", ValidationResultSeverityEnum.WARNING);
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/organisasjonsnummer", "Organisasjonsnummeret til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/organisasjonsnummer", "Organisasjonsnummeret ('{0}') til foretaket er ikke gyldig.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/organisasjonsnummer", "Organisasjonsnummeret ('{0}') til foretaket har ikke gyldig kontrollsiffer.", ValidationResultSeverityEnum.ERROR);

            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/navn", "Navnet til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.telmob_utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/mobilnummer", "Telefonnummeret eller mobilnummeret til foretaket bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/mobilnummer", "Mobilnummeret til foretaket må kun inneholde tall og '+'.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/telefonnummer", "Telefonnummeret til foretaket må kun inneholde tall og '+'.");
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/epost", "E-postadressen til foretaket bør fylles ut.");

            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse", "Adressen til foretaket bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse/adresselinje1", "Adresselinje 1 bør fylles ut for foretaket.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse/landkode", "Ugyldig landkode til foretaket");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse/postnr", "Postnummeret til foretaket bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.kontrollsiffer, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse/postnr", "Postnummeret '{0}' til foretaket har ikke gyldig kontrollsiffer. Du kan sjekke riktig postnummer på http://adressesok.bring.no/", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse/postnr", "Postnummeret '{0}' til foretaket er ikke gyldig. Du kan sjekke riktig postnummer på http://adressesok.bring.no/", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.postnr_stemmerIkke, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse/poststed", "Postnummeret '{0}' til foretaket stemmer ikke overens med poststedet '{1}'. Postnummeret er fra '{2}'. Du kan sjekke riktig postnummer/poststed på http://adressesok.bring.no/");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/adresse/postnr", "Postnummeret '{0}' til foretaket ble ikke validert. Postnummeret kan være riktig, men en teknisk feil gjør at vi ikke kan bekrefte det.");
            
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/kontaktperson", "Kontaktpersonen til foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/kontaktperson/navn", "Navnet til kontaktpersonen for foretaket må fylles ut.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.telmob_utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/kontaktperson/mobilnummer", "Telefonnummeret eller mobilnummeret til foretakets kontaktperson bør fylles ut.");
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/kontaktperson/mobilnummer", "Mobilnummer til foretakets kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/kontaktperson/telefonnummeret ", "Telefonnummeret  til foretakets kontaktperson må kun inneholde tall og '+'.", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/foretak/kontaktperson/epost ", "E-postadressen til foretakets kontaktperson bør fylles ut.");
            //ansvarsområde
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader", "Du må definere minst ett ansvarsområde.",ValidationResultSeverityEnum.ERROR);
            //KodeverdiValidatorV2
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon", "Du må oppgi en funksjon for ansvarsområdet. Du kan sjekke gyldige funksjoner på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "Kodeverdien for ‘funksjon’ for ansvarsområdet må fylles ut. Du kan sjekke gyldige funksjoner på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "Kodeverdien for ‘funksjon’ kan ikke valideres", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "'{0}' er en ugyldig kodeverdi for funksjon. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodebeskrivelse", "Når funksjon er valgt, må kodebeskrivelse fylles ut.Du kan sjekke riktig godebeskrivelse på https://register.geonorge.no/kodelister/byggesoknad/funksjon", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodebeskrivelse", "Kodebeskrivelsen '{0}' stemmer ikke med den valgte kodeverdien for funksjon. Du kan sjekke riktig kodebeskrivelse på https://register.geonorge.no/byggesoknad/funksjon");

            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/beskrivelseAvAnsvarsomraade", "Du må fylle ut en beskrivelse av ansvarsområdet. Ansvarlig foretak kan endre beskrivelsen senere.");
            //KodeverdiValidatorV2
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon", "Du må oppgi en tiltaksklasse for ansvarsområdet. Du kan sjekke gyldige funksjoner på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "Kodeverdien for ‘tiltaksklasse’ for ansvarsområdet må fylles ut. Du kan sjekke gyldige kodeverdi på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.validert, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "Kodeverdien for ‘tiltaksklasse’ kan ikke valideres", ValidationResultSeverityEnum.WARNING);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodeverdi", "'{0}' er en ugyldig kodeverdi for tiltaksklasse. Du kan sjekke riktig kodeverdi på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.utfylt, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodebeskrivelse", "Når tiltaksklasse er valgt, må kodebeskrivelse fylles ut. Du kan sjekke riktig godebeskrivelse på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse ", ValidationResultSeverityEnum.ERROR);
            AddRuleToValidationMessageStorageEntry(null, ValidationRuleEnum.gyldig, "/ErklaeringAnsvarsrett/ansvarsrett/ansvarsomraader/ansvarsomraade{0}/funksjon/kodebeskrivelse", "Kodebeskrivelsen '{0}' stemmer ikke med den valgte kodeverdien for tiltaksklasse. Du kan sjekke riktig kodebeskrivelse på https://register.geonorge.no/kodelister/byggesoknad/tiltaksklasse");

            return _validationMessageStorageEntry;
        }

    }
}


