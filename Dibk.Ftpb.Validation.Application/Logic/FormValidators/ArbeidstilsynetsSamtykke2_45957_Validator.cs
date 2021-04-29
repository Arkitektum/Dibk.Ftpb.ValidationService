using Dibk.Ftpb.Validation.Application.Logic.FormValidators.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.FormValidators
{
    [FormData(DataFormatVersion = "45957")]

    public class ArbeidstilsynetsSamtykke2_45957_Validator : IFormValidator
    {
        internal ArbeidstilsynetsSamtykkeType form;
        public void DeserializeToDatamodel(string xmlData)
        {
            //form = desierialize xmlData
            //throw new NotImplementedException();
        }

        public List<ValidationMessage> Validate(string context, ArbeidstilsynetsSamtykke2Form_45957 form)
        {
            return new List<ValidationMessage>();
        }
    }
}
