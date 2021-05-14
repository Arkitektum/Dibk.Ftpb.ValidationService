using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Aktoer
    {
        public PartstypeCode Partstype { get; set; }

        public string Foedselsnummer { get; set; }

        public string Organisasjonsnummer { get; set; }

        public string Navn { get; set; }

        public EnkelAdresse Adresse { get; set; }

        public string Telefonnummer { get; set; }

        public string Mobilnummer { get; set; }

        public string Epost { get; set; }

        public Kontaktperson Kontaktperson { get; set; }
    }
}
