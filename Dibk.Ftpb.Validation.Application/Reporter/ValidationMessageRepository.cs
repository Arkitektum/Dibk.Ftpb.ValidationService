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
        public bool GetValidationMessageStorageEntry(ValidationMessage validationMessage, string languageCode, out string result)
        {
           var index = validationMessage.Xpath.IndexOf("[");
          string xPath = validationMessage.Xpath;
           if (index>0)
           {
               xPath = validationMessage.Xpath.Substring(0, index)+"{0}"+validationMessage.Xpath.Substring(index+3);
           }
            
            var theStorageEntry = _validationMessageStorageEntry.FirstOrDefault(x => x.Id.Equals(validationMessage.Reference) && x.LanguageCode.Equals(languageCode) && x.XPath.Equals(xPath));

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
            _validationMessageStorageEntry = new ArbeidstilsyneDB().InitiateMessageRepository();
        }
    }
}
