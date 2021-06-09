using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities
{
    public class ArbeidstilsynetsSamtykke2_45957_Form
    {

        public IEnumerable<EiendomValidationEntity> EiendomValidationEntities { get; set; }
        public ArbeidsplasserValidationEntity ArbeidsplasserValidationEntity { get; set; }
        public AktoerValidationEntity TiltakshaverValidationEntity { get; set; }
        public FakturamottakerValidationEntity FakturamottakerValidationEntity { get; set; }
        public IEnumerable<SjekklistekravValidationEntity> SjekklistekravValidationEntities { get; set; }
    }
}