namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Matrikkel : ValidationEntityBase
    {
        public Matrikkel(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

        public string Kommunenummer { get; set; }
        public string Gaardsnummer { get; set; }
        public string Bruksnummer { get; set; }
        public string Festenummer { get; set; }
        public string Seksjonsnummer { get; set; }
    }
}