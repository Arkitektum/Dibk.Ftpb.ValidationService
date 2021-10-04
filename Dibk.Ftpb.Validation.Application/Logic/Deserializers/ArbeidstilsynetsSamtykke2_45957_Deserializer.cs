using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Deserializers
{
    public class ArbeidstilsynetsSamtykke2_45957_Deserializer
    {
        public ArbeidstilsynetsSamtykke2_45957_Deserializer()
        {
        }
        public ArbeidstilsynetsSamtykke2_45957_Form Deserialize(string xmlData)
        {
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke2_45957_Form>(xmlData);
        }
    }
}
