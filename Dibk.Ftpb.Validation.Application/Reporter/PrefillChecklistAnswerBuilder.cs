using Dibk.Ftpb.Validation.Application.Logic.Mappers.ArbeidstilsynetsSamtykke2;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Models.Web;
using no.kxml.skjema.dibk.arbeidstilsynetsSamtykke2;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class PrefillChecklistAnswerBuilder
    {
        //public IEnumerable<SjekklistekravValidationEntity> _sjekklistekravValidationEntities;
        public static PrefillChecklist Build(SjekklisteKravType[] alleKrav, ValidationInput validationInput)
        {
            var sjekklisteKrav = new List<ChecklistAnswer>();

            //IEnumerable<SjekklistekravValidationEntity> sjekklistekravValidationEntities = new List<SjekklistekravValidationEntity>();
            var sjekklistekravValidationEntities = new SjekklistekravMapper().Map(alleKrav, "krav");
            
            foreach (var krav in alleKrav)
            {
                sjekklisteKrav.Add(new ChecklistAnswer() { ChecklistReference = krav.sjekklistepunkt.kodeverdi, ChecklistQuestion = krav.sjekklistepunkt.kodebeskrivelse, yesNo = (bool)krav.sjekklistepunktsvar, SupportingDataXpathField = "krav/sjekklistekrav", Documentation = krav.dokumentasjon });
            }

            return new PrefillChecklist() { ChecklistAnswers = sjekklisteKrav };
        }
    }
}
