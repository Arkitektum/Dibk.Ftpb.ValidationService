using Arkitektum.XmlSchemaValidator.Validator;
using Dibk.Ftpb.Validation.Application.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Services
{
    public class XsdValidationService : IXsdValidationService
    {
        private readonly IXmlSchemaValidator _xmlSchemaValidator;

        public XsdValidationService(
            IXmlSchemaValidator xmlSchemaValidator)
        {
            _xmlSchemaValidator = xmlSchemaValidator;
        }

        public List<string> Validate(InputData inputData)
        {
            inputData.Data.Seek(0, SeekOrigin.Begin);
            var messages = _xmlSchemaValidator.Validate(inputData.Config.DataType.ToString(), inputData.Data);

            inputData.IsValid = !messages.Any();
            
            return messages;
        }
    }
}
