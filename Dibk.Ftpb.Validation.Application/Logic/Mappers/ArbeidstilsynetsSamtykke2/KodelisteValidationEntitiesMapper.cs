using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class KodelisteValidationEntitiesMapper
    {
        public static KodelisteValidationEntity[] Map(KodeType[] mapFrom)
        {
            if (mapFrom == null) return null;
            var retVal = new List<KodelisteValidationEntity>();

            for (int i = 0; i < mapFrom.Count(); i++)
            {
                var kode = mapFrom[i];

                var kodeliste = new KodelisteValidationEntity()
                {
                    Kodebeskrivelse = kode.kodebeskrivelse,
                    Kodeverdi = kode.kodeverdi
                };
                retVal.Add(kodeliste);
            }

            return retVal.ToArray();
        }
    }
}
