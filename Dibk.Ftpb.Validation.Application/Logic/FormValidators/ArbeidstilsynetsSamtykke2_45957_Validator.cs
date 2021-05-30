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
        private readonly IMunicipalityValidator _municipalityValidator;
        private readonly ICodeListService _codeListService;
        public ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }


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

            return ValidationResult;
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

            var eiendomValidator = new EiendomValidator($"{form.DataModelXpath}/eiendomByggested{{0}}", _municipalityValidator);
            AccumulateValidationRules(eiendomValidator.ValidationResult.ValidationRules);

            var eiendomValidationResult = eiendomValidator.Validate(form.ModelData.EiendomValidationEntities);
            AccumulateValidationMessages(eiendomValidationResult.ValidationMessages);

            var arbeidsplasserValidator = new ArbeidsplasserValidator($"{form.DataModelXpath}/arbeidsplasser");
            AccumulateValidationRules(arbeidsplasserValidator.ValidationResult.ValidationRules);
            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            var arbeidsplasserValidationResult = arbeidsplasserValidator.Validate(form.ModelData.ArbeidsplasserValidationEntity, attachments);
            AccumulateValidationMessages(arbeidsplasserValidationResult.ValidationMessages);

            var tiltakshaverValidator = new TiltakshaverValidator($"{form.DataModelXpath}/tiltakshaver", _codeListService);
            AccumulateValidationRules(tiltakshaverValidator.ValidationResult.ValidationRules);
            var tiltakshaverValidationResult = tiltakshaverValidator.Validate(form.ModelData.TiltakshaverValidationEntity);
            AccumulateValidationMessages(tiltakshaverValidationResult.ValidationMessages);


            var fakturamottakerValidator = new FakturamottakerValidator($"{form.DataModelXpath}/fakturamottaker");
            AccumulateValidationRules(fakturamottakerValidator.ValidationResult.ValidationRules);
            var fakturamottakerValidationResult = fakturamottakerValidator.Validate(form.ModelData.FakturamottakerValidationEntity);
            AccumulateValidationMessages(fakturamottakerValidationResult.ValidationMessages);

            return ValidationResult;
        }
    }
}
