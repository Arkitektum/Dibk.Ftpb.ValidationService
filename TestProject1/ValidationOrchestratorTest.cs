using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Process;
using Moq;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ValidationOrchestratorTest
    {
        [Fact]
        public async Task test()
        {

            //TODO found out if we can test orchestrator starting with test motor
            var ServiceProvider = new Mock<IServiceProvider>();

            await new ValidationOrchestrator(ServiceProvider.Object).ExecuteAsync("", "45957");



        }
    }
}
