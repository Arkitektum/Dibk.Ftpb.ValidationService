namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class MetadataValidationEntity
    {
        public string FraSluttbrukersystem { get; set; }
        public string Prosjektnavn { get; set; }
        public string FtbId { get; set; }
        public string SluttbrukersystemUrl { get; set; }
        public string Hovedinnsendingsnummer { get; set; }
        public bool? ErNorskSvenskDansk { get; set; }
        public bool? KlartForSigneringFraSluttbrukersystem { get; set; }
        public bool? UnntattOffentlighet { get; set; }
    }
}
