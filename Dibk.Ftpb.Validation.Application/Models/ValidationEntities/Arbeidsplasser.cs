namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class ArbeidsplasserValidationEntity 
    {

        public bool? Framtidige { get; set; }
        public bool? Faste { get; set; }
        public bool? Midlertidige { get; set; }
        public string AntallAnsatte { get; set; }
        public bool? Eksisterende { get; set; }
        public bool? UtleieBygg { get; set; }
        public string AntallVirksomheter { get; set; }
        public string Beskrivelse { get; set; }
        public bool? Veiledning { get; set; }

    }
}