using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]

    public class ArbeidstilsynetsSamtykke2_45957_Validator : IFormValidator
    {
        private readonly IMunicipalityApiService _municipalityApiService;
        public ArbeidstilsynetsSamtykke2Form_45957 ArbeidstilsynetsSamtykke2Form45957 { get; set; }
        public ArbeidstilsynetsSamtykkeType _form { get; set; }

        public ArbeidstilsynetsSamtykke2_45957_Validator(IMunicipalityApiService municipalityApiService)
        {
            _municipalityApiService = municipalityApiService;
        }

        public void Execute(string xmlData)
        {
            _form = DeserializeDataForm(xmlData);
            ArbeidstilsynetsSamtykke2Form45957 = MapDataModelToFormEntity(_form);
            Validate("ArbeidstilsynetsSamtykkeType", ArbeidstilsynetsSamtykke2Form45957);
        }
        private ArbeidstilsynetsSamtykkeType DeserializeDataForm(string xmlData)
        {
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }

        public ArbeidstilsynetsSamtykke2Form_45957 MapDataModelToFormEntity(ArbeidstilsynetsSamtykkeType dataModel)
        {
            return new ArbeidstilsynetsSamtykke2Form_45957();
        }

        public List<ValidationMessage> Validate(string context, ArbeidstilsynetsSamtykke2Form_45957 form)
        {

            return new List<ValidationMessage>();
        }


    }
}
