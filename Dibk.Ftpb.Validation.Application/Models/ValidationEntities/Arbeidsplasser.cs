namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Arbeidsplasser : ValidationEntityBase
    {
        public Arbeidsplasser(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

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