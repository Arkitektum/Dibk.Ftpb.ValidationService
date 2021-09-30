using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class BeskrivelseAvTiltakValidationEntity 
    {
        public FormaaltypeValidationEntity Formaaltype { get; set; }
        public string BRA { get; set; }
        public IEnumerable<KodelisteValidationEntity> Tiltakstype { get; set; }
    }
}
