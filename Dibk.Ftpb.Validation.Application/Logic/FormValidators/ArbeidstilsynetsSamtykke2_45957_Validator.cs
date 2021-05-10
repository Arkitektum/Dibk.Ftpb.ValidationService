using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Logic.Mappers;
using Dibk.Ftpb.Validation.Application.Logic.Deserializers;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]

    public class ArbeidstilsynetsSamtykke2_45957_Validator : IFormValidator
    {
        private readonly IMunicipalityValidator _municipalityApiService;
        public ArbeidstilsynetsSamtykke2Form_45957 ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }

        private readonly string context = "ArbeidstilsynetsSamtykke";
        public ArbeidstilsynetsSamtykke2_45957_Validator(IMunicipalityValidator municipalityApiService)
        {
            _municipalityApiService = municipalityApiService;
        }

        public ValidationResult StartValidation(string xmlData)
        {
            ValidationResult validationResponse = new();

            validationResponse.ValidationRules = new List<ValidationRule>();
            validationResponse.ValidationMessages = new List<ValidationMessage>();

            //TODO xsd validering
            var xsdValidationMesagges = new List<ValidationMessage>();
            validationResponse.ValidationMessages.AddRange(xsdValidationMesagges);

            //Get Arbeidstilsynets Samtykke v2 Dfv45957 class
            //_form = DeserializeDataForm(xmlData);
            _form = new ArbeidstilsynetsSamtykke2_45957_Deserializer().Deserialize(xmlData);

            // map to arbeidstilsynet formEntity 
            ArbeidstilsynetsSamtykke2Form45957 = MapDataModelToFormEntity(_form);
            validationResponse = Validate(context, ArbeidstilsynetsSamtykke2Form45957);
            //            validationMessages.AddRange(validationMessagesResult);

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

        public ValidationResult Validate(string context, ArbeidstilsynetsSamtykke2Form_45957 form)
        {
            List<Eiendom> eiendommer = new List<Eiendom>();
            eiendommer.Add(ArbeidstilsynetsSamtykke2Form45957.Eiendom);
            var validationResultForEiendom = new EiendomValidator().Validate(context, eiendommer);

            return validationResultForEiendom;
        }
    }
}
