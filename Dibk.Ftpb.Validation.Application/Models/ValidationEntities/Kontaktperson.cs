namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Kontaktperson : ValidationEntityBase
    {
        public Kontaktperson(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

        public string Navn { get; set; }

        public string Telefonnummer { get; set; }

        public string Mobilnummer { get; set; }

        public string Epost { get; set; }
    }
}
