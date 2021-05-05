using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators;
using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces;
using Dibk.Ftpb.Validation.Application.Process;
using Dibk.Ftpb.Validation.Application.Reporter;
using Moq;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests
{
    public class ValidationOrchestratorTest
    {
        [Fact]
        public void testArbeidstilsynetOrchestrator()
        {

            //TODO found out if we can test orchestrator starting with test motor
            //var ServiceProvider = new Mock<IServiceProvider>();
            //await new ValidationOrchestrator(ServiceProvider.Object).ExecuteAsync("", "45957");

        }
    }
}
