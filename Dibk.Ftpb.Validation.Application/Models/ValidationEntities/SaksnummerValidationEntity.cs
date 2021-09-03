namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class SaksnummerValidationEntity : ValidationEntityBase<Saksnummer>
    {
        public SaksnummerValidationEntity(Saksnummer modelData, string xmlElementName, string parentEntityDataModelXpath = null) : base(modelData, xmlElementName, parentEntityDataModelXpath)
        { }
    }
    public class Saksnummer
    {
        public string Saksaar { get; set; }
        public string Sakssekvensnummer { get; set; }
    }
}