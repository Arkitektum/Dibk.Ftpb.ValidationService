using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Eiendom
    {
        private EiendommensAdresse Adresse { get; set; }

        private Matrikkel Eiendomsidentifikasjon { get; set; }

        private string Bygningsnummer { get; set; }

        private string Bolignummer { get; set; }

        private string Kommunenavn { get; set; }
    }
}
