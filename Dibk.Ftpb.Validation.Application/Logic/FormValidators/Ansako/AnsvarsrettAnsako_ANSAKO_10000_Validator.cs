using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
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

        //*Ansvarsrett
        private AnsvarsrettValidator _AnsvarsrettValidator;
        //**foretak
        private IForetakValidator _foretakValidator;
        private IKontaktpersonValidator _foretakKontaktpersonValidator;
        private IKodelisteValidator _foretakPartstypeValidator;
        private IEnkelAdresseValidator _foretakEnkelAdresseValidator;
        //Ansavarområde        
        private IAnsvarsomraadeValidator _ansvarsomraadeValidator;
        private IKodelisteValidator _funksjonValidator;
        private IKodelisteValidator _tiltaksklasseValidator;


        public AnsvarsrettAnsako_ANSAKO_10000_Validator(IValidationMessageComposer validationMessageComposer, IChecklistService checklistService,
                                                        ICodeListService codeListService, IPostalCodeService postalCodeService)
            : base(validationMessageComposer, checklistService)
        {
            _validationMessageComposer = validationMessageComposer;
            _checklistService = checklistService;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;

            _entitiesNodeList = new List<EntityValidatorNode>();
            _tiltakstypes = new string[] { };
        }

        public override ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            _validationForm = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(validationInput.FormData);
            base.StartValidation(dataFormatVersion, validationInput);

            return _validationResult;
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
            _entitiesNodeList.AddRange(soeknadssystemetsReferanseNodeList);

        }

        protected override IEnumerable<string> GetFormTiltakstyper()
        {
            return _tiltakstypes;
        }

        protected override void InstantiateValidators()
        {
            var tree = EntityValidatiorTree.BuildTree(_entitiesNodeList);

            //*AnsvarligSoeker
            _ansvarligSoekerKontaktpersonValidator = new KontaktpersonValidator(tree, 02);
            _ansvarligSoekerPartstypeValidator = new PartstypeValidator(tree, 03, _codeListService);
            _ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(tree, 04, _postalCodeService);
            _ansvarligSoekerValidator = new AnsvarligSoekerValidator(tree, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);

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

        protected override void Validate(ValidationInput validationInput)
        {
            ValidateEntityFields();

            var ansvarsrettResult = _AnsvarsrettValidator.Validate(_validationForm.Ansvarsretts);
            AccumulateValidationMessages(ansvarsrettResult.ValidationMessages);

            var ansvarligSoekerValidationResult = _ansvarligSoekerValidator.Validate(_validationForm.AnsvarligSoeker);
            AccumulateValidationMessages(ansvarligSoekerValidationResult.ValidationMessages);
        }

        private void ValidateEntityFields()
        {
            if (string.IsNullOrEmpty(_validationForm.FraSluttbrukersystem))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.fraSluttbrukersystem);
        }


        protected override void DefineValidationRules()
        {
            // Local
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.fraSluttbrukersystem);

            //AnsvarligSoeker
            AccumulateValidationRules(_ansvarligSoekerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerPartstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerKontaktpersonValidator.ValidationResult.ValidationRules);
        }
    }
}
