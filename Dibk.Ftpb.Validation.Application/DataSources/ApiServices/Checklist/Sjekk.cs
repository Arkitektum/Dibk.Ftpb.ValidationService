using System.Collections.Generic;

namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class Sjekk
{       /// <summary>
        /// Intern id
        /// </summary>
        public int SjekkId { get; set; }

        /// <summary>
        /// Referanse Id til felles Id for sjekkpunkt som gjelder flere søknadstyper
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Kategorisering av sjekk. Samme som sjekkpunkttype i eByggesak
        /// </summary>
        public string Sjekkpunkttype { get; set; }

        /// <summary>
        /// Sjekkpunkt
        /// </summary>
        public string Navn { get; set; }

        /// <summary>
        /// Tema
        /// </summary>
        public string Tema { get; set; }

        /// <summary>
        /// Prosesskategori/Søknadstype
        /// </summary>
        public string Prosesskategori { get; set; }

        /// <summary>
        /// Hvis regel/spørsmål/behandling=true/false så angis det her mulige utfall på behandlingen
        /// </summary>
        public List<Utfall> Utfall { get; set; }
        public List<Sjekk> Undersjekkpunkter { get; set; }

        /// <summary>
        /// Rekkefølge på sjekk - samme som radnr i excelarket
        /// </summary>
        public int Rekkefolge { get; set; }

    }
}
