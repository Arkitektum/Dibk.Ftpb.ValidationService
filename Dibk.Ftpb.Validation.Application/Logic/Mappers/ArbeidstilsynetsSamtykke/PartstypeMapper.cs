using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
   public class PartstypeMapper
    {
        public static KodelisteValidationEntity Map(KodeType mapFrom)
        {
            KodelisteValidationEntity partstypeCode = null;
            if (mapFrom != null)
                partstypeCode = new KodelisteValidationEntity()
                {
                    Kodebeskrivelse = mapFrom.kodebeskrivelse,
                    Kodeverdi = mapFrom.kodeverdi
                };
            return partstypeCode;
        }
    }
}
