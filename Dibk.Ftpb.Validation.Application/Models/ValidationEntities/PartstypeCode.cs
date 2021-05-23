namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class PartstypeCode : ValidationEntityBase
    {
        public PartstypeCode(string xmlElementName, ValidationEntityBase parentEntity = null) : base(xmlElementName, parentEntity)
        {}

        public string Kodeverdi { get; set; }
        public string Kodebeskrivelse { get; set; }
    }
}
