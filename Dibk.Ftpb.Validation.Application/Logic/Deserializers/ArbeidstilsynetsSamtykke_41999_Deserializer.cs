using Dibk.Ftpb.Validation.Application.Models.FormEntities;
using Dibk.Ftpb.Validation.Application.Utils;


namespace Dibk.Ftpb.Validation.Application.Logic.Deserializers
{
    public class ArbeidstilsynetsSamtykke_41999_Deserializer
    {
        public ArbeidstilsynetsSamtykke_41999_Deserializer()
        {
        }
        public ArbeidstilsynetsSamtykke_41999_Form Deserialize(string xmlData)
        {
            return SerializeUtil.DeserializeFromString<ArbeidstilsynetsSamtykke_41999_Form>(xmlData); ;
        }
    }
}
