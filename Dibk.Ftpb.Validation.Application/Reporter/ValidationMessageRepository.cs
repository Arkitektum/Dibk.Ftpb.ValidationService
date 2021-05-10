using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.Reporter.DataBase;

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
        public bool GetValidationRuleMessage(ValidationRule validationRule, string languageCode, out string validationRuleMessage)
        {
            var theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.Id.Equals(validationRule.Id) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationRule.Xpath));

            if (theStorageEntry == null)
            {
                validationRuleMessage = $"Could not find validation message with reference: '{validationRule.Id}', xpath: '{validationRule.Xpath}' and languageCode:'{languageCode}'.-";
                return false;
            }

            validationRuleMessage = theStorageEntry.Message;
            
            return true;
        }

        public bool GetValidationMessageStorageEntry(ValidationMessage validationMessage, string languageCode, out string composedValidationMessage)
        {
            var index = validationMessage.Xpath.IndexOf("[");
            string xPath = validationMessage.Xpath;
            if (index > 0)
            {
                xPath = validationMessage.Xpath.Substring(0, index) + "{0}" + validationMessage.Xpath.Substring(index + 3);
            }

            var theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.Id.Equals(validationMessage.Reference) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(xPath));

            if (theStorageEntry == null)
            {
                composedValidationMessage = $"Could not find validation message with reference: '{validationMessage.Reference}', xpath: '{validationMessage.Xpath}' and languageCode:'{languageCode}'.-";
                return false;
            }

            if (validationMessage.MessageParameters != null)
            {
                try
                {
                    composedValidationMessage = String.Format(theStorageEntry.Message, validationMessage.MessageParameters.ToArray());

                    return true;
                }
                catch (FormatException)
                {
                    composedValidationMessage = $"{theStorageEntry.Message} . **'Illegal number of validation parameters'";
                    return false;
                }
            }
            composedValidationMessage = theStorageEntry.Message;

            return true;
        }

        private void InitiateMessageRepository()
        {
            _validationMessageStorageEntry = new ArbeidstilsyneDB().InitiateMessageRepository();
        }
    }
}
