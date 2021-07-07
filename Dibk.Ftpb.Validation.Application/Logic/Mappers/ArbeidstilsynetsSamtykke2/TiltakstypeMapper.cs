using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class TiltakstypeMapper : ModelToValidationEntityMapper<KodeType[], IEnumerable<KodelisteValidationEntity>>
    {
        public override IEnumerable<KodelisteValidationEntity> Map(KodeType[] mapFrom, string parentElementXpath = null)
        {
            if (mapFrom == null) return null;
            var retVal = new List<KodelisteValidationEntity>();

            for (int i = 0; i < mapFrom.Count(); i++)
            {
                var kode = mapFrom[i];
                if (kode ==null) break;

                var kodeliste = new Kodeliste()
                {
                    Kodebeskrivelse = kode.kodebeskrivelse,
                    Kodeverdi = kode.kodeverdi
                };

                var kodelisteValidationEntity = new KodelisteValidationEntity(kodeliste, $"type[{i}]", parentElementXpath);

                retVal.Add(kodelisteValidationEntity);
            }

            return retVal;
        }
    }
}