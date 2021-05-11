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
        public ValidationRule GetValidationRuleMessage(ValidationRule validationRule, string languageCode)
        {
            var theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.Id.Equals(validationRule.Id) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationRule.Xpath));

            if (theStorageEntry == null)
            {
                validationRule.Message = $"Could not find validation message with reference: '{validationRule.Id}', xpath: '{validationRule.Xpath}' and languageCode:'{languageCode}'.-";
            }
            else
            {
                validationRule.Message = theStorageEntry.Message;
                validationRule.ChecklistReference = theStorageEntry.ChecklistReference;
            }

            return validationRule;
        }

        public ValidationMessage GetComposedValidationMessage(ValidationMessage validationMessage, string languageCode)
        {

            string xPath = Regex.Replace(validationMessage.Xpath, @"\[([0-9]*)\]", "{0}"); ;

            var theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.Id.Equals(validationMessage.Reference) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(xPath));

            if (theStorageEntry == null)
            {
                validationMessage.Message = $"Could not find validation message with reference: '{validationMessage.Reference}', xpath: '{validationMessage.Xpath}' and languageCode:'{languageCode}'.-";
            }
            else
            {
                if (validationMessage.MessageParameters != null)
                {
                    try
                    {
                        validationMessage.Message = String.Format(theStorageEntry.Message, validationMessage.MessageParameters.ToArray());
                    }
                    catch (FormatException)
                    {
                        validationMessage.Message = $"{theStorageEntry.Message} . **'Illegal number of validation parameters'";
                    }
                }
                else
                {
                    validationMessage.Message = theStorageEntry.Message;
                }
                validationMessage.ChecklistReference = theStorageEntry.ChecklistReference;
            }
            return validationMessage;
        }

        private void InitiateMessageRepository()
        {
            _validationMessageStorageEntry = new ArbeidstilsyneDB().InitiateMessageRepository();
        }
    }
}
