using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities
{
    public class ArbeidstilsynetsSamtykke_41999_Form
    {
        public EiendomValidationEntity[] EiendomValidationEntities { get; set; }
        public ArbeidsplasserValidationEntity ArbeidsplasserValidationEntity { get; set; }
        public AktoerValidationEntity TiltakshaverValidationEntity { get; set; }
        public AktoerValidationEntity AnsvarligSoekerValidationEntity { get; set; }
        public FakturamottakerValidationEntity FakturamottakerValidationEntity { get; set; }
    }
}
