namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class FakturamottakerValidationEntity : ValidationEntityBase<Fakturamottaker>
    {
        public FakturamottakerValidationEntity(Fakturamottaker modelData, string xmlElementName, string parentEntityDataModelXpath = null) 
            : base(modelData, xmlElementName, parentEntityDataModelXpath)
        {}
    }
    public class Fakturamottaker
    { 
        public string Organisasjonsnummer { get; set; }

        public string BestillerReferanse { get; set; }

        public string Fakturareferanser { get; set; }

        public string Navn { get; set; }

        public string Prosjektnummer { get; set; }

        public bool? EhfFaktura { get; set; }

        public bool? FakturaPapir { get; set; }

        public EnkelAdresseValidationEntity Adresse { get; set; }
    }
}
