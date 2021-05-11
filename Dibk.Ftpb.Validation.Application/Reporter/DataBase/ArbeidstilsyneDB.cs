using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter.DataBase
{
    public class ArbeidstilsyneDB
    {
        private List<ValidationMessageStorageEntry> _validationMessageStorageEntry;
        public ArbeidstilsyneDB()
        {
            _validationMessageStorageEntry = new List<ValidationMessageStorageEntry>();
        }

        public void AddRuleTOValidationMessageStorageEntry(string id, string xPath, string message, string languageCode = null, string checklistReference = null, string dataForm = null, string dataFormatVersion = null)
        {
            var validationMessageStorageEntry = new ValidationMessageStorageEntry()
            {
                Id = id,
                XPath = xPath,
                Message = message,
                ChecklistReference = checklistReference,
            };
            validationMessageStorageEntry.LanguageCode = string.IsNullOrEmpty(languageCode) ? "NO" : languageCode;
            validationMessageStorageEntry.DataForm = string.IsNullOrEmpty(dataForm) ? "ArbeidstilsynetsSamtykkeV2" : dataForm;
            validationMessageStorageEntry.DataFormatVersion = string.IsNullOrEmpty(dataFormatVersion) ? "45957" : dataFormatVersion;
           _validationMessageStorageEntry.Add(validationMessageStorageEntry);
           
        }
        public List<ValidationMessageStorageEntry> InitiateMessageRepository()
        {

            AddRuleTOValidationMessageStorageEntry("bygningsnummer_utfylt","ArbeidstilsynetsSamtykke/eiendomByggested{0}/bygningsnummer", "Bygningsnr må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("bolignummer_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bolignummer", "Bolignummer må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("kommunenavn_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/kommunenavn", "Kommunenavn må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("tillatte_postnr_i_kommune", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/postnr", "Postnr {0} ligger ikke i {1} kommune",null,"2.3");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_adresselinje1_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje1", "Eiendommens adresselinje1 må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_adresselinje2_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje2", "Eiendommens adresselinje2 må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_adresselinje3_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje3", "Eiendommens adresselinje3 må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_landkode_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/landkode", "Eiendommens landkode må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_postnr_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_poststed_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/poststed", "Eiendommens poststed må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_gatenavn_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/gatenavn", "Eiendommens gatenavn må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_husnr_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/husnr", "Eiendommens husnr må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_bokstav_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/bokstav", "Eiendommens bokstav må være utfyllt");
            AddRuleTOValidationMessageStorageEntry("eiendomsAdresse_postnr_4siffer", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr", "Eiendommens postnr må bestå av 4 siffer");
            AddRuleTOValidationMessageStorageEntry("kommunenummer_utfylt", "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer", "Eiendommens kommunenr i Matrikkelen må være utfyllt");

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "gaardsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/gaardsnummer",
                Message = "Eiendommens GNR i Matrikkelen må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bruksnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/bruksnummer",
                Message = "Eiendommens BNR i Matrikkelen må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "festenummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/festenummer",
                Message = "Eiendommens FNR i Matrikkelen må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "seksjonsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/seksjonsnummer",
                Message = "Eiendommens SNR i Matrikkelen må være utfyllt"
            }); ;
            //Test
            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "Parameter_Test",
                LanguageCode = "NO",
                XPath = "Unit{0}/Test/Parameter",
                Message = "Parameter 1 kommer har '{0}' og parameter 2 kommer har ({1})"
            });



            //arbeidsplasser
            AddRuleTOValidationMessageStorageEntry("arbeidsplasser_utfylt", "ArbeidstilsynetsSamtykke/arbeidsplasser", "Arbeidsplasser må fylles ut");
            AddRuleTOValidationMessageStorageEntry("framtidige_eller_eksisterende_utfylt", "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'eksisterende' eller 'fremtidige' eller begge deler.");
            AddRuleTOValidationMessageStorageEntry("faste_eller_midlertidige_utfylt", "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det må velges enten 'faste' eller 'midlertidige' eller begge deler");
            AddRuleTOValidationMessageStorageEntry("type_arbeid_utfylt", "ArbeidstilsynetsSamtykke/arbeidsplasser", "Er tiltaket knyttet til utleiebygg så skal antall virksomheter angis.");
            AddRuleTOValidationMessageStorageEntry("utleieBygg", "ArbeidstilsynetsSamtykke/arbeidsplasser", "Det skal angis hvormange ansatte som bygget dimensjoneres for.");
            AddRuleTOValidationMessageStorageEntry("arbeidsplasser_beskrivelse", "ArbeidstilsynetsSamtykke/arbeidsplasser/beskrivelse", "Enten skal arbeidsplasser beskrives i søknaden eller det skal være lagt ved vedlegg 2: 'Beskrivelse av type arbeid / prosesser'.");
            
          
           
          
          

            return _validationMessageStorageEntry;
        }


    }
}
