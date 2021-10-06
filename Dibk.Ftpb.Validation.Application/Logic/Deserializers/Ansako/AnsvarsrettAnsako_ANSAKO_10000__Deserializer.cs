using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Utils;
using no.kxml.skjema.dibk.ansvarsrettAnsako;

namespace Dibk.Ftpb.Validation.Application.Logic.Deserializers.Ansako
{
   public class AnsvarsrettAnsako_ANSAKO_10000__Deserializer
    {
        public AnsvarsrettAnsako_ANSAKO_10000__Deserializer()
        {
        }
        public ErklaeringAnsvarsrettType Deserialize(string xmlData)
        {
            return SerializeUtil.DeserializeFromString<ErklaeringAnsvarsrettType>(xmlData);
        }
    }
}
