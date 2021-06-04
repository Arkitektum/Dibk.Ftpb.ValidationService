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
        public ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }

        public ArbeidstilsynetsSamtykke2_45957_Validator(EntityValidatorOrchestrator entityValidatorOrchestrator, IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _entityValidatorOrchestrator = entityValidatorOrchestrator;
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
        }

        public ValidationResult StartValidation(ValidationInput validationInput)
        {
            InitializeValidatorConfig();

            //Get Arbeidstilsynets Samtykke v2 Dfv45957 class
            _form = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);

            // map to arbeidstilsynet formEntity 
            ArbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(_form);
            Validate(ArbeidstilsynetsSamtykke2Form45957, validationInput);

            return ValidationResult;
        }

        private void InitializeValidatorConfig()
        {
            _entityValidatorOrchestrator.ValidatorFormName = "ArbeidstilsynetsSamtykke2_45957_Validator";

            _entityValidatorOrchestrator.Validators = new List<EntityValidatorInfo>();

            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EiendomValidator", Parent = null, ParentXPath = "ArbeidstilsynetsSamtykke" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EiendomsAdresseValidator", Parent = "EiendomValidator", ParentXPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "MatrikkelValidator", Parent = "EiendomValidator", ParentXPath = "ArbeidstilsynetsSamtykke/eiendomByggested{0}" });

            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "TiltakshaverValidator", Parent = null, ParentXPath = "ArbeidstilsynetsSamtykke" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EnkelAdresseValidator", Parent = "TiltakshaverValidator", ParentXPath = "ArbeidstilsynetsSamtykke/tiltakshaver" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "KontaktpersonValidator", Parent = "TiltakshaverValidator", ParentXPath = "ArbeidstilsynetsSamtykke/tiltakshaver" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "PartstypeValidator", Parent = "TiltakshaverValidator", ParentXPath = "ArbeidstilsynetsSamtykke/tiltakshaver" });

            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "FakturamottakerValidator", Parent = null, ParentXPath = "ArbeidstilsynetsSamtykke" });
            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "EnkelAdresseValidator", Parent = "FakturamottakerValidator", ParentXPath = "ArbeidstilsynetsSamtykke/fakturamottaker" });

            _entityValidatorOrchestrator.Validators.Add(new EntityValidatorInfo() { EntityValidator = "ArbeidsplasserValidator", Parent = null, ParentXPath = "ArbeidstilsynetsSamtykke" });
        }

        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            //TODO add error massages if.... :thinkingFace
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        public ValidationResult Validate(ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity form, ValidationInput validationInput)
        {
            //Parent xml element have the info to decide if sub element is an array or not
            //TODO: How to configure what version of and sub validator to use?
            
            //Validering av eiendom
            IEiendomsAdresseValidator eiendomsAdresseValidator = new EiendomsAdresseValidator(_entityValidatorOrchestrator);
            IMatrikkelValidator matrikkelValidator = new MatrikkelValidator(_entityValidatorOrchestrator);
            IEiendomValidator eiendomValidator = new EiendomValidator(_entityValidatorOrchestrator, eiendomsAdresseValidator, matrikkelValidator, _municipalityValidator);
            AccumulateValidationRules(eiendomValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(eiendomsAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(matrikkelValidator.ValidationResult.ValidationRules);
            var eiendomValidationResult = eiendomValidator.Validate(form.ModelData.EiendomValidationEntities);
            AccumulateValidationMessages(eiendomValidationResult.ValidationMessages);

            //Validering av arbeidsplasser
            var arbeidsplasserValidator = new ArbeidsplasserValidator(_entityValidatorOrchestrator);
            AccumulateValidationRules(arbeidsplasserValidator.ValidationResult.ValidationRules);
            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidationResult = arbeidsplasserValidator.Validate(form.ModelData.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            //Validering av tiltakshaver
            IEnkelAdresseValidator enkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorOrchestrator, "TiltakshaverValidator");
            IKontaktpersonValidator kontaktpersonValidator = new KontaktpersonValidator(_entityValidatorOrchestrator, "TiltakshaverValidator");
            IPartstypeValidator partstypeValidator = new PartstypeValidator(_entityValidatorOrchestrator, "TiltakshaverValidator", _codeListService);
            var tiltakshaverValidator = new TiltakshaverValidator(_entityValidatorOrchestrator, enkelAdresseValidator, kontaktpersonValidator, partstypeValidator, _codeListService);
            AccumulateValidationRules(tiltakshaverValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(partstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(enkelAdresseValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(kontaktpersonValidator.ValidationResult.ValidationRules);
            var tiltakshaverValidationResult = tiltakshaverValidator.Validate(form.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);

            //Validering av fakturamottaker
            enkelAdresseValidator = new EnkelAdresseValidator(_entityValidatorOrchestrator, "FakturamottakerValidator");
            IFakturamottakerValidator fakturamottakerValidator = new FakturamottakerValidator(_entityValidatorOrchestrator, enkelAdresseValidator);
            AccumulateValidationRules(fakturamottakerValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(enkelAdresseValidator.ValidationResult.ValidationRules);
            var fakturamottakerValidationResult = fakturamottakerValidator.Validate(form.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);

            return ValidationResult;
        }
    }
}
