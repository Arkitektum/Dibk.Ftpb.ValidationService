namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class EnkelAdresseValidationEntity : ValidationEntityBase<EnkelAdresse>
    {
        public EnkelAdresseValidationEntity(EnkelAdresse modelData, string xmlElementName, string parentEntityDataModelXpath = null) 
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class EnkelAdresse
    { 
        public string Adresselinje1 { get; set; }
        public string Adresselinje2 { get; set; }
        public string Adresselinje3 { get; set; }
        public string Postnr { get; set; }
        public string Poststed { get; set; }
        public string Landkode { get; set; }

    }
}