using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Logic.Deserializers
{
    public class ArbeidstilsynetsSamtykke2_45957_Deserializer
    {
        public ArbeidstilsynetsSamtykke2_45957_Deserializer()
        {
        }
        public ArbeidstilsynetsSamtykkeType Deserialize(string xmlData)
        {
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }
    }
}
