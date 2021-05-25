namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class EiendomsAdresseValidationEntity : ValidationEntityBase<EiendomsAdresse>
    {
        public EiendomsAdresseValidationEntity(EiendomsAdresse modelData, string xmlElementName, string parentEntityDataModelXpath = null) 
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class EiendomsAdresse
    { 
        public string Adresselinje1 { get; set; }
        public string Adresselinje2 { get; set; }
        public string Adresselinje3 { get; set; }
        public string Postnr { get; set; }
        public string Poststed { get; set; }
        public string Landkode { get; set; }
        public string Gatenavn { get; set; }
        public string Husnr { get; set; }
        public string Bokstav { get; set; }
    }
}