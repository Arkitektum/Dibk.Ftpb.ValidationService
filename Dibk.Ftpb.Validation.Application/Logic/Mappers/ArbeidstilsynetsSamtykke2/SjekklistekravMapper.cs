using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2
{
    public class SjekklistekravMapper
    {
        public SjekklistekravValidationEntity[] Map(no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2.SjekklisteKravType[] sjekklistekraver)
        {
            if (sjekklistekraver == null) return null;
            var retVal = new List<SjekklistekravValidationEntity>();

            for (int i = 0; i < sjekklistekraver.Count(); i++)
            {
                var sjekklistekrav = sjekklistekraver[i];

                var krav = new SjekklistekravValidationEntity
                {
                    Sjekklistepunktsvar = sjekklistekrav.sjekklistepunktsvar,
                    Dokumentasjon = sjekklistekrav.dokumentasjon,
                    Sjekklistepunkt = KodelisteValidationEntityMapper.Map(sjekklistekrav.sjekklistepunkt)
                };
                retVal.Add(krav);
            }

            return retVal.ToArray();
        }
    }
}
