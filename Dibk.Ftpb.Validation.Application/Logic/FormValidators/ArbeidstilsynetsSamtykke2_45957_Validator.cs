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
        private TiltakshaverValidatorLogic _tiltakshaverValidatorLogic;
        private FakturamottakerValidatorLogic _fakturamottakerValidatorLogic;
        private ArbeidsplasserValidatorLogic _arbeidsplasserValidatorLogic;
        private AnsvarligSoekerValidatorLogic _ansvarligSoekerValidatorLogic;
        private SjekklistekravValidatorLogic _sjekklistekravValidatorLogic;

        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        //private IKodelisteValidator _sjekklistepunktValidator;

        private IBeskrivelseAvTiltakValidator _beskrivelseAvTiltakValidator;
        private IAktoerValidator _tiltakshaverValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        private IAktoerValidator _ansvarligSoekerValidator;
        private IFakturamottakerValidator _fakturamottakerValidator;
        private ISjekklistekravValidator _sjekklistekravValidator;
        private IArbeidsplasserValidator _arbeidsplasserValidator;



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

            _eiendombyggestedLogic = new EiendombyggestedLogic(1, _municipalityValidator);
            _tiltakshaverValidatorLogic = new TiltakshaverValidatorLogic(_eiendombyggestedLogic.LastNodeNumber + 1, _codeListService, _postalCodeService);
            _fakturamottakerValidatorLogic = new FakturamottakerValidatorLogic(_tiltakshaverValidatorLogic.LastNodeNumber + 1, _postalCodeService);
            _arbeidsplasserValidatorLogic = new ArbeidsplasserValidatorLogic(_fakturamottakerValidatorLogic.LastNodeNumber + 1);
            _ansvarligSoekerValidatorLogic = new AnsvarligSoekerValidatorLogic(_arbeidsplasserValidatorLogic.LastNodeNumber + 1, _codeListService, _postalCodeService);
            _beskrivelseAvTiltakValidatorLogic = new BeskrivelseAvTiltakValidatorLogic(_ansvarligSoekerValidatorLogic.LastNodeNumber + 1, _codeListService);
            _sjekklistekravValidatorLogic = new SjekklistekravValidatorLogic(_beskrivelseAvTiltakValidatorLogic.LastNodeNumber + 1);
        }

        protected override void InstantiateValidators()
        {
            //BeskrivelseAvTiltak
            _beskrivelseAvTiltakValidator = _beskrivelseAvTiltakValidatorLogic.Validator;
            //Eiendombyggested
            _eiendomByggestedValidator = _eiendombyggestedLogic.Validator;
            //Tiltakshaver
            _tiltakshaverValidator = _tiltakshaverValidatorLogic.Validator;
            //Fakturamottaker
            _fakturamottakerValidator = _fakturamottakerValidatorLogic.Validator;
            //Arbeidsplasser
            _arbeidsplasserValidator = _arbeidsplasserValidatorLogic.Validator;
            //AnsvarligSoeker
            _ansvarligSoekerValidator = _ansvarligSoekerValidatorLogic.Validator;
            //Sjekklistekr
            _sjekklistekravValidator = _sjekklistekravValidatorLogic.Validator;
        }

        protected override void DefineValidationRules()
        {
            AccumulateValidationRules(_beskrivelseAvTiltakValidatorLogic.ValidationRules);
            AccumulateValidationRules(_eiendombyggestedLogic.ValidationRules);
            AccumulateValidationRules(_tiltakshaverValidatorLogic.ValidationRules);
            AccumulateValidationRules(_fakturamottakerValidatorLogic.ValidationRules);
            AccumulateValidationRules(_arbeidsplasserValidatorLogic.ValidationRules);
            AccumulateValidationRules(_ansvarligSoekerValidatorLogic.ValidationRules);
            AccumulateValidationRules(_sjekklistekravValidatorLogic.ValidationRules);
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
