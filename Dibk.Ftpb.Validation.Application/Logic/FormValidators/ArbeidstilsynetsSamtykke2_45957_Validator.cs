using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
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

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]
    public class ArbeidstilsynetsSamtykke2_45957_Validator : FormValidatorBase, IFormValidator
    {
        private readonly EntityValidatorOrchestrator _entityValidatorOrchestrator;
        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        private ArbeidstilsynetsSamtykke2_45957_ValidationEntity _validationForm { get; set; }
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
        private ISjekklistepunktValidator _sjekklistepunktValidator;

        protected override string XPathRoot => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke2_45957_Validator(EntityValidatorOrchestrator entityValidatorOrchestrator, IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _entityValidatorOrchestrator = entityValidatorOrchestrator;
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
        }

        public override ValidationResult StartValidation(ValidationInput validationInput)
        {
            ArbeidstilsynetsSamtykkeType formModel = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);
            _validationForm = new ArbeidstilsynetsSamtykke2_45957_Mapper().GetFormEntity(formModel, XPathRoot);
            
            base.StartValidation(validationInput);
            
            return ValidationResult;
        }

        protected override void InitializeValidatorConfig()
        {
            _entityValidatorOrchestrator.ValidatorFormName = this.GetType().Name;
            _entityValidatorOrchestrator.ValidatorFormXPath = XPathRoot;

            _entityValidatorOrchestrator.Validators = new List<EntityValidatorInfo>();

            //Eiendombyggested
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomByggestedValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomsAdresseValidator, EntityValidatorEnum.EiendomByggestedValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.MatrikkelValidator, EntityValidatorEnum.EiendomByggestedValidator));

            //Tiltakshaver
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltakshaverValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.TiltakshaverValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.KontaktpersonValidator, EntityValidatorEnum.TiltakshaverValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.PartstypeValidator, EntityValidatorEnum.TiltakshaverValidator));

            //AnsvarligSoeker
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.AnsvarligSoekerValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.AnsvarligSoekerValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.KontaktpersonValidator, EntityValidatorEnum.AnsvarligSoekerValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.PartstypeValidator, EntityValidatorEnum.AnsvarligSoekerValidator));

            //Fakturamottaker
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.FakturamottakerValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidatorV2, EntityValidatorEnum.FakturamottakerValidator));

            //Arbeidsplasser
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.ArbeidsplasserValidator));

            //Sjekkliste
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.SjekklistekravValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.SjekklistepunktValidator, EntityValidatorEnum.SjekklistekravValidator));

        }

        protected override void InstantiateValidators()
        {
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(_entityValidatorOrchestrator, EntityValidatorEnum.EiendomByggestedValidator);
            _matrikkelValidator = new MatrikkelValidator(_entityValidatorOrchestrator, EntityValidatorEnum.EiendomByggestedValidator);
            _eiendomByggestedValidator = new EiendomByggestedValidator(_entityValidatorOrchestrator, _eiendomsAdresseValidator, _matrikkelValidator, _municipalityValidator);
            
            _arbeidsplasserValidator = new ArbeidsplasserValidator(_entityValidatorOrchestrator);

            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator);
            _tiltakshaverKontaktpersonValidator = new KontaktpersonValidator(_entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator);
            _tiltakshaverPartstypeValidator = new PartstypeValidator(_entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator, _codeListService);
            _tiltakshaverValidator = new AktoerValidator(_entityValidatorOrchestrator, AktoerEnum.tiltakshaver, _tiltakshaverEnkelAdresseValidator, _tiltakshaverKontaktpersonValidator, _tiltakshaverPartstypeValidator, _codeListService);

            _ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorOrchestrator, EntityValidatorEnum.AnsvarligSoekerValidator);
            _ansvarligSoekerKontaktpersonValidator = new KontaktpersonValidator(_entityValidatorOrchestrator, EntityValidatorEnum.AnsvarligSoekerValidator);
            _ansvarligSoekerPartstypeValidator = new PartstypeValidator(_entityValidatorOrchestrator, EntityValidatorEnum.AnsvarligSoekerValidator, _codeListService);
            _ansvarligSoekerValidator = new AktoerValidator(_entityValidatorOrchestrator, AktoerEnum.ansvarligSoeker, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);

            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidatorV2(_entityValidatorOrchestrator, EntityValidatorEnum.FakturamottakerValidator);
            _fakturamottakerValidator = new FakturamottakerValidator(_entityValidatorOrchestrator, _fakturamottakerEnkelAdresseValidator);

            _sjekklistepunktValidator = new SjekklistepunktValidator(_entityValidatorOrchestrator, EntityValidatorEnum.SjekklistekravValidator);
            _sjekklistekravValidator = new SjekklistekravValidator(_entityValidatorOrchestrator, _sjekklistepunktValidator);
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
            AccumulateValidationRules(_sjekklistekravValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_sjekklistepunktValidator.ValidationResult.ValidationRules);
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
        }
    }
}
