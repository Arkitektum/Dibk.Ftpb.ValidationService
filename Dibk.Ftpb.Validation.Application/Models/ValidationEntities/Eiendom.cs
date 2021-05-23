namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Eiendom : ValidationEntityBase
    {
        public Eiendom(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

        public EiendomsAdresse Adresse { get; set; }
        public Matrikkel Matrikkel { get; set; }
        public string Bygningsnummer { get; set; }
        public string Bolignummer { get; set; }
        public string Kommunenavn { get; set; }
    }
}
