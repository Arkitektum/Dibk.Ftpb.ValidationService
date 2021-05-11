using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Reporter;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.ReporterTests
{
    public class ValidationMessageRepositoryTests
    {
        [Fact]
        public void TestGetValidationMessageStorageEntry()
        {
            ValidationMessage validationMessage = new ValidationMessage()
            {
                Reference = "Parameter_Test",
                ChecklistReference = "1.1",
                MessageParameters = new List<string>() { "jeg er først"},
                //PreCondition = "..?..",
                //ValidationResult = ValidationResultEnum.Unused,
                Xpath = "Unit/Test/Parameter",
            };
            //string result;
            var noko = new ValidationMessageRepository().GetComposedValidationMessage(validationMessage, "NO");

            //if (noko)
            //{
            //    //
            //}
            //else
            //{
            //    //  
            //}

            //noko.Should().NotBeEmpty();
        }
    }
}
