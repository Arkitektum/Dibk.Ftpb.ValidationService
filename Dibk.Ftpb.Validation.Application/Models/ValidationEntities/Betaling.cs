using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class BetalingValidationEntity 
    {
        public bool? SkalFaktureres { get; set; }
        public string Beskrivelse { get; set; }
        public string OrdreId { get; set; }
        public string TransId { get; set; }
        public string GebyrKategori { get; set; }
        public string Sum { get; set; }

    }
}
