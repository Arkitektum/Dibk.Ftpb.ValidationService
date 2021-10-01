using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class SjekklistekravValidationEntity
    {
        public bool? Sjekklistepunktsvar { get; set; }
        public string Dokumentasjon { get; set; }
        //public SjekklistepunktValidationEntity Sjekklistepunkt { get; set; }
        public KodelisteValidationEntity Sjekklistepunkt { get; set; }
    }
}
