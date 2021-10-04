using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class FakturamottakerValidationEntity 
    {
        [XmlElement("organisasjonsnummer")]
        public string Organisasjonsnummer { get; set; }
        [XmlElement("bestillerReferanse")]
        public string BestillerReferanse { get; set; }
        [XmlElement("fakturareferanser")]
        public string Fakturareferanser { get; set; }
        [XmlElement("navn")]
        public string Navn { get; set; }
        [XmlElement("prosjektnummer")]
        public string Prosjektnummer { get; set; }
        [XmlElement("ehfFaktura")]
        public bool? EhfFaktura { get; set; }
        [XmlElement("fakturaPapir")]
        public bool? FakturaPapir { get; set; }
        [XmlElement("adresse")]
        public EnkelAdresseValidationEntity Adresse { get; set; }
    }
}
