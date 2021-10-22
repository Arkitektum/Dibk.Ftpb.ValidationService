using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using Dibk.Ftpb.Validation.Application.Tests.Utils;
using Dibk.Ftpb.Validation.Application.Utils;
using FluentAssertions;
using Xunit;

namespace Dibk.Ftpb.Validation.Application.Tests.EntityValidatorTests
{
    public class AnsvarsrettValidatorTests
    {
        private ICodeListService _codeListService;
        private IPostalCodeService _postalCodeService;

        private AnsvarsrettValidator _AnsvarsrettValidator;
        //**foretak
        private IAktoerValidator _foretakValidator;
        private IKontaktpersonValidator _foretakKontaktpersonValidator;
        private IKodelisteValidator _foretakPartstypeValidator;
        private IEnkelAdresseValidator _foretakEnkelAdresseValidator;
        //Ansavarområde        
        private IAnsvarsomraadeValidator _ansvarsomraadeValidator;
        private IKodelisteValidator _funksjonValidator;
        private IKodelisteValidator _tiltaksklasseValidator;
        private AnsvarsrettAnsako_ANSAKO_10000_Form _form;

        public AnsvarsrettValidatorTests()
        {
            var xmlData = File.ReadAllText(@"Data\Ansako\ErklaeringAnsvarsrett_1.xml");
            _form = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(xmlData);

            _codeListService = MockDataSource.IsCodeListValid(FtbKodeListeEnum.Partstype, true);
            _postalCodeService = MockDataSource.ValidatePostnr(true, "Bø i Telemark", "true");

            var soeknadssystemetsReferanseNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 05, EnumId = EntityValidatorEnum.AnsvarsrettValidator, ParentID = null},
                //foretak
                new () {NodeId = 06, EnumId = EntityValidatorEnum.ForetakValidator, ParentID = 05},
                new () {NodeId = 07, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 06},
                new () {NodeId = 08, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 06},
                new () {NodeId = 09, EnumId = EntityValidatorEnum.KontaktpersonValidatorV2, ParentID = 06},
                //ansvarsområde
                new () {NodeId = 10, EnumId = EntityValidatorEnum.AnsvarsomraadeValidator, ParentID = 05},
                new () {NodeId = 11, EnumId = EntityValidatorEnum.FunksjonValidator, ParentID = 10},
                new () {NodeId = 12, EnumId = EntityValidatorEnum.TiltaksklasseValidator, ParentID = 10},
            };
            var tree = EntityValidatiorTree.BuildTree(soeknadssystemetsReferanseNodeList);

            //*Ansvarsrett
            //**Ansvarsområde
            _funksjonValidator = new FunksjonValidator(tree, 11, _codeListService);
            _tiltaksklasseValidator = new tiltaksklasseValidator(tree, 12, _codeListService);
            _ansvarsomraadeValidator = new AnsvarsomraadeValidator(tree, _funksjonValidator, _tiltaksklasseValidator);
            //**foretak
            _foretakPartstypeValidator = new PartstypeValidator(tree, 07, _codeListService);
            _foretakEnkelAdresseValidator = new EnkelAdresseValidator(tree, 08, _postalCodeService);
            _foretakKontaktpersonValidator = new KontaktpersonValidatorV2(tree, 09);
            _foretakValidator = new ForetakValidator(tree, _foretakEnkelAdresseValidator, _foretakKontaktpersonValidator, _foretakPartstypeValidator, _codeListService);

            _AnsvarsrettValidator = new AnsvarsrettValidator(tree, _foretakValidator, _ansvarsomraadeValidator);
        }
        [Fact]
        public void ForetakTest()
        {
            var result = _AnsvarsrettValidator.Validate(_form.Ansvarsretts);
            var errorMessages = result?.ValidationMessages;

            errorMessages.Should().BeNullOrEmpty();
        }
    }
}
