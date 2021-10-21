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
        public ValidationRule GetValidationRuleMessage(ValidationRule validationRule, string languageCode, string dataFormatVersion)
        {
            ValidationMessageStorageEntry theStorageEntry;
            theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => dataFormatVersion.Equals(x.DataFormatVersion) && x.Rule.Equals(validationRule.Rule) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationRule.XpathField));
            if (theStorageEntry == null)
            {
                theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.DataFormatVersion == null && x.Rule.Equals(validationRule.Rule) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(validationRule.XpathField));
            }

            if (theStorageEntry == null)
            {
                validationRule.Message = $"Could not find validation message with reference: '{validationRule.Rule}', xpath: '{validationRule.XpathField}' and languageCode:'{languageCode}'.-";
            }
            else
            {
                validationRule.Message = theStorageEntry.Message;
                validationRule.Messagetype = theStorageEntry.Messagetype;
                //validationRule.ChecklistReference = theStorageEntry.ChecklistReference;
            }

            return validationRule;
        }

        public ValidationMessage GetComposedValidationMessage(string dataFormatVersion, ValidationMessage validationMessage, string languageCode)
        {

            if (string.IsNullOrEmpty(validationMessage.XpathField))
            {
                validationMessage.Message = $"Could not find validation message xPath for rule: '{validationMessage.Rule}', and rule Reference id:'{validationMessage.Reference}'.-";
            }
            else
            {
                string xPath = Regex.Replace(validationMessage.XpathField, @"\[([0-9]*)\]", "{0}"); ;

                ValidationMessageStorageEntry theStorageEntry;
                theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => dataFormatVersion.Equals(x.DataFormatVersion) && x.Rule == validationMessage.Rule && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(xPath, StringComparison.OrdinalIgnoreCase));

                if (theStorageEntry == null)
                {
                    theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.DataFormatVersion == null && x.Rule == validationMessage.Rule && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(xPath, StringComparison.OrdinalIgnoreCase));
                }

                if (theStorageEntry == null)
                {
                    validationMessage.Message = $"Could not find validation message with reference: '{validationMessage.Rule}', xpath: '{validationMessage.XpathField}' and languageCode:'{languageCode}'.-";
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
                    validationMessage.Messagetype = theStorageEntry.Messagetype;
                }
            }
            return validationMessage;
        }

        private void InitiateMessageRepository()
        {
            _validationMessageStorageEntry = new ArbeidstilsyneDB().InitiateMessageRepository();
        }
    }
}
