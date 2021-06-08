using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities
{
    public class ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity : ValidationEntityBase<ArbeidstilsynetsSamtykke2_45957_Form>
    {
        public ArbeidstilsynetsSamtykke2Form_45957_ValidationEntity(ArbeidstilsynetsSamtykke2_45957_Form modelData, string xmlElementName, string parentEntityDataModelXpath = null) : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class ArbeidstilsynetsSamtykke2_45957_Form
    {

        public IEnumerable<EiendomValidationEntity> EiendomValidationEntities { get; set; }
        public ArbeidsplasserValidationEntity ArbeidsplasserValidationEntity { get; set; }
        public AktoerValidationEntity TiltakshaverValidationEntity { get; set; }
        public FakturamottakerValidationEntity FakturamottakerValidationEntity { get; set; }
    }
}