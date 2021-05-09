using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageRepository
    {
        private List<ValidationMessageStorageEntry> _storageEntry;

        public ValidationMessageRepository()
        {
            _storageEntry = new List<ValidationMessageStorageEntry>();
            InitiateMessageRepository();
        }
        public bool GetValidationMessageStorageEntry(ValidationRule validationRule, string languageCode, out string result)
        {
            var theStorageEntry = _storageEntry.FirstOrDefault(x => x.Id.Equals(validationRule.Id) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationRule.Xpath));

            if (theStorageEntry == null)
            {
                result = $"could not find rule with Id: '{validationRule.Id}', xpath: '{validationRule.Xpath}' and languageCode:'{languageCode}'.-";
                return false;
            }

            if (validationRule.MessageParameters != null)
            {
                try
                {
                    result = String.Format(theStorageEntry.Message, validationRule.MessageParameters.ToArray());
                    return true;
                }
                catch (FormatException)
                {
                    result = $"{theStorageEntry.Message} . **'Illegal number og validation parameters'";
                    return false;
                    throw new ArgumentOutOfRangeException("Illegal number og validation parameters");
                }
            }
            result = theStorageEntry.Message;
            return true;
        }

        public string GetValidationMessageStorageEntry(ValidationRule validationRule, string languageCode)
        {
            var theStorageEntry = _storageEntry.Where(x => x.Id.Equals(validationRule.Id) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationRule.Xpath)).FirstOrDefault();
            int numberOfParametersInMessageText = theStorageEntry.Message.Where(x => (x == '{')).Count();
            int numberOfParametersInValidation = (validationRule.MessageParameters == null ? 0 : validationRule.MessageParameters.Count());

            if (numberOfParametersInMessageText != numberOfParametersInValidation)
            {
                throw new ArgumentOutOfRangeException("Illegal number og validation parameters");
            }

            var newText = theStorageEntry.Message;
            if (numberOfParametersInValidation > 0)
            {
                foreach (var parameter in validationRule.MessageParameters)
                {
                    var regex = new Regex(Regex.Escape("{}"));
                    newText = regex.Replace(newText, "'" + parameter + "'", 1);
                }
            }

            return newText;
        }
        private void InitiateMessageRepository()
        {
            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bygningsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Bygningsnummer",
                Message = "Bygningsnr må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bolignummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Bolignummer",
                Message = "Bolignummer må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "kommunenavn_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Kommunenavn",
                Message = "Kommunenavn må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "kommunenavn_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Kommunenavn",
                Message = "Kommunenavn må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "tillatte_postnr_i_kommune",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom",
                Message = "Postnr {} ligger ikke i {} kommune"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje1_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Adresselinje1",
                Message = "Eiendommens adresselinje1 må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje2_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Adresselinje2",
                Message = "Eiendommens adresselinje2 må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje3_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Adresselinje3",
                Message = "Eiendommens adresselinje3 må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_landkode_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Landkode",
                Message = "Eiendommens landkode må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_postnr_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Postnr",
                Message = "Eiendommens postnr må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_poststed_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Poststed",
                Message = "Eiendommens poststed må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_gatenavn_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Gatenavn",
                Message = "Eiendommens gatenavn må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_husnr_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Husnr",
                Message = "Eiendommens husnr må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_bokstav_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Bokstav",
                Message = "Eiendommens bokstav må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_postnr_4siffer",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/EiendomsAdresse/Postnr",
                Message = "Eiendommens postnr må bestå av 4 siffer"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "kommunenummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Matrikkel/Kommunenummer",
                Message = "Eiendommens kommunenr i Matrikkelen må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "gaardsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Matrikkel/Gaardsnummer",
                Message = "Eiendommens GNR i Matrikkelen må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bruksnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Matrikkel/Bruksnummer",
                Message = "Eiendommens BNR i Matrikkelen må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "festenummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Matrikkel/Festenummer",
                Message = "Eiendommens FNR i Matrikkelen må være utfyllt"
            });

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "seksjonsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/Eiendom/Matrikkel/Seksjonsnummer",
                Message = "Eiendommens SNR i Matrikkelen må være utfyllt"
            });;

            _storageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "Parameter_Test",
                LanguageCode = "NO",
                XPath = "Unit/Test/Parameter",
                Message = "Parameter 1 kommer har '{0}' og parameter 2 kommer har ({1})"
            });
        }
    }
}
