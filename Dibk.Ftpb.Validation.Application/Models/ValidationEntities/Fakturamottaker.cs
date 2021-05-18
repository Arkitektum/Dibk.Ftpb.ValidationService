using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Fakturamottaker
    {
        public string organisasjonsnummer { get; set; }

        public string bestillerReferanse { get; set; }

        public string fakturareferanser { get; set; }

        public string navn { get; set; }

        public string prosjektnummer { get; set; }

        public bool? ehfFaktura { get; set; }

        public bool? fakturaPapir { get; set; }

        public EnkelAdresse adresse { get; set; }
    }
}
