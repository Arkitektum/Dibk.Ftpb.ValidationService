using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

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
