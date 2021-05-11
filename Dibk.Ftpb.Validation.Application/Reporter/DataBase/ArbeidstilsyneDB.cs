using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Reporter.DataBase
{
    public class ArbeidstilsyneDB
    {
        public List<ValidationMessageStorageEntry> InitiateMessageRepository()
        {
            List<ValidationMessageStorageEntry> _validationMessageStorageEntry =
                new List<ValidationMessageStorageEntry>();
            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bygningsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bygningsnummer",
                Message = "Bygningsnr må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bolignummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/bolignummer",
                Message = "Bolignummer må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "kommunenavn_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/kommunenavn",
                Message = "Kommunenavn må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                DataForm = "ArbeidstilsynetsSamtykkeV2",
                DataFormatVersion = "45957",
                ChecklistReference = "2.31",
                Id = "tillatte_postnr_i_kommune",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/postnr",
                Message = "Postnr {0} ligger ikke i {1} kommune"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje1_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje1",
                Message = "Eiendommens adresselinje1 må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje2_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje2",
                Message = "Eiendommens adresselinje2 må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje3_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/adresselinje3",
                Message = "Eiendommens adresselinje3 må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_landkode_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/landkode",
                Message = "Eiendommens landkode må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_postnr_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr",
                Message = "Eiendommens postnr må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_poststed_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/poststed",
                Message = "Eiendommens poststed må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_gatenavn_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/gatenavn",
                Message = "Eiendommens gatenavn må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_husnr_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/husnr",
                Message = "Eiendommens husnr må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_bokstav_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/bokstav",
                Message = "Eiendommens bokstav må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_postnr_4siffer",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/adresse/postnr",
                Message = "Eiendommens postnr må bestå av 4 siffer"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "kommunenummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}/eiendomsidentifikasjon/kommunenummer",
                Message = "Eiendommens kommunenr i Matrikkelen må være utfyllt"
            });

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

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "Parameter_Test",
                LanguageCode = "NO",
                XPath = "Unit{0}/Test/Parameter",
                Message = "Parameter 1 kommer har '{0}' og parameter 2 kommer har ({1})"
            });

            return _validationMessageStorageEntry;
        }

    }
}
