using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class EiendomByggestedMapper
    {
        public EiendomValidationEntity[] Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.EiendomType[] eiendomByggesteder, string parentElementXpath = null)
        {
            if (eiendomByggesteder == null) return null;
            var propertyList = new List<EiendomValidationEntity>();

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


                propertyList.Add(eiendom);
            }

            return propertyList.ToArray();
        }
    }
}
