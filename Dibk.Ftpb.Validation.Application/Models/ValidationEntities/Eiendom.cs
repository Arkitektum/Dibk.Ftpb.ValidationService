using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Eiendom
    {
        public EiendomsAdresse Adresse { get; set; }
        public Matrikkel Eiendomsidentifikasjon { get; set; }
        public string Bygningsnummer { get; set; }
        public string Bolignummer { get; set; }
        public string Kommunenavn { get; set; }
    }
}
