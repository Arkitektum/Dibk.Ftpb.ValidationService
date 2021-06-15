using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class FormaaltypeValidationEntity : ValidationEntityBase<Formaaltype>
    {
        public FormaaltypeValidationEntity(Formaaltype modelData, string xmlElementName, string parentEntityDataModelXpath = null)
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        { }
    }
    public class Formaaltype
    {
        public KodelisteValidationEntity Anleggstype { get; set; }
        public KodelisteValidationEntity Naeringsgruppe { get; set; }
        public KodelisteValidationEntity Bygningstype { get; set; }
        public IEnumerable<KodelisteValidationEntity> Tiltaksformaal { get; set; }
        public string BeskrivPlanlagtFormaal {  get; set; }
    }
}
