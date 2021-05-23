namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class EnkelAdresse : ValidationEntityBase
    {
        public EnkelAdresse(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

        public string Adresselinje1 { get; set; }
        public string Adresselinje2 { get; set; }
        public string Adresselinje3 { get; set; }
        public string Postnr { get; set; }
        public string Poststed { get; set; }
        public string Landkode { get; set; }

    }
}