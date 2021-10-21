namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class Utfall
    {        /// <summary>
             /// Om svaret/bahendlingen er Ja/nei-true/false
             /// </summary>
        public bool Utfallverdi { get; set; }

        /// <summary>
        /// Utfall type som passer til verdi  
        /// </summary>
//        public string Utfalltype { get; set; }

        public string Utfalltypekode { get; set; }

//        public Utfalltekst Utfalltekst { get; set; }


        public Utfall(bool utfallverdi, string utfalltype, string utfallkode, string tittel, string beskrivelse, string tittelNynorsk, string beskrivelseNynorsk, string innholdstype)
        {
            Utfallverdi = utfallverdi;
//            Utfalltype = utfalltype;
            Utfalltypekode = utfallkode;
        }
    }
}
