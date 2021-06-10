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

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "41999")]
    public class ArbeidstilsynetsSamtykke_41999_Validator : FormValidatorBase, IFormValidator
    {
        private readonly EntityValidatorOrchestrator _entityValidatorOrchestrator;
        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        private ArbeidstilsynetsSamtykke_41999_ValidationEntity _validationForm { get; set; }
        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        private IArbeidsplasserValidator _arbeidsplasserValidator;
        private IEnkelAdresseValidator _tiltakshaverEnkelAdresseValidator;
        private IKontaktpersonValidator _kontaktpersonValidator;
        private IPartstypeValidator _partstypeValidator;
        private IAktoerValidator _tiltakshaverValidator;
        private IEnkelAdresseValidator _fakturamottakerEnkelAdresseValidator;
        private IFakturamottakerValidator _fakturamottakerValidator;

        protected override string XPathRoot => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke_41999_Validator(EntityValidatorOrchestrator entityValidatorOrchestrator, IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _entityValidatorOrchestrator = entityValidatorOrchestrator;
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
        }

        public override ValidationResult StartValidation(ValidationInput validationInput)
        {
            ArbeidstilsynetsSamtykkeType formModel = new ArbeidstilsynetsSamtykke_41999_Deserializer().Deserialize(validationInput.FormData);
            _validationForm = new ArbeidstilsynetsSamtykke_41999_Mapper().GetFormEntity(formModel, XPathRoot);
            
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

            //Fakturamottaker
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.FakturamottakerValidator));
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.FakturamottakerValidator));

            //Arbeidsplasser
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.ArbeidsplasserValidator));
        }

        protected override void InstantiateValidators()
        {
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(_entityValidatorOrchestrator, EntityValidatorEnum.EiendomByggestedValidator);
            _matrikkelValidator = new MatrikkelValidator(_entityValidatorOrchestrator, EntityValidatorEnum.EiendomByggestedValidator);
            _eiendomByggestedValidator = new EiendomByggestedValidator(_entityValidatorOrchestrator, _eiendomsAdresseValidator, _matrikkelValidator, _municipalityValidator);
            _arbeidsplasserValidator = new ArbeidsplasserValidator(_entityValidatorOrchestrator);
            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator);
            _kontaktpersonValidator = new KontaktpersonValidator(_entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator);
            _partstypeValidator = new PartstypeValidator(_entityValidatorOrchestrator, EntityValidatorEnum.TiltakshaverValidator, _codeListService);
            _tiltakshaverValidator = new AktoerValidator(_entityValidatorOrchestrator, AktoerEnum.tiltakshaver, _tiltakshaverEnkelAdresseValidator, _kontaktpersonValidator, _partstypeValidator, _codeListService);
            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorOrchestrator, EntityValidatorEnum.FakturamottakerValidator);
            _fakturamottakerValidator = new FakturamottakerValidator(_entityValidatorOrchestrator, _fakturamottakerEnkelAdresseValidator);

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

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidationResult = _arbeidsplasserValidator.Validate(_validationForm.ModelData.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var tiltakshaverValidationResult = _tiltakshaverValidator.Validate(_validationForm.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            var fakturamottakerValidationResult = _fakturamottakerValidator.Validate(_validationForm.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);

            //return ValidationResult;
        }
    }
}
