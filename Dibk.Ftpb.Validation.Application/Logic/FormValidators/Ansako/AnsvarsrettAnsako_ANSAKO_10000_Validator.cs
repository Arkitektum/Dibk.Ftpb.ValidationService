using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Services;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators.Ansako
{
    [FormData(DataFormatVersion = "10000")]
    public class AnsvarsrettAnsako_ANSAKO_10000_Validator : FormValidatorBase, IFormValidator
    {
        private AnsvarsrettAnsako_ANSAKO_10000_Form _validationForm;

        private readonly IValidationMessageComposer _validationMessageComposer;
        private readonly IChecklistService _checklistService;
        private readonly ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        private string[] _tiltakstypes;

        //AnsvarligSoeker
        private IAktoerValidator _ansvarligSoekerValidator;
        private IEnkelAdresseValidator _ansvarligSoekerEnkelAdresseValidator;
        private IKodelisteValidator _ansvarligSoekerPartstypeValidator;
        private IKontaktpersonValidator _ansvarligSoekerKontaktpersonValidator;
        private List<EntityValidatorNode> _entitiesNodeList;


        public AnsvarsrettAnsako_ANSAKO_10000_Validator(IValidationMessageComposer validationMessageComposer, IChecklistService checklistService,
                                                        ICodeListService codeListService, IPostalCodeService postalCodeService) 
            : base(validationMessageComposer, checklistService)
        {
            _validationMessageComposer = validationMessageComposer;
            _checklistService = checklistService;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;

            _entitiesNodeList = new List<EntityValidatorNode>();
            _tiltakstypes = new string[]{};
        }

        public override ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            _validationForm = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(validationInput.FormData);
            base.StartValidation(dataFormatVersion, validationInput);

            return ValidationResult;
        }

        protected override string XPathRoot { get; }
        protected override void InitializeValidatorConfig()
        {
            //AnsvarligSoeker
            var ansvarligSoekervalidatorNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 01, EnumId = EntityValidatorEnum.AnsvarligSoekerValidator, ParentID = null},
                new () {NodeId = 02, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = 01},
                new () {NodeId = 03, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 01},
                new () {NodeId = 04, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 01}
            };
            _entitiesNodeList.AddRange(ansvarligSoekervalidatorNodeList);
        }

        protected override IEnumerable<string> GetFormTiltakstyper()
        {
            return _tiltakstypes;
        }

        protected override void InstantiateValidators()
        {
            var tree = EntityValidatiorTree.BuildTree(_entitiesNodeList);
            //AnsvarligSoeker TODO not applied in FTB v1
            _ansvarligSoekerKontaktpersonValidator = new KontaktpersonValidator(tree, 02);
            _ansvarligSoekerPartstypeValidator = new PartstypeValidator(tree, 03, _codeListService);
            _ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(tree, 04, _postalCodeService);
            _ansvarligSoekerValidator = new AnsvarligSoekerValidator(tree, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);

        }

        protected override void Validate(ValidationInput validationInput)
        {
            var ansvarligSoekerValidationResult = _ansvarligSoekerValidator.Validate(_validationForm.AnsvarligSoeker);
            AccumulateValidationMessages(ansvarligSoekerValidationResult.ValidationMessages);
        }

        protected override void DefineValidationRules()
        {
            //AnsvarligSoeker
            AccumulateValidationRules(_ansvarligSoekerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerPartstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerKontaktpersonValidator.ValidationResult.ValidationRules);
        }
    }
}
