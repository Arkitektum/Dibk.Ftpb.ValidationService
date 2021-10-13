using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities
{
    public class ArbeidstilsynetsSamtykke2_45957_Form
    {

        public EiendomValidationEntity[] EiendomValidationEntities { get; set; }
        public ArbeidsplasserValidationEntity ArbeidsplasserValidationEntity { get; set; }
        public BetalingValidationEntity BetalingValidationEntity { get; set; }
        public AktoerValidationEntity TiltakshaverValidationEntity { get; set; }
        public AktoerValidationEntity AnsvarligSoekerValidationEntity { get; set; }
        public FakturamottakerValidationEntity FakturamottakerValidationEntity { get; set; }
        public SjekklistekravValidationEntity[] SjekklistekravValidationEntities { get; set; }
        public BeskrivelseAvTiltakValidationEntity BeskrivelseAvTiltakValidationEntity { get; set; }
        public MetadataValidationEntity MetadataValidationEntity { get; set; }
        public SaksnummerValidationEntity ArbeidstilsynetsSaksnummerValidationEntity { get; set; }
        public SaksnummerValidationEntity KommunensSaksnummerValidationEntity { get; set; }
        public SignaturValidationEntity SignaturValidationEntity { get; set; }
    }
}