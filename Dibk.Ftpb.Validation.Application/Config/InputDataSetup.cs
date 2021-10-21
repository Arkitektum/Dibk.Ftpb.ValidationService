using Dibk.Ftpb.Validation.Application.Constants;
using Dibk.Ftpb.Validation.Application.Models;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Config
{
    public class InputDataSetup
    {
        public static readonly List<InputDataConfig> Config = new()
        {
            new InputDataConfig(DataType.ArbeidstilsynetsSamtykke, "5547", "41999", typeof(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke.ArbeidstilsynetsSamtykkeType)),
            new InputDataConfig(DataType.ArbeidstilsynetsSamtykke2, "6821", "45957", typeof(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.ArbeidstilsynetsSamtykkeType)),
            new InputDataConfig(DataType.AnsvarsrettAnsako, "10000", "1", typeof(no.kxml.skjema.dibk.ansvarsrettAnsako.ErklaeringAnsvarsrettType)),
        };
    }
}
