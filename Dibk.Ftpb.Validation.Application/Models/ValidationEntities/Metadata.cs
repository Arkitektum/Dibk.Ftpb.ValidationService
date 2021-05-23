namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Metadata : ValidationEntityBase
    {
        public Metadata(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

        public string FraSluttbrukersystem { get; set; }
        public string Prosjektnavn { get; set; }
    }
}
