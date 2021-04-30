using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Logic.Mappers;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]

    public class ArbeidstilsynetsSamtykke2_45957_Validator : IFormValidator
    {
        private readonly IMunicipalityValidator _municipalityApiService;
        public ArbeidstilsynetsSamtykke2Form_45957 ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }

        public ArbeidstilsynetsSamtykke2_45957_Validator(IMunicipalityValidator municipalityApiService)
        {
            _municipalityApiService = municipalityApiService;
        }

        public void Execute(string xmlData)
        {
            _form = DeserializeDataForm(xmlData);
            ArbeidstilsynetsSamtykke2Form45957 = MapDataModelToFormEntity(_form);
            Validate("ArbeidstilsynetsSamtykkeType", ArbeidstilsynetsSamtykke2Form45957);
        }
        public ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        private ArbeidstilsynetsSamtykke2Form_45957 MapDataModelToFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            var formEntity = new ArbeidstilsynetsSamtykkeV2Dfv45957_Mapper().GetFormEntity(dataModel);

            return formEntity;
        }

        public List<ValidationMessage> Validate(string context, ArbeidstilsynetsSamtykke2Form_45957 form)
        {
            return new List<ValidationMessage>();
        }
    }
}
