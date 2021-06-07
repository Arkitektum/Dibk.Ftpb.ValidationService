using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]
    public class ArbeidstilsynetsSamtykke2_45957_Validator : FormValidatorBase, IFormValidator
    {
        private readonly EntityValidatorOrchestrator _entityValidatorOrchestrator;
        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        //private readonly string _formXPath = "ArbeidstilsynetsSamtykke"; 
        public ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        private IArbeidsplasserValidator _arbeidsplasserValidator;
        private IEnkelAdresseValidator _tiltakshaverEnkelAdresseValidator;
        private IKontaktpersonValidator _kontaktpersonValidator;
        private IPartstypeValidator _partstypeValidator;
        private ITiltakshaverValidator _tiltakshaverValidator;
        private IEnkelAdresseValidator _fakturamottakerEnkelAdresseValidator;
        private IFakturamottakerValidator _fakturamottakerValidator;

        protected override string FormXPath => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke2_45957_Validator(EntityValidatorOrchestrator entityValidatorOrchestrator, IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _entityValidatorOrchestrator = entityValidatorOrchestrator;
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
        }

        public override ValidationResult StartValidation(ValidationInput validationInput)
        {
            InitializeValidatorConfig();

            //Get Arbeidstilsynets Samtykke v2 Dfv45957 class
            _form = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);

            // map to arbeidstilsynet formEntity 
            ArbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(_form);
            InitializeValidatorConfig();
            InstantiateValidators();
            Validate(ArbeidstilsynetsSamtykke2Form45957, validationInput);

            return ValidationResult;
        }

        protected override void InitializeValidatorConfig()
        {
            _entityValidatorOrchestrator.ValidatorFormName = this.GetType().Name;
            //_entityValidatorOrchestrator.ValidatorFormXPath = "ArbeidstilsynetsSamtykke";

            _entityValidatorOrchestrator.Validators = new List<EntityValidatorInfo>();

            //Eiendombyggested
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EiendomByggestedValidator", ParentXPath = FormXPath });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EiendomsAdresseValidator", ParentValidator = "EiendomByggestedValidator", ParentXPath = $"{FormXPath}/eiendomByggested{{0}}" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "MatrikkelValidator", ParentValidator = "EiendomByggestedValidator", ParentXPath = $"{FormXPath}/eiendomByggested{{0}}" });

            //Tiltakshaver
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "TiltakshaverValidator", ParentXPath = FormXPath });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EnkelAdresseValidatorV2", ParentValidator = "TiltakshaverValidator", ParentXPath = $"{FormXPath}/tiltakshaver" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "KontaktpersonValidator", ParentValidator = "TiltakshaverValidator", ParentXPath = $"{FormXPath}/tiltakshaver" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "PartstypeValidator", ParentValidator = "TiltakshaverValidator", ParentXPath = $"{FormXPath}/tiltakshaver" });

            //Fakturamottaker
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "FakturamottakerValidator", ParentXPath = FormXPath });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EnkelAdresseValidator", ParentValidator = "FakturamottakerValidator", ParentXPath = $"{FormXPath}/fakturamottaker" });

            //Arbeidsplasser
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "ArbeidsplasserValidator", ParentXPath = FormXPath });
        }

        protected override void InstantiateValidators()
        {
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(_entityValidatorOrchestrator);
            _matrikkelValidator = new MatrikkelValidator(_entityValidatorOrchestrator);
            _eiendomByggestedValidator = new EiendomByggestedValidator(_entityValidatorOrchestrator, _eiendomsAdresseValidator, _matrikkelValidator, _municipalityValidator);
            _arbeidsplasserValidator = new ArbeidsplasserValidator(_entityValidatorOrchestrator);
            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidatorV2(_entityValidatorOrchestrator, "TiltakshaverValidator");
            _kontaktpersonValidator = new KontaktpersonValidator(_entityValidatorOrchestrator, "TiltakshaverValidator");
            _partstypeValidator = new PartstypeValidator(_entityValidatorOrchestrator, "TiltakshaverValidator", _codeListService);
            _tiltakshaverValidator = new TiltakshaverValidator(_entityValidatorOrchestrator, _tiltakshaverEnkelAdresseValidator, _kontaktpersonValidator, _partstypeValidator, _codeListService);
            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorOrchestrator, "FakturamottakerValidator");
            _fakturamottakerValidator = new FakturamottakerValidator(_entityValidatorOrchestrator, _fakturamottakerEnkelAdresseValidator);

            DefineValidationRules();
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

        public ValidationResult Validate(ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity form, ValidationInput validationInput)
        {
            var eiendomValidationResult = _eiendomByggestedValidator.Validate(form.ModelData.EiendomValidationEntities);
            AccumulateValidationMessages(eiendomValidationResult.ValidationMessages);

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidationResult = _arbeidsplasserValidator.Validate(form.ModelData.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var tiltakshaverValidationResult = _tiltakshaverValidator.Validate(form.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            var fakturamottakerValidationResult = _fakturamottakerValidator.Validate(form.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);

            return ValidationResult;
        }
    }
}
