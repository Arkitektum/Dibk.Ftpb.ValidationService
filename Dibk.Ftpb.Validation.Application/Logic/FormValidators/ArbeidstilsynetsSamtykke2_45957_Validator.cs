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
using Dibk.Ftpb.Validation.Application.Services;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]
    public class ArbeidstilsynetsSamtykke2_45957_Validator : FormValidatorBase, IFormValidator
    {
        private List<EntityValidatorNode> _entitiesNodeList;


        private ArbeidstilsynetsSamtykke2_45957_ValidationEntity _validationForm { get; set; }

        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;
        private readonly IChecklistService _checklistService;

        private ArbeidsplasserValidatorV2 _arbeidsplasserValidator;

        //**
        //EiendomByggested
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        //Tiltakshaver
        private IAktoerValidator _tiltakshaverValidator;
        private IEnkelAdresseValidator _enkelAdresseValidator;
        private IKodelisteValidator _partstypeValidator;
        private IKontaktpersonValidator _kontaktpersonValidator;
        //Fakturamotaker
        private IFakturamottakerValidator _fakturamottakerValidator;
        private IEnkelAdresseValidator _fakturamottakerEnkelAdresseValidator;

        //BeskrivelseAvTiltak
        private AnleggstypeValidator _anleggstypeValidator;
        private NaeringsgruppeValidator _naeringsgruppeValidator;
        private BygningstypeValidator _bygningstypeValidator;
        private TiltaksformaalValidator _tiltaksformaalValidator;
        private FormaaltypeValidator _formaaltypeValidator;
        private TiltakstypeValidator _tiltakstypeValidator;
        private IBeskrivelseAvTiltakValidator _beskrivelseAvTiltakValidator;

        //AnsvarligSoeker
        private IAktoerValidator _ansvarligSoekerValidator;
        private IEnkelAdresseValidator _ansvarligSoekerEnkelAdresseValidator;
        private IKodelisteValidator _ansvarligSoekerPartstypeValidator;
        private IKontaktpersonValidator _ansvarligSoekerKontaktpersonValidator;

        //Sjekklistekrav
        private IKodelisteValidator _sjekklistepunktValidator;
        private ISjekklistekravValidator _sjekklistekravValidator;


        //Metadata
        private IMetadataValidator _metadataValidator;

        //Saksnummer
        private ISaksnummerValidator _arbeidstilsynetsSaksnummerValidator;
        private ISaksnummerValidator _kommunensSaksnummerValidator;

        protected override string XPathRoot => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke2_45957_Validator(IValidationMessageComposer validationMessageComposer
                                                        , IMunicipalityValidator municipalityValidator, ICodeListService codeListService
                                                        , IPostalCodeService postalCodeService, IChecklistService checklistService)
            : base(validationMessageComposer)
        {
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;
            _checklistService = checklistService;
            _entitiesNodeList = new List<EntityValidatorNode>();
        }

        public override ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            ArbeidstilsynetsSamtykkeType formModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);
            _validationForm = new ArbeidstilsynetsSamtykke2_45957_Mapper().GetFormEntity(formModel);

            base.StartValidation(dataFormatVersion, validationInput);
            ValidationReport.ValidationResult = ValidationResult;

            return ValidationResult;
        }

        protected override void InitializeValidatorConfig()
        {

            // EiendomByggested
            List<EntityValidatorNode> eiendombyggestedNodeList = new()
            {
                new() { NodeId = 1, EnumId = EntityValidatorEnum.EiendomByggestedValidator, ParentID = null },
                new() { NodeId = 2, EnumId = EntityValidatorEnum.EiendomsAdresseValidator, ParentID = 1 },
                new() { NodeId = 3, EnumId = EntityValidatorEnum.MatrikkelValidator, ParentID = 1 },
            };
            _entitiesNodeList.AddRange(eiendombyggestedNodeList);

            // Tiltakshaver
            var tiltakshaverNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 4, EnumId = EntityValidatorEnum.TiltakshaverValidator, ParentID = null},
                new () {NodeId = 5, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = 4},
                new () {NodeId = 6, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 4},
                new () {NodeId = 7, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 4}
            };
            _entitiesNodeList.AddRange(tiltakshaverNodeList);

            //fakturamottake
            var fakturamottakeNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 8, EnumId = EntityValidatorEnum.FakturamottakerValidator, ParentID = null},
                new () {NodeId = 9, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 8}
            };
            _entitiesNodeList.AddRange(fakturamottakeNodeList);

            var beskrivelseAvTiltakNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 10, EnumId = EntityValidatorEnum.BeskrivelseAvTiltakValidator, ParentID = null},
                new () {NodeId = 11, EnumId = EntityValidatorEnum.FormaaltypeValidator, ParentID = 10},
                new () {NodeId = 12, EnumId = EntityValidatorEnum.AnleggstypeValidator, ParentID = 11},
                new () {NodeId = 13, EnumId = EntityValidatorEnum.NaeringsgruppeValidator, ParentID = 11},
                new () {NodeId = 14, EnumId = EntityValidatorEnum.BygningstypeValidator, ParentID = 11},
                new () {NodeId = 15, EnumId = EntityValidatorEnum.TiltaksformaalValidator, ParentID = 11},
                new () {NodeId = 16, EnumId = EntityValidatorEnum.TiltakstypeValidator, ParentID = 10},
            };
            _entitiesNodeList.AddRange(beskrivelseAvTiltakNodeList);

            var arbeidsplasserValidatorNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = 17, EnumId = EntityValidatorEnum.ArbeidsplasserValidatorV2, ParentID = null}
            };
            _entitiesNodeList.AddRange(arbeidsplasserValidatorNodeList);

            //AnsvarligSoeker
            var ansvarligSoekervalidatorNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = 18, EnumId = EntityValidatorEnum.AnsvarligSoekerValidator, ParentID = null},
                new () {NodeId = 19, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = 18},
                new () {NodeId = 20, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = 18},
                new () {NodeId = 21, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = 18}
            };
            _entitiesNodeList.AddRange(ansvarligSoekervalidatorNodeList);

            //Sjekklistekrav
            var sjekklistekravValidatorNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = 22, EnumId = EntityValidatorEnum.SjekklistekravValidator, ParentID = null},
                new() {NodeId = 23, EnumId = EntityValidatorEnum.SjekklistepunktValidator, ParentID = 22}
            };
            _entitiesNodeList.AddRange(sjekklistekravValidatorNodeList);

            //Metadata
            var metadataValidatorNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = 24, EnumId = EntityValidatorEnum.MetadataValidator, ParentID = null}
            };
            _entitiesNodeList.AddRange(metadataValidatorNodeList);

            //Arbeidstilsynets Saksnummer
            var arbeidstilsynetsSaksnummerValidatorNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = 25, EnumId = EntityValidatorEnum.ArbeidstilsynetsSaksnummerValidator, ParentID = null}
            };
            _entitiesNodeList.AddRange(arbeidstilsynetsSaksnummerValidatorNodeList);

            //Kommunenes Saksnummer
            var kommunenesSaksnummerValidatorNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = 26, EnumId = EntityValidatorEnum.KommunensSaksnummerValidator, ParentID = null}
            };
            _entitiesNodeList.AddRange(kommunenesSaksnummerValidatorNodeList);

        }

        protected override void InstantiateValidators()
        {
            var tree = EntityValidatiorTree.BuildTree(_entitiesNodeList);
            //Sjekklistekrav
            _sjekklistepunktValidator = new SjekklistepunktValidator(tree, _codeListService);
            _sjekklistekravValidator = new SjekklistekravValidator(tree, _sjekklistepunktValidator, _codeListService);

            //AnsvarligSoeker
            _ansvarligSoekerKontaktpersonValidator = new KontaktpersonValidator(tree, 19);
            _ansvarligSoekerPartstypeValidator = new PartstypeValidator(tree, 20, _codeListService);
            _ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(tree, 21, _postalCodeService);
            _ansvarligSoekerValidator = new AnsvarligSoekerValidator(tree, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);

            //Arbaidsplaser
            _arbeidsplasserValidator = new ArbeidsplasserValidatorV2(tree);
            //BeskrivelseAvTiltak
            _anleggstypeValidator = new AnleggstypeValidator(tree, _codeListService);
            _naeringsgruppeValidator = new NaeringsgruppeValidator(tree, _codeListService);
            _bygningstypeValidator = new BygningstypeValidator(tree, _codeListService);
            _tiltaksformaalValidator = new TiltaksformaalValidator(tree, _codeListService);
            _formaaltypeValidator = new FormaaltypeValidator(tree, _anleggstypeValidator, _naeringsgruppeValidator, _bygningstypeValidator, _tiltaksformaalValidator);
            _tiltakstypeValidator = new TiltakstypeValidator(tree, _codeListService);
            _beskrivelseAvTiltakValidator = new BeskrivelseAvTiltakValidator(tree, _formaaltypeValidator, _tiltakstypeValidator);

            //EiendomByggested
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(tree);
            _matrikkelValidator = new MatrikkelValidator(tree, _municipalityValidator);
            _eiendomByggestedValidator = new EiendomByggestedValidator(tree, _eiendomsAdresseValidator, _matrikkelValidator);
            //Tiltakshaver
            _kontaktpersonValidator = new KontaktpersonValidator(tree, 4);
            _partstypeValidator = new PartstypeValidator(tree, 6, _codeListService);
            _enkelAdresseValidator = new EnkelAdresseValidator(tree, 7, _postalCodeService);
            _tiltakshaverValidator = new TiltakshaverValidator(tree, _enkelAdresseValidator, _kontaktpersonValidator, _partstypeValidator, _codeListService);
            //fakturamottaker
            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidator(tree, 9, _postalCodeService);
            _fakturamottakerValidator = new FakturamottakerValidator(tree, _fakturamottakerEnkelAdresseValidator);
            
            //Metadata
            _metadataValidator = new MetadataValidator(tree);
            //Arbeidstilsynets saksnummer
            _arbeidstilsynetsSaksnummerValidator = new ArbeidstilsynetsSaksnummerValidator(tree);
            //Kommunens saksnummer
            _kommunensSaksnummerValidator = new KommunensSaksnummerValidator(tree);

        }
        protected override void DefineValidationRules()
        {
            //TODO create a method to do this automatic, potential error

            //Sjekklistekrav
            AccumulateValidationRules(_sjekklistepunktValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_sjekklistekravValidator.ValidationResult.ValidationRules);
            //EiendomByggested rules
            AccumulateValidationRules(_matrikkelValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_eiendomsAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_eiendomByggestedValidator.ValidationResult.ValidationRules);
            //Tiltashaver
            AccumulateValidationRules(_kontaktpersonValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_partstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_enkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverValidator.ValidationResult.ValidationRules);
            //fakturamottaker
            AccumulateValidationRules(_fakturamottakerEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_fakturamottakerValidator.ValidationResult.ValidationRules);
            //BeskrivelseAvTiltak
            AccumulateValidationRules(_anleggstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_naeringsgruppeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_bygningstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltaksformaalValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_formaaltypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_beskrivelseAvTiltakValidator.ValidationResult.ValidationRules);
            //Arbeidsplasser
            AccumulateValidationRules(_arbeidsplasserValidator.ValidationResult.ValidationRules);
            //AnsvarligSoeker
            AccumulateValidationRules(_ansvarligSoekerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerPartstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerKontaktpersonValidator.ValidationResult.ValidationRules);

            //Metadata
            AccumulateValidationRules(_metadataValidator.ValidationResult.ValidationRules);
            //Arbeidstilsynets saksnummer
            AccumulateValidationRules(_arbeidstilsynetsSaksnummerValidator.ValidationResult.ValidationRules);
            //Kommunens saksnummer
            AccumulateValidationRules(_kommunensSaksnummerValidator.ValidationResult.ValidationRules);

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
            var arbeidsplasserValidationResult = _arbeidsplasserValidator.Validate(_validationForm.ModelData.ArbeidsplasserValidationEntity, _validationForm.ModelData.SjekklistekravValidationEntities, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var tiltakshaverValidationResult = _tiltakshaverValidator.Validate(_validationForm.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            var ansvarligSoekerValidationResult = _ansvarligSoekerValidator.Validate(_validationForm.ModelData.AnsvarligSoekerValidationEntity);
            AccumulateValidationMessages(ansvarligSoekerValidationResult.ValidationMessages);

            var fakturamottakerValidationResult = _fakturamottakerValidator.Validate(_validationForm.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);

            var sjekklistekravValidationResult = _sjekklistekravValidator.Validate(_validationForm.ModelData.SjekklistekravValidationEntities, _checklistService);
            AccumulateValidationMessages(sjekklistekravValidationResult.ValidationMessages);

            var beskrivelseAvTiltakValidationResult = _beskrivelseAvTiltakValidator.Validate(_validationForm.ModelData.BeskrivelseAvTiltakValidationEntity);
            AccumulateValidationMessages(beskrivelseAvTiltakValidationResult.ValidationMessages);

            var metadataValidationResult = _metadataValidator.Validate(_validationForm.ModelData.MetadataValidationEntity);
            AccumulateValidationMessages(metadataValidationResult.ValidationMessages);

            var arbeidstilsynetsSaksnummerValidationResult = _arbeidstilsynetsSaksnummerValidator.Validate(_validationForm.ModelData.ArbeidstilsynetsSaksnummerValidationEntity);
            AccumulateValidationMessages(arbeidstilsynetsSaksnummerValidationResult.ValidationMessages);

            var kommunensSaksnummerValidationResult = _arbeidstilsynetsSaksnummerValidator.Validate(_validationForm.ModelData.KommunensSaksnummerValidationEntity);
            AccumulateValidationMessages(kommunensSaksnummerValidationResult.ValidationMessages);

        }


    }
}
