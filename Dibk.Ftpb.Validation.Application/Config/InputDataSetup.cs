using Dibk.Ftpb.Validation.Application.Constants;
using Dibk.Ftpb.Validation.Application.Models;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Config
{
    public class InputDataSetup
    {
        public static readonly List<InputDataConfig> Config = new()
        {
            { new InputDataConfig(DataType.ArbeidstilsynetsSamtykke, "6821", "45957", typeof(ArbeidstilsynetsSamtykkeType)) }
        };
    }
}
