using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]
    public class ArbeidstilsynetsSamtykke2_45957_Validator : FormValidatorBase, IFormValidator
    {
        private ArbeidstilsynetsSamtykke2_45957_ValidationEntity _validationForm { get; set; }


        //TODO abstract class / Interface ... :thinkingface ...
        private BeskrivelseAvTiltakValidatorLogic _beskrivelseAvTiltakValidatorLogic;
        private EiendombyggestedLogic _eiendombyggestedLogic;


        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        private IArbeidsplasserValidator _arbeidsplasserValidator;

        private IAktoerValidator _tiltakshaverValidator;
        private IEnkelAdresseValidator _tiltakshaverEnkelAdresseValidator;
        private IKontaktpersonValidator _tiltakshaverKontaktpersonValidator;
        private IKodelisteValidator _tiltakshaverPartstypeValidator;

        private IAktoerValidator _ansvarligSoekerValidator;
        private IEnkelAdresseValidator _ansvarligSoekerEnkelAdresseValidator;
        private IKontaktpersonValidator _ansvarligSoekerKontaktpersonValidator;
        private IKodelisteValidator _ansvarligSoekerPartstypeValidator;

        private IEnkelAdresseValidator _fakturamottakerEnkelAdresseValidator;
        private IFakturamottakerValidator _fakturamottakerValidator;

        private ISjekklistekravValidator _sjekklistekravValidator;
        //private IKodelisteValidator _sjekklistepunktValidator;


        private AnleggstypeValidator _anleggstypeValidator;
        private NaeringsgruppeValidator _naeringsgruppeValidator;
        private BygningstypeValidator _bygningstypeValidator;
        private TiltaksformaalValidator _tiltaksformaalValidator;
        private FormaaltypeValidator _formaaltypeValidator;
        private TiltakstypeValidator _tiltakstypeValidator;
        private IBeskrivelseAvTiltakValidator _beskrivelseAvTiltakValidator;



        protected override string XPathRoot => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke2_45957_Validator(IValidationMessageComposer validationMessageComposer, IMunicipalityValidator municipalityValidator, ICodeListService codeListService, IPostalCodeService postalCodeService)
            : base(validationMessageComposer)
        {
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;

           


        }

        public override ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            ArbeidstilsynetsSamtykkeType formModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);
            _validationForm = new ArbeidstilsynetsSamtykke2_45957_Mapper().GetFormEntity(formModel);

            base.StartValidation(dataFormatVersion, validationInput);


            return ValidationResult;
        }

        protected override void InitializeValidatorConfig()
        {
            _beskrivelseAvTiltakValidatorLogic = new BeskrivelseAvTiltakValidatorLogic(15, _codeListService);
            _eiendombyggestedLogic = new EiendombyggestedLogic(1, _municipalityValidator);

            var tiltakshaverTree = new List<EntityValidatorNode>()
            {
                new () {NodeId = 4, EnumId = EntityValidatorEnum.TiltakshaverValidator, ParentID = null},
                new () {NodeId = 5, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = 4},
                new () {NodeId = 6, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 4},
                new () {NodeId = 7, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 4}
            };
            var fakturamottakerTree = new List<EntityValidatorNode>()
            {
                new () {NodeId = 8, EnumId = EntityValidatorEnum.FakturamottakerValidator, ParentID = null},
                new () {NodeId = 9, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 8}
            };

            var arbeidsplasserTree = new List<EntityValidatorNode>()
            {
                new() {NodeId = 10, EnumId = EntityValidatorEnum.ArbeidsplasserValidator, ParentID = null}
            };

            var ansvarligSoekerTree = new List<EntityValidatorNode>()
            {
                new () {NodeId = 11, EnumId = EntityValidatorEnum.AnsvarligSoekerValidator, ParentID = null},
                new () {NodeId = 12, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 11},
                new () {NodeId = 13, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = 11,},
                new () {NodeId = 14, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 11,}
            };



            var sjekklisteKravTree = new List<EntityValidatorNode>()
            {
                new() {NodeId = 22, EnumId = EntityValidatorEnum.SjekklistekravValidator, ParentID = null},
                new() {NodeId = 23, EnumId = EntityValidatorEnum.SjekklistepunktValidator, ParentID = 22}
            };

            var formTree = new List<EntityValidatorNode>();
            formTree.AddRange(tiltakshaverTree);
            formTree.AddRange(fakturamottakerTree);
            formTree.AddRange(arbeidsplasserTree);
            formTree.AddRange(ansvarligSoekerTree);
            formTree.AddRange(sjekklisteKravTree);
            EntityValidatorTree = EntityValidatiorTree.BuildTree(formTree);
        }

        protected override void InstantiateValidators()
        {
            

            //Tiltakshaver
            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(EntityValidatorTree, 7, _postalCodeService);
            _tiltakshaverKontaktpersonValidator = new KontaktpersonValidator(EntityValidatorTree, 5);
            _tiltakshaverPartstypeValidator = new PartstypeValidator(EntityValidatorTree, 6, _codeListService);
            _tiltakshaverValidator = new TiltakshaverValidator(EntityValidatorTree, 4, _tiltakshaverEnkelAdresseValidator, _tiltakshaverKontaktpersonValidator, _tiltakshaverPartstypeValidator, _codeListService);

            //AnsvarligSoeker
            _ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(EntityValidatorTree, 12, _postalCodeService);
            _ansvarligSoekerKontaktpersonValidator = new KontaktpersonValidator(EntityValidatorTree, 13);
            _ansvarligSoekerPartstypeValidator = new PartstypeValidator(EntityValidatorTree, 14, _codeListService);
            _ansvarligSoekerValidator = new AnsvarligSoekerValidator(EntityValidatorTree, 11, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);

            //Fakturamottaker
            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidatorV2(EntityValidatorTree, 9);
            _fakturamottakerValidator = new FakturamottakerValidator(EntityValidatorTree, 8, _fakturamottakerEnkelAdresseValidator);

            //Arbeidsplasser
            _arbeidsplasserValidator = new ArbeidsplasserValidator(EntityValidatorTree, 10);



            //_sjekklistepunktValidator = new SjekklistepunktValidator(EntityValidatorTree, 23, _codeListService);
            //_sjekklistekravValidator = new SjekklistekravValidator(EntityValidatorTree, 22, _sjekklistepunktValidator);
            _sjekklistekravValidator = new SjekklistekravValidator(EntityValidatorTree, 22);


            //_naeringsgruppeValidator, _codeListService,
            //_bygningstypeValidator, _codeListService,
            //_tiltaksformaalValidator, _codeListService);


            //** New
            //**BeskrivelseAvTiltak
            _beskrivelseAvTiltakValidator = _beskrivelseAvTiltakValidatorLogic.Validator;
            //Eiendombyggested
            _eiendomByggestedValidator = _eiendombyggestedLogic.Validator;


        }
        protected override void DefineValidationRules()
        {
            AccumulateValidationRules(_arbeidsplasserValidator.ValidationResult.ValidationRules);

            AccumulateValidationRules(_tiltakshaverValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverPartstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverKontaktpersonValidator.ValidationResult.ValidationRules);

            AccumulateValidationRules(_ansvarligSoekerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerPartstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerKontaktpersonValidator.ValidationResult.ValidationRules);

            AccumulateValidationRules(_fakturamottakerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_fakturamottakerEnkelAdresseValidator.ValidationResult.ValidationRules);

            AccumulateValidationRules(_sjekklistekravValidator.ValidationResult.ValidationRules);
            //AccumulateValidationRules(_sjekklistepunktValidator.ValidationResult.ValidationRules);

            //** New
            AccumulateValidationRules(_beskrivelseAvTiltakValidatorLogic.ValidationRules);
            AccumulateValidationRules(_eiendombyggestedLogic.ValidationRules);

        }

        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            //TODO add error massages if.... :thinkingFace
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        protected override void Validate(ValidationInput validationInput)
        {
            var eiendomValidationResult = _eiendomByggestedValidator.Validate(_validationForm.ModelData.EiendomValidationEntities);
            AccumulateValidationMessages(eiendomValidationResult.ValidationMessages);

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidationResult = _arbeidsplasserValidator.Validate(_validationForm.ModelData.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var tiltakshaverValidationResult = _tiltakshaverValidator.Validate(_validationForm.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            var ansvarligSoekerValidationResult = _ansvarligSoekerValidator.Validate(_validationForm.ModelData.AnsvarligSoekerValidationEntity);
            AccumulateValidationMessages(ansvarligSoekerValidationResult.ValidationMessages);

            var fakturamottakerValidationResult = _fakturamottakerValidator.Validate(_validationForm.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);

            var sjekklistekravValidationResult = _sjekklistekravValidator.Validate(_validationForm.ModelData.SjekklistekravValidationEntities);
            AccumulateValidationMessages(sjekklistekravValidationResult.ValidationMessages);

            var beskrivelseAvTiltakValidationResult = _beskrivelseAvTiltakValidator.Validate(_validationForm.ModelData.BeskrivelseAvTiltakValidationEntity);
            AccumulateValidationMessages(beskrivelseAvTiltakValidationResult.ValidationMessages);
        }
    }
}
