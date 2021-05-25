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
    public class ArbeidstilsynetsSamtykke2_45957_Validator : IFormValidator
    {
        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        public ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }

        private ValidationResult _validationResult;

        //private readonly string _xPath = "ArbeidstilsynetsSamtykke";
        public ArbeidstilsynetsSamtykke2_45957_Validator(IMunicipalityValidator municipalityValidator, ICodeListService codeListService)
        {
            _municipalityValidator = municipalityValidator;
            _codeListService = codeListService;
        }

        public ValidationResult StartValidation(ValidationInput validationInput)
        {
            //Get Arbeidstilsynets Samtykke v2 Dfv45957 class
            _form = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);

            // map to arbeidstilsynet formEntity 
            ArbeidstilsynetsSamtykke2Form45957 = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(_form);
            Validate(ArbeidstilsynetsSamtykke2Form45957, validationInput);

            return _validationResult;
        }
        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            //TODO add error massages if.... :thinkingFace
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        public ValidationResult Validate(ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity form, ValidationInput validationInput)
        {
            var eiendomValidator = new EiendomValidator(_municipalityValidator);

            foreach (var eiendom in form.ModelData.Eiendommer)
            {
                int index = form.ModelData.Eiendommer.ToList().IndexOf(eiendom);
                var eiendomValidationResult = eiendomValidator.Validate(eiendom);
                UpdateValidationResult(eiendomValidationResult);
            }

            var arbeidsplasser = new ArbeidsplasserValidator();

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidator = arbeidsplasser.Validate(form.ModelData.Arbeidsplasser, attachments);
            UpdateValidationResult(arbeidsplasserValidator);

            var tiltakshaverResult = new TiltakshaverValidator(_codeListService).Validate(form.ModelData.Tiltakshaver);
            UpdateValidationResult(tiltakshaverResult);

            var fakturamottakerResult = new FakturamottakerValidator().Validate(form.ModelData.Fakturamottaker);
            UpdateValidationResult(fakturamottakerResult);

            return _validationResult;
        }

        internal void UpdateValidationResult(ValidationResult validationResult)
        {
            _validationResult ??= new ValidationResult();
            _validationResult.ValidationRules ??= new List<ValidationRule>();
            _validationResult.ValidationMessages ??= new List<ValidationMessage>();

            var whereNotAlreadyExists = validationResult.ValidationRules.Where(x => _validationResult.ValidationRules.All(y => y.Xpath != x.Xpath));
            _validationResult.ValidationRules.AddRange(whereNotAlreadyExists);
            _validationResult.ValidationMessages.AddRange(validationResult.ValidationMessages);
        }
    }
}
