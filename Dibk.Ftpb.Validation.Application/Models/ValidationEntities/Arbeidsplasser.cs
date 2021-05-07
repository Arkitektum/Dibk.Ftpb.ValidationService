using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Arbeidsplasser
    {
        public bool? Framtidige { get; set; }
        public bool? Faste { get; set; }
        public bool? Midlertidige { get; set; }
        public string AntallAnsatte { get; set; }
        public bool? Eksisterende { get; set; }
        public bool? UtleieBygg { get; set; }
        public string AntallVirksomheter { get; set; }
        public string Beskrivelse { get; set; }
    }
}