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
        private readonly FormValidatorConfiguration _formValidatorConfiguration;
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


        private AnleggstypeValidator _anleggstypeValidator;
        private NaeringsgruppeValidator _naeringsgruppeValidator;
        private BygningstypeValidator _bygningstypeValidator;
        private TiltaksformaalValidator _tiltaksformaalValidator;
        private FormaaltypeValidator _formaaltypeValidator;
        private TiltakstypeValidator _tiltakstypeValidator;
        private IBeskrivelseAvTiltakValidator _beskrivelseAvTiltakValidator;


        protected override string XPathRoot => "ArbeidstilsynetsSamtykke";

        public ArbeidstilsynetsSamtykke2_45957_Validator(FormValidatorConfiguration formValidatorConfiguration, IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _formValidatorConfiguration = formValidatorConfiguration;
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
            _formValidatorConfiguration.ValidatorFormName = this.GetType().Name;
            _formValidatorConfiguration.FormXPathRoot = XPathRoot;

            _formValidatorConfiguration.Validators = new List<EntityValidatorInfo>();

            //Eiendombyggested
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomByggestedValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EiendomsAdresseValidator, EntityValidatorEnum.EiendomByggestedValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.MatrikkelValidator, EntityValidatorEnum.EiendomByggestedValidator));

            //Tiltakshaver
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltakshaverValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.TiltakshaverValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.KontaktpersonValidator, EntityValidatorEnum.TiltakshaverValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.PartstypeValidator, EntityValidatorEnum.TiltakshaverValidator));

            //AnsvarligSoeker
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.AnsvarligSoekerValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidator, EntityValidatorEnum.AnsvarligSoekerValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.KontaktpersonValidator, EntityValidatorEnum.AnsvarligSoekerValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.PartstypeValidator, EntityValidatorEnum.AnsvarligSoekerValidator));

            //Fakturamottaker
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.FakturamottakerValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.EnkelAdresseValidatorV2, EntityValidatorEnum.FakturamottakerValidator));

            //Arbeidsplasser
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.ArbeidsplasserValidator));

            //Sjekkliste
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.SjekklistekravValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.SjekklistepunktValidator, EntityValidatorEnum.SjekklistekravValidator));

            //BeskrivelseAvTiltak
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.AnleggstypeValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.NaeringsgruppeValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.BygningstypeValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltaksformaalValidator, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));
            _formValidatorConfiguration.Validators.Add(new EntityValidatorInfo(EntityValidatorEnum.TiltakstypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator));

        }

        protected override void InstantiateValidators()
        {
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(_formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);
            _matrikkelValidator = new MatrikkelValidator(_formValidatorConfiguration, EntityValidatorEnum.EiendomByggestedValidator);
            _eiendomByggestedValidator = new EiendomByggestedValidator(_formValidatorConfiguration, _eiendomsAdresseValidator, _matrikkelValidator, _municipalityValidator);

            _arbeidsplasserValidator = new ArbeidsplasserValidator(_formValidatorConfiguration);

            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(_formValidatorConfiguration, EntityValidatorEnum.TiltakshaverValidator);
            _tiltakshaverKontaktpersonValidator = new KontaktpersonValidator(_formValidatorConfiguration, EntityValidatorEnum.TiltakshaverValidator);
            _tiltakshaverPartstypeValidator = new PartstypeValidator(_formValidatorConfiguration, EntityValidatorEnum.TiltakshaverValidator, _codeListService);
            _tiltakshaverValidator = new AktoerValidator(_formValidatorConfiguration, AktoerEnum.tiltakshaver, _tiltakshaverEnkelAdresseValidator, _tiltakshaverKontaktpersonValidator, _tiltakshaverPartstypeValidator, _codeListService);

            _ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(_formValidatorConfiguration, EntityValidatorEnum.AnsvarligSoekerValidator);
            _ansvarligSoekerKontaktpersonValidator = new KontaktpersonValidator(_formValidatorConfiguration, EntityValidatorEnum.AnsvarligSoekerValidator);
            _ansvarligSoekerPartstypeValidator = new PartstypeValidator(_formValidatorConfiguration, EntityValidatorEnum.AnsvarligSoekerValidator, _codeListService);
            _ansvarligSoekerValidator = new AktoerValidator(_formValidatorConfiguration, AktoerEnum.ansvarligSoeker, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);

            _fakturamottakerEnkelAdresseValidator = new EnkelAdresseValidatorV2(_formValidatorConfiguration, EntityValidatorEnum.FakturamottakerValidator);
            _fakturamottakerValidator = new FakturamottakerValidator(_formValidatorConfiguration, _fakturamottakerEnkelAdresseValidator);

            _sjekklistepunktValidator = new SjekklistepunktValidator(_formValidatorConfiguration, EntityValidatorEnum.SjekklistekravValidator);
            _sjekklistekravValidator = new SjekklistekravValidator(_formValidatorConfiguration, _sjekklistepunktValidator);

            _anleggstypeValidator = new AnleggstypeValidator(_formValidatorConfiguration, EntityValidatorEnum.FormaaltypeValidator, EntityValidatorEnum.BeskrivelseAvTiltakValidator, _codeListService);
            _naeringsgruppeValidator = new NaeringsgruppeValidator(_formValidatorConfiguration, EntityValidatorEnum.FormaaltypeValidator, _codeListService);
            _bygningstypeValidator = new BygningstypeValidator(_formValidatorConfiguration, EntityValidatorEnum.FormaaltypeValidator, _codeListService);
            _tiltaksformaalValidator = new TiltaksformaalValidator(_formValidatorConfiguration, EntityValidatorEnum.FormaaltypeValidator, _codeListService);
            _formaaltypeValidator = new FormaaltypeValidator(_formValidatorConfiguration,
                                                             EntityValidatorEnum.BeskrivelseAvTiltakValidator,
                                                             _anleggstypeValidator, _codeListService,
                                                             _naeringsgruppeValidator, _codeListService,
                                                             _bygningstypeValidator, _codeListService,
                                                             _tiltaksformaalValidator, _codeListService);
            _tiltakstypeValidator = new TiltakstypeValidator(_formValidatorConfiguration, EntityValidatorEnum.BeskrivelseAvTiltakValidator, _codeListService);
            _beskrivelseAvTiltakValidator = new BeskrivelseAvTiltakValidator(_formValidatorConfiguration, _formaaltypeValidator, _tiltakstypeValidator);



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

            AccumulateValidationRules(_beskrivelseAvTiltakValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_formaaltypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_anleggstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_naeringsgruppeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_bygningstypeValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltaksformaalValidator.ValidationResult.ValidationRules);
            AccumulateValidationRules(_tiltakstypeValidator.ValidationResult.ValidationRules);


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
