using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke
{
    public class EiendomByggestedMapper
    {
        public EiendomValidationEntity[] Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke.EiendomType[] eiendomByggesteder)
        {
            if (eiendomByggesteder == null) return null;
            var retVal = new List<EiendomValidationEntity>();

            for (int i = 0; i < eiendomByggesteder.Count(); i++)
            {
                var eiendomByggested = eiendomByggesteder[i];

                var eiendom = new EiendomValidationEntity
                {
                    Bolignummer = eiendomByggested.bolignummer,
                    Bygningsnummer = eiendomByggested.bygningsnummer,
                    Kommunenavn = eiendomByggested.kommunenavn,
                    Matrikkel = MatrikkelToByggestedMapper.Map(eiendomByggested.eiendomsidentifikasjon),
                    Adresse = EiendomAdresseMapper.Map(eiendomByggested.adresse)
                };

                retVal.Add(eiendom);
            }

            return retVal.ToArray();
        }
    }
}
