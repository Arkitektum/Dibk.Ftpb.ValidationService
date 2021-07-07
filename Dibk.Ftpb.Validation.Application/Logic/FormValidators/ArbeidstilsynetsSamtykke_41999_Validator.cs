using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke;
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
        private ArbeidstilsynetsSamtykke_41999_ValidationEntity _validationForm { get; set; }
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        private IArbeidsplasserValidator _arbeidsplasserValidator;
        private IEnkelAdresseValidator _tiltakshaverEnkelAdresseValidator;
        private IKontaktpersonValidator _kontaktpersonValidator;
        private IKodelisteValidator _partstypeValidator;
        private IAktoerValidator _tiltakshaverValidator;
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
            ArbeidstilsynetsSamtykkeType formModel = new ArbeidstilsynetsSamtykke_41999_Deserializer().Deserialize(validationInput.FormData);
            _validationForm = new ArbeidstilsynetsSamtykke_41999_Mapper().GetFormEntity(formModel);

            base.StartValidation(dataFormatVersion, validationInput);

            return ValidationResult;
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
            _matrikkelValidator = new MatrikkelValidator(EntityValidatorTree, 3);
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(EntityValidatorTree, 2);
            _eiendomByggestedValidator = new EiendomByggestedValidator(EntityValidatorTree, 1, _eiendomsAdresseValidator, _matrikkelValidator, _municipalityValidator);

            //Tiltakshaver
            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(EntityValidatorTree, 7, _postalCodeService);
            _kontaktpersonValidator = new KontaktpersonValidator(EntityValidatorTree, 5);
            _partstypeValidator = new PartstypeValidator(EntityValidatorTree, 6, _codeListService);
            _tiltakshaverValidator = new TiltakshaverValidator(EntityValidatorTree, 4, _tiltakshaverEnkelAdresseValidator, _kontaktpersonValidator, _partstypeValidator, _codeListService);
            
            //Arbeidsplasser
            _arbeidsplasserValidator = new ArbeidsplasserValidator(EntityValidatorTree, 10);

            //Fakturamottaker
            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidator(EntityValidatorTree, 9, _postalCodeService);
            _fakturamottakerValidator = new FakturamottakerValidator(EntityValidatorTree, 8, _fakturamottakerEnkelAdresseValidator);

        }
        protected override void DefineValidationRules()
        {
            AccumulateValidationRules(_eiendomByggestedValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_eiendomsAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_matrikkelValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_arbeidsplasserValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_partstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakshaverEnkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_kontaktpersonValidator.ValidationResult.ValidationRules);
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
            var eiendomValidationResult = _eiendomByggestedValidator.Validate(_validationForm.ModelData.EiendomValidationEntities);
            AccumulateValidationMessages(eiendomValidationResult.ValidationMessages);

            var tiltakshaverValidationResult = _tiltakshaverValidator.Validate(_validationForm.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidationResult = _arbeidsplasserValidator.Validate(_validationForm.ModelData.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var fakturamottakerValidationResult = _fakturamottakerValidator.Validate(_validationForm.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);
        }
    }
}
