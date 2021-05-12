using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Logic.Mappers;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]

    public class ArbeidstilsynetsSamtykke2_45957_Validator : IFormValidator
    {
        private readonly IMunicipalityValidator _municipalityValidator;
        public ArbeidstilsynetsSamtykke2Form_45957 ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }

        private ValidationResult FormValidationResult;

        private readonly string _xPath = "ArbeidstilsynetsSamtykke";
        public ArbeidstilsynetsSamtykke2_45957_Validator(IMunicipalityValidator municipalityValidator)
        {
            _municipalityValidator = municipalityValidator;
        }

        public ValidationResult StartValidation(ValidationInput validationInput)
        {
            ValidationResult validationResponse = new();

            validationResponse.ValidationRules = new List<ValidationRule>();
            validationResponse.ValidationMessages = new List<ValidationMessage>();

            //Get Arbeidstilsynets Samtykke v2 Dfv45957 class
            _form = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(validationInput.FormData);

            // map to arbeidstilsynet formEntity 
            ArbeidstilsynetsSamtykke2Form45957 = MapDataModelToFormEntity(_form);
            validationResponse = Validate(_xPath, ArbeidstilsynetsSamtykke2Form45957,validationInput);

            return validationResponse;
        }
        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            //TODO add error massages if.... :thinkingFace
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        private ArbeidstilsynetsSamtykke2Form_45957 MapDataModelToFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var formEntity = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataModel);
            return formEntity;
        }

        public ValidationResult Validate(string xPath, ArbeidstilsynetsSamtykke2Form_45957 form, ValidationInput validationInput)
        {
            List<Eiendom> eiendommer = new List<Eiendom>();
            var eiendomValidator = new EiendomValidator($"{xPath}/eiendomByggested{{0}}", _municipalityValidator);

            //ValidationResult validationResult = new();
            foreach (var eiendom in form.Eiendommer)
            {
                int index = form.Eiendommer.IndexOf(eiendom);
                var eiendomValidationResult = eiendomValidator.Validate($"{xPath}/eiendomByggested[{index}]", eiendom);
                UpdateValidationResult(eiendomValidationResult);
            }

            var arbeidsplasser = new ArbeidsplasserValidator();

            var attachments = Helpers.ObjectIsNullOrEmpty(validationInput.Attachments) ? null : validationInput.Attachments.Select(a => a.AttachmentTypeName).ToList();
            
           var arbeidsplasserValidator= arbeidsplasser.Validate(_xPath, form.Arbeidsplasser, attachments);
           UpdateValidationResult(arbeidsplasserValidator);

            return FormValidationResult;
        }

        internal void UpdateValidationResult(ValidationResult validationResult)
        {
            FormValidationResult ??= new ValidationResult();
            FormValidationResult.ValidationRules ??= new List<ValidationRule>();
            FormValidationResult.ValidationMessages ??= new List<ValidationMessage>();

            var whereNotAlreadyExists = validationResult.ValidationRules.Where(x => FormValidationResult.ValidationRules.All(y => y.Xpath != x.Xpath));
            FormValidationResult.ValidationRules.AddRange(whereNotAlreadyExists);
            FormValidationResult.ValidationMessages.AddRange(validationResult.ValidationMessages);
        }
    }
}
