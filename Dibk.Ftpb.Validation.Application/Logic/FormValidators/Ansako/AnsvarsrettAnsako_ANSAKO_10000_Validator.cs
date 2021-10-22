using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Dibk.Ftpb.Validation.Application.DataSources;
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
    [FormData(DataFormatId = "10000", DataFormatVersion = "1")]
    public class AnsvarsrettAnsako_ANSAKO_10000_Validator : FormValidatorBase, IFormValidator, IFormWithChecklistAnswers
    {
        private AnsvarsrettAnsako_ANSAKO_10000_Form _validationForm;

        private readonly IValidationMessageComposer _validationMessageComposer;
        private readonly IMunicipalityValidator _municipalityValidator;
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
        private IAktoerValidator _foretakValidator;
        private IKontaktpersonValidator _foretakKontaktpersonValidator;
        private IKodelisteValidator _foretakPartstypeValidator;
        private IEnkelAdresseValidator _foretakEnkelAdresseValidator;
        //**Ansavarområde        
        private IAnsvarsomraadeValidator _ansvarsomraadeValidator;
        private IKodelisteValidator _funksjonValidator;
        private IKodelisteValidator _tiltaksklasseValidator;

        //EiendomByggested
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;

        //KommuneSaksnummer
        private ISaksnummerValidator _kommunensSaksnummerValidator;

        public AnsvarsrettAnsako_ANSAKO_10000_Validator(IValidationMessageComposer validationMessageComposer, IMunicipalityValidator municipalityValidator, ICodeListService codeListService
            , IPostalCodeService postalCodeService, IChecklistService checklistService)
            : base(validationMessageComposer, checklistService)
        {
            _validationMessageComposer = validationMessageComposer;
            _municipalityValidator = municipalityValidator;
            _checklistService = checklistService;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;

            _entitiesNodeList = new List<EntityValidatorNode>();
            _tiltakstypes = new string[] { };
        }

        public override ValidationResult StartValidation(ValidationInput validationInput)
        {
            _validationForm = SerializeUtil.DeserializeFromString<AnsvarsrettAnsako_ANSAKO_10000_Form>(validationInput.FormData);
            
            //GetRootXmlNode name
            var xmlRootElelement = _validationForm.GetType().GetCustomAttributes(typeof(XmlRootAttribute), true)?.SingleOrDefault() as XmlRootAttribute;
            base.XPathRoot = xmlRootElelement?.ElementName;
            base.StartValidation(validationInput);

            return ValidationResult;
        }

        protected override void InitializeValidatorConfig()
        {
            //AnsvarligSoeker
            var ansvarligSoekervalidatorNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 01, EnumId = EntityValidatorEnum.AnsvarligSoekerValidatorV2, ParentID = null},
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
                new () {NodeId = 09, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = 06},
                //ansvarsområde
                new () {NodeId = 10, EnumId = EntityValidatorEnum.AnsvarsomraadeValidator, ParentID = 05},
                new () {NodeId = 11, EnumId = EntityValidatorEnum.FunksjonValidator, ParentID = 10},
                new () {NodeId = 12, EnumId = EntityValidatorEnum.TiltaksklasseValidator, ParentID = 10},
            };
            _entitiesNodeList.AddRange(soeknadssystemetsReferanseNodeList);
            // EiendomByggested
            var eiendombyggestedNodeList = new List<EntityValidatorNode>()
            {
                new() { NodeId = 13, EnumId = EntityValidatorEnum.EiendomByggestedValidator, ParentID = null },
                new() { NodeId = 14, EnumId = EntityValidatorEnum.EiendomsAdresseValidatorV2, ParentID = 13 },
                new() { NodeId = 15, EnumId = EntityValidatorEnum.EiendomsidentifikasjonValidatorV2, ParentID = 13 },
            };
            _entitiesNodeList.AddRange(eiendombyggestedNodeList);

            //Kommunenes Saksnummer
            var kommunenesSaksnummerValidatorNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = 16, EnumId = EntityValidatorEnum.KommunensSaksnummerValidator, ParentID = null}
            };
            _entitiesNodeList.AddRange(kommunenesSaksnummerValidatorNodeList);
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
            var ansvarligSoekerPartstypes = new[] { "Foretak", "Organisasjon" };
            _ansvarligSoekerValidator = new AnsvarligSoekerValidatorV2(tree, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService, ansvarligSoekerPartstypes);

            //*Ansvarsrett
            //**Ansvarsområde
            _funksjonValidator = new FunksjonValidator(tree, 11, _codeListService);
            _tiltaksklasseValidator = new tiltaksklasseValidator(tree, 12, _codeListService);
            _ansvarsomraadeValidator = new AnsvarsomraadeValidator(tree, _funksjonValidator, _tiltaksklasseValidator);
            //**foretak
            _foretakPartstypeValidator = new PartstypeValidator(tree, 07, _codeListService);
            _foretakEnkelAdresseValidator = new EnkelAdresseValidator(tree, 08, _postalCodeService);
            _foretakKontaktpersonValidator = new KontaktpersonValidator(tree, 09);
           
            var foretakPartstypes = new[] { "Foretak"};
            _foretakValidator = new ForetakValidator(tree, _foretakEnkelAdresseValidator, _foretakKontaktpersonValidator, _foretakPartstypeValidator, _codeListService, foretakPartstypes);

            _AnsvarsrettValidator = new AnsvarsrettValidator(tree, _foretakValidator, _ansvarsomraadeValidator);

            //EiendomByggested
            _eiendomsAdresseValidator = new EiendomsAdresseValidatorV2(tree);
            _matrikkelValidator = new EiendomsidentifikasjonValidatorV2(tree, _codeListService);
            _eiendomByggestedValidator = new EiendomByggestedValidator(tree, _eiendomsAdresseValidator, _matrikkelValidator);

            //Kommunens saksnummer
            _kommunensSaksnummerValidator = new KommunensSaksnummerValidator(tree);

        }

        protected override void Validate(ValidationInput validationInput)
        {
            ValidateEntityFields(_validationForm);

            var ansvarsrettResult = _AnsvarsrettValidator.Validate(_validationForm.Ansvarsretts);
            AccumulateValidationMessages(ansvarsrettResult.ValidationMessages);

            var ansvarligSoekerValidationResult = _ansvarligSoekerValidator.Validate(_validationForm.AnsvarligSoeker);
            AccumulateValidationMessages(ansvarligSoekerValidationResult.ValidationMessages);

            var index = GetArrayIndex(_validationForm.eiendomByggested);
            for (int i = 0; i < index; i++)
            {
                Eiendom eiendom = Helpers.ObjectIsNullOrEmpty(_validationForm.eiendomByggested) ? null : _validationForm.eiendomByggested[i];
                var eiendomsResult = _eiendomByggestedValidator.Validate(eiendom);
                AccumulateValidationMessages(eiendomsResult.ValidationMessages, i);
            }

            var kommunensSaksnummerValidatorResult = _kommunensSaksnummerValidator.Validate(_validationForm.KommunensSaksnummer);
            AccumulateValidationMessages(kommunensSaksnummerValidatorResult.ValidationMessages);

        }

        public void ValidateEntityFields(AnsvarsrettAnsako_ANSAKO_10000_Form form)
        {
            if (string.IsNullOrEmpty(form.FraSluttbrukersystem))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.fraSluttbrukersystem);
            }
            else
            {
                if (!Guid.TryParse(form.FraSluttbrukersystem, out var referenceGuid))
                {
                    AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.fraSluttbrukersystem);
                }
            }

            if (string.IsNullOrEmpty(form.Prosjektnavn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.prosjektnavn);

            if (string.IsNullOrEmpty(form.Prosjektnr))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.prosjektnr);
        }


        protected override void DefineValidationRules()
        {
            // Local
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.fraSluttbrukersystem);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.fraSluttbrukersystem);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.prosjektnavn);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.prosjektnr);

            //AnsvarligSoeker
            AccumulateValidationRules(_ansvarligSoekerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerPartstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerKontaktpersonValidator.ValidationResult.ValidationRules);
            //*Ansvarsrett
            AccumulateValidationRules(_AnsvarsrettValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_foretakValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_foretakKontaktpersonValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_foretakPartstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_foretakEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarsomraadeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_funksjonValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltaksklasseValidator.ValidationResult.ValidationRules);

            //EiendomByggested
            AccumulateValidationRules(_matrikkelValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_eiendomsAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_eiendomByggestedValidator.ValidationResult.ValidationRules);

            //Kommunens saksnummer
            AccumulateValidationRules(_kommunensSaksnummerValidator.ValidationResult.ValidationRules);

        }

        public List<ChecklistAnswer> GetChecklistAnswersFromForm(ValidationInput validationInput)
        {
            //throw new NotImplementedException();
            return new List<ChecklistAnswer>();

        }
    }
}
