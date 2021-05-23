using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class EiendomToByggestedMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType[], IEnumerable<Eiendom>>
    {
        public override IEnumerable<Eiendom> Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType[] eiendomByggesteder, ValidationEntityBase parentEntity = null)
        {
            if (eiendomByggesteder == null) return null;
            var retVal = new List<Eiendom>();

            for (int i = 0; i < eiendomByggesteder.Count(); i++)
            {
                var eiendomByggested = eiendomByggesteder[i];

                var eiendom = new Eiendom($"EiendomByggested[{i}]", parentEntity)
                {
                    Bolignummer = eiendomByggested.bolignummer,
                    Bygningsnummer = eiendomByggested.bygningsnummer,
                    Kommunenavn = eiendomByggested.kommunenavn
                };

                eiendom.Matrikkel = new MatrikkelToByggestedMapper().Map(eiendomByggested.eiendomsidentifikasjon, eiendom);
                eiendom.Adresse = new AdresseMapper().Map(eiendomByggested.adresse, eiendom);
                retVal.Add(eiendom);
            }

            return retVal;
        }
    }
}
