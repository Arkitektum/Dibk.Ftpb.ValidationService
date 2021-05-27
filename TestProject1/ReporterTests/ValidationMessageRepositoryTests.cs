using Dibk.Ftpb.Validation.Application.Reporter;
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
                Reference = Enums.ValidationRuleEnum.Parameter_Test,
                ChecklistReference = "1.1",
                //MessageParameters = new List<string>() { "jeg er først"},
                //PreCondition = "..?..",
                //ValidationResult = ValidationResultEnum.Unused,
                XpathField = "Unit/Test/Parameter",
            };
            //string result;
            var noko = new ValidationMessageRepository().GetComposedValidationMessage(validationMessage, "NO");
         

            //noko.Should().NotBeNull();
        }
    }
}
