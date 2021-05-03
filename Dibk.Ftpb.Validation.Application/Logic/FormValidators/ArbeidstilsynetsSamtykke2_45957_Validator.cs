﻿using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Logic.Mappers;
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

        public List<ValidationRule> StartValidation(string xmlData)
        {
            var validationMessages = new List<ValidationRule>();
            
            //TODO xsd validering
            var xsdValidationMesagges = new List<ValidationRule>();
            validationMessages.Concat(xsdValidationMesagges);

            //Get Arbeidstilsynets Samtykke v2 Dfv45957 class
            _form = DeserializeDataForm(xmlData);

            // map to arbeidstilsynet formEntity 
            ArbeidstilsynetsSamtykke2Form45957 = MapDataModelToFormEntity(_form);

            var validationMessagesResult = Validate(context, ArbeidstilsynetsSamtykke2Form45957);
            validationMessages.Concat(validationMessagesResult);
            
            return validationMessages;
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

        public List<ValidationRule> Validate(string context, ArbeidstilsynetsSamtykke2Form_45957 form)
        {

            var validationMAssegeForEindom = new EiendomValidator().Validate(context, new List<Eiendom>() { ArbeidstilsynetsSamtykke2Form45957.Eiendom });

            return new List<ValidationRule>();
        }


    }
}
