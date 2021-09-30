using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class EiendomByggestedMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType[], IEnumerable<EiendomValidationEntity>>
    {
        public override IEnumerable<EiendomValidationEntity> Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType[] eiendomByggesteder, string parentElementXpath = null)
        {
            if (eiendomByggesteder == null) return null;
            var retVal = new List<EiendomValidationEntity>();

            for (int i = 0; i < eiendomByggesteder.Count(); i++)
            {
                var eiendomByggested = eiendomByggesteder[i];
                
                var eiendom = new Eiendom()
                {
                    Bolignummer = eiendomByggested.bolignummer,
                    Bygningsnummer = eiendomByggested.bygningsnummer,
                    Kommunenavn = eiendomByggested.kommunenavn
                };

                var eiendomValEntity = new EiendomValidationEntity(eiendom, $"eiendomByggested[{i}]", parentElementXpath);
                eiendom.Matrikkel = new MatrikkelToByggestedMapper().Map(eiendomByggested.eiendomsidentifikasjon, eiendomValEntity.DataModelXpath);
                eiendom.Adresse = new EiendomAdresseMapper().Map(eiendomByggested.adresse, eiendomValEntity.DataModelXpath);

                retVal.Add(eiendomValEntity);
            }

            return retVal;
        }
    }
}
