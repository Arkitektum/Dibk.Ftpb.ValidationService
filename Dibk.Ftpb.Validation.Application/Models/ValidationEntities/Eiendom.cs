namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class EiendomValidationEntity 
    {
        public EiendomsAdresseValidationEntity Adresse { get; set; }
        public MatrikkelValidationEntity Matrikkel { get; set; }
        public string Bygningsnummer { get; set; }
        public string Bolignummer { get; set; }
        public string Kommunenavn { get; set; }
    }
}
