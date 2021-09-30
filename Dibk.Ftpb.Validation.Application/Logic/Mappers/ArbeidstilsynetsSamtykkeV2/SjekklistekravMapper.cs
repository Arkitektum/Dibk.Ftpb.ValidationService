using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;
using System.Linq;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykkeV2
{
    public class SjekklistekravMapper : ModelToValidationEntityMapper<no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.SjekklisteKravType[], IEnumerable<SjekklistekravValidationEntity>>
    {
        public override IEnumerable<SjekklistekravValidationEntity> Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.SjekklisteKravType[] sjekklistekraver, string parentElementXpath = null)
        {
            if (sjekklistekraver == null) return null;
            var retVal = new List<SjekklistekravValidationEntity>();

            for (int i = 0; i < sjekklistekraver.Count(); i++)
            {
                var sjekklistekrav = sjekklistekraver[i];
                
                var krav = new Sjekklistekrav()
                {
                    Sjekklistepunktsvar = sjekklistekrav.sjekklistepunktsvar,
                    Dokumentasjon = sjekklistekrav.dokumentasjon
                };

                var sjekklistekravValEntity = new SjekklistekravValidationEntity(krav, $"krav[{i}]", parentElementXpath);
                krav.Sjekklistepunkt = new SjekklistepunktMapper().Map(sjekklistekrav.sjekklistepunkt, sjekklistekravValEntity.DataModelXpath);

                retVal.Add(sjekklistekravValEntity);
            }

            return retVal;
        }
    }
}
