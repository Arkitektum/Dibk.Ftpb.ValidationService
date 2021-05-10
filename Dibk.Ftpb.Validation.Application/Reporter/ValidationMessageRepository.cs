using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class ValidationMessageRepository
    {
        private List<ValidationMessageStorageEntry> _validationMessageStorageEntry;

        public ValidationMessageRepository()
        {
            _validationMessageStorageEntry = new List<ValidationMessageStorageEntry>();
            InitiateMessageRepository();
        }
        public bool GetValidationMessageStorageEntry(ValidationMessage validationMessage, string languageCode, out string result)
        {
            var theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.Id.Equals(validationMessage.Reference) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationMessage.Xpath));

            if (theStorageEntry == null)
            {
                result = $"Could not find validation message with reference: '{validationMessage.Reference}', xpath: '{validationMessage.Xpath}' and languageCode:'{languageCode}'.-";
                return false;
            }

            if (validationMessage.MessageParameters != null)
            {
                try
                {
                    result = String.Format(theStorageEntry.Message, validationMessage.MessageParameters.ToArray());
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

        public string GetValidationMessageStorageEntry(ValidationMessage validationMessage, string languageCode)
        {
            var theStorageEntry = _validationMessageStorageEntry.Where(x => x.Id.Equals(validationMessage.Reference) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationMessage.Xpath)).FirstOrDefault();
            int numberOfParametersInMessageText = theStorageEntry.Message.Where(x => (x == '{')).Count();
            int numberOfParametersInValidation = (validationMessage.MessageParameters == null ? 0 : validationMessage.MessageParameters.Count());

            if (numberOfParametersInMessageText != numberOfParametersInValidation)
            {
                throw new ArgumentOutOfRangeException("Illegal number og validation parameters");
            }

            var newText = theStorageEntry.Message;
            if (numberOfParametersInValidation > 0)
            {
                foreach (var parameter in validationMessage.MessageParameters)
                {
                    var regex = new Regex(Regex.Escape("{}"));
                    newText = regex.Replace(newText, "'" + parameter + "'", 1);
                }
            }

            return newText;
        }
        private void InitiateMessageRepository()
        {
            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bygningsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/bygningsnummer",
                Message = "Bygningsnr må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bolignummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/bolignummer",
                Message = "Bolignummer må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "kommunenavn_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/kommunenavn",
                Message = "Kommunenavn må være utfyllt"
            });

            //_validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            //{
            //    Id = "kommunenavn_utfylt",
            //    LanguageCode = "NO",
            //    XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/Kommunenavn",
            //    Message = "Kommunenavn må være utfyllt"
            //});

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "tillatte_postnr_i_kommune",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested",
                Message = "Postnr {} ligger ikke i {} kommune"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje1_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/adresselinje1",
                Message = "Eiendommens adresselinje1 må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje2_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/adresselinje2",
                Message = "Eiendommens adresselinje2 må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_adresselinje3_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/adresselinje3",
                Message = "Eiendommens adresselinje3 må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_landkode_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/landkode",
                Message = "Eiendommens landkode må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_postnr_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/postnr",
                Message = "Eiendommens postnr må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_poststed_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/poststed",
                Message = "Eiendommens poststed må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_gatenavn_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/gatenavn",
                Message = "Eiendommens gatenavn må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_husnr_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/husnr",
                Message = "Eiendommens husnr må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_bokstav_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/bokstav",
                Message = "Eiendommens bokstav må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "eiendomsAdresse_postnr_4siffer",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/adresse/postnr",
                Message = "Eiendommens postnr må bestå av 4 siffer"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "kommunenummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/eiendomsidentifikasjon/kommunenummer",
                Message = "Eiendommens kommunenr i Matrikkelen må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "gaardsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/eiendomsidentifikasjon/gaardsnummer",
                Message = "Eiendommens GNR i Matrikkelen må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "bruksnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/eiendomsidentifikasjon/bruksnummer",
                Message = "Eiendommens BNR i Matrikkelen må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "festenummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/eiendomsidentifikasjon/festenummer",
                Message = "Eiendommens FNR i Matrikkelen må være utfyllt"
            });

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "seksjonsnummer_utfylt",
                LanguageCode = "NO",
                XPath = "ArbeidstilsynetsSamtykke/eiendomByggested/eiendomsidentifikasjon/seksjonsnummer",
                Message = "Eiendommens SNR i Matrikkelen må være utfyllt"
            });;

            _validationMessageStorageEntry.Add(new ValidationMessageStorageEntry()
            {
                Id = "Parameter_Test",
                LanguageCode = "NO",
                XPath = "Unit/Test/Parameter",
                Message = "Parameter 1 kommer har '{0}' og parameter 2 kommer har ({1})"
            });
        }
    }
}
