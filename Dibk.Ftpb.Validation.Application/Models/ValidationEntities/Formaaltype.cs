using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class FormaaltypeValidationEntity 
    {
        public KodelisteValidationEntity Anleggstype { get; set; }
        public KodelisteValidationEntity Naeringsgruppe { get; set; }
        public KodelisteValidationEntity Bygningstype { get; set; }
        public KodelisteValidationEntity[] Tiltaksformaal { get; set; }
        public string BeskrivPlanlagtFormaal {  get; set; }
    }
}
