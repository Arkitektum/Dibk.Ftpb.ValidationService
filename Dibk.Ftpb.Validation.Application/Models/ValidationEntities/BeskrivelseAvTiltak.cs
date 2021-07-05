using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class BeskrivelseAvTiltakValidationEntity : ValidationEntityBase<BeskrivelseAvTiltak>
    {
        public BeskrivelseAvTiltakValidationEntity(BeskrivelseAvTiltak modelData, string xmlElementName, string parentEntityDataModelXpath = null)
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        { }
    }
    public class BeskrivelseAvTiltak
    {
        public FormaaltypeValidationEntity Formaaltype { get; set; }
        public string BRA { get; set; }
        public IEnumerable<KodelisteValidationEntity> Tiltakstype { get; set; }
    }
}
