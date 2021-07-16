using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests.EntityValidatorLogic
{
    public class SjekklistekravValidatorLogicTests
    {
        private ArbeidstilsynetsSamtykkeType _form;

        private readonly IPostalCodeService _postalCodeService;

        private readonly SjekklistekravValidatorLogic _sjekklistekravValidatorLogic;
        private ISjekklistekravValidator _sjekklistekravValidator;
        
        

        public SjekklistekravValidatorLogicTests()
        {
            _sjekklistekravValidatorLogic = new SjekklistekravValidatorLogic(1);
            _sjekklistekravValidator = _sjekklistekravValidatorLogic.Validator;
        }

        [Fact]
        public void TestTree()
        {
            var validatorTree = _sjekklistekravValidatorLogic.Tree;
            validatorTree.Count.Should().Be(1);
            validatorTree?.FirstOrDefault()?.Children.Count.Should().Be(0);
        }
        [Fact]
        public void TestValidationRules()
        {
            var validatorNodes = _sjekklistekravValidatorLogic.ValidationRules;
            var rulesCount = validatorNodes.Count;

            rulesCount.Should().Be(13);

        }
        [Fact]
        public void TestValidator()
        {
            
            //var result = _sjekklistekravValidator.Validate();
            //result.Should().NotBeNull();

        }

    }
}
