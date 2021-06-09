using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;


namespace Dibk.Ftpb.Validation.Application.Logic.Deserializers
{
    public class ArbeidstilsynetsSamtykke_41999_Deserializer
    {
        public ArbeidstilsynetsSamtykke_41999_Deserializer()
        {
        }
        public ArbeidstilsynetsSamtykkeType Deserialize(string xmlData)
        {
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykkeType>(xmlData);
        }
    }
}
