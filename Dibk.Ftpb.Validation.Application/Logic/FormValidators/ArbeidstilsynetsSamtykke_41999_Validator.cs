using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "41999")]
    public class ArbeidstilsynetsSamtykke_41999_Validator : FormValidatorBase, IFormValidator
    {
        private IList<EntityValidatorNode> _entityValidatorTree;

        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;
        private ArbeidstilsynetsSamtykke_41999_Form _validationForm { get; set; }
        
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        
        private ArbeidsplasserValidator _arbeidsplasserValidator;
        
        private IEnkelAdresseValidator _tiltakshaverEnkelAdresseValidator;
        private IKontaktpersonValidator _tiltakshaverKontaktpersonValidator;
        private IKodelisteValidator _tiltakshaverPartstypeValidator;
        private IAktoerValidator _tiltakshaverValidator;

        private IAktoerValidator _ansvarligSoekerValidator;
        private IEnkelAdresseValidator _ansvarligSoekerEnkelAdresseValidator;
        private IKontaktpersonValidator _ansvarligSoekerKontaktpersonValidator;
        private IKodelisteValidator _ansvarligSoekerPartstypeValidator;

        private IEnkelAdresseValidator _fakturamottakerEnkelAdresseValidator;
        private IFakturamottakerValidator _fakturamottakerValidator;

        protected override string XPathRoot => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke_41999_Validator(IValidationMessageComposer validationMessageComposer, IMunicipalityValidator municipalityValidator, ICodeListService codeListService, IPostalCodeService postalCodeService)
            : base(validationMessageComposer)
        {
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;
        }

        public override ValidationResult StartValidation(string dataFormatVersion, ValidationInput validationInput)
        {
            _validationForm = new ArbeidstilsynetsSamtykke_41999_Deserializer().Deserialize(validationInput.FormData);

            base.StartValidation(dataFormatVersion, validationInput);
            //ValidationReport.ValidationResult = ValidationResult;

            return _validationResult;
        }

        protected override void InitializeValidatorConfig()
        {
            var eiendombyggestedTree = new List<EntityValidatorNode>()
            {
                new () {NodeId = 1, EnumId = EntityValidatorEnum.EiendomByggestedValidator, ParentID = null},
                new () {NodeId = 2, EnumId = EntityValidatorEnum.EiendomsAdresseValidator, ParentID = 1},
                new () {NodeId = 3, EnumId = EntityValidatorEnum.MatrikkelValidator, ParentID = 1},
            };
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

            //Datamodellen for denne skjemaversjon er ulik v.2 - kommenterer derfor ut
            //var beskrivelseAvTiltakTree = new List<EntityValidatorNode>()
            //{
            //    new () {Id = 15, EnumId = EntityValidatorEnum.BeskrivelseAvTiltakValidator, ParentID = null},
            //    new () {Id = 16, EnumId = EntityValidatorEnum.FormaaltypeValidator, ParentID = 15},
            //    new () {Id = 17, EnumId = EntityValidatorEnum.AnleggstypeValidator, ParentID = 16},
            //    new () {Id = 18, EnumId = EntityValidatorEnum.NaeringsgruppeValidator, ParentID = 16},
            //    new () {Id = 19, EnumId = EntityValidatorEnum.BygningstypeValidator, ParentID = 16},
            //    new () {Id = 20, EnumId = EntityValidatorEnum.TiltaksformaalValidator, ParentID = 16},
            //    new () {Id = 21, EnumId = EntityValidatorEnum.TiltakstypeValidator, ParentID = 15},
            //};

            var formTree = new List<EntityValidatorNode>();
            formTree.AddRange(tiltakshaverTree);
            formTree.AddRange(eiendombyggestedTree);
            formTree.AddRange(fakturamottakerTree);
            formTree.AddRange(arbeidsplasserTree);
            formTree.AddRange(ansvarligSoekerTree);
            //formTree.AddRange(beskrivelseAvTiltakTree);

            EntityValidatorTree = EntityValidatiorTree.BuildTree(formTree);

        }

        protected override void InstantiateValidators()
        {
            //EiendomByggested
            _matrikkelValidator = new MatrikkelValidator(EntityValidatorTree, _municipalityValidator);
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(EntityValidatorTree);
            _eiendomByggestedValidator = new EiendomByggestedValidator(EntityValidatorTree, _eiendomsAdresseValidator, _matrikkelValidator);

            //Tiltakshaver
            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(EntityValidatorTree, 7, _postalCodeService);
            _tiltakshaverKontaktpersonValidator = new KontaktpersonValidator(EntityValidatorTree, 5);
            _tiltakshaverPartstypeValidator = new PartstypeValidator(EntityValidatorTree, 6, _codeListService);
            _tiltakshaverValidator = new TiltakshaverValidator(EntityValidatorTree, _tiltakshaverEnkelAdresseValidator, _tiltakshaverKontaktpersonValidator, _tiltakshaverPartstypeValidator, _codeListService);

            //AnsvarligSoeker
            _ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(EntityValidatorTree, 12, _postalCodeService);
            _ansvarligSoekerKontaktpersonValidator = new KontaktpersonValidator(EntityValidatorTree, 13);
            _ansvarligSoekerPartstypeValidator = new PartstypeValidator(EntityValidatorTree, 14, _codeListService);
            _ansvarligSoekerValidator = new AnsvarligSoekerValidator(EntityValidatorTree, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);

            //Fakturamottaker
            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidator(EntityValidatorTree, 9, _postalCodeService);
            _fakturamottakerValidator = new FakturamottakerValidator(EntityValidatorTree, _fakturamottakerEnkelAdresseValidator);

            //Arbeidsplasser
            _arbeidsplasserValidator = new ArbeidsplasserValidator(EntityValidatorTree);

        }
        protected override void DefineValidationRules()
        {
            AccumulateValidationRules(_eiendomByggestedValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_eiendomsAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_matrikkelValidator.ValidationResult.ValidationRules);
            
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
        }

        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            //TODO add error massages if.... :thinkingFace
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        protected override void Validate(ValidationInput validationInput)
        {

            var eiendoms = _validationForm?.EiendomByggested;
            var index = GetArrayIndex(eiendoms);

            for (int i = 0; i < index; i++)
            {
                var eiendom = Helpers.ObjectIsNullOrEmpty(eiendoms) ? null : eiendoms[i];
                var eiendomValidationResult = _eiendomByggestedValidator.Validate(eiendom);
                AccumulateValidationMessages(eiendomValidationResult.ValidationMessages, i);
            }

            var tiltakshaverValidationResult = _tiltakshaverValidator.Validate(_validationForm.Tiltakshaver);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            var ansvarligSoekerValidationResult = _ansvarligSoekerValidator.Validate(_validationForm.AnsvarligSoeker);
            AccumulateValidationMessages(ansvarligSoekerValidationResult.ValidationMessages);

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToArray();
            var arbeidsplasserValidationResult = _arbeidsplasserValidator.Validate(_validationForm.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var fakturamottakerValidationResult = _fakturamottakerValidator.Validate(_validationForm.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);
        }

        protected override IEnumerable<string> GetFormTiltakstyper()
        {
            //Read comment in line 103
            return new List<string>();
        }
    }
}
