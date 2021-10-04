﻿using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class AktoerValidationEntity 
    {
        [XmlElement("partstype", IsNullable = true)]
        public KodelisteValidationEntity Partstype { get; set; }
        [XmlElement("foedselsnummer", IsNullable = true)]
        public string Foedselsnummer { get; set; }
        [XmlElement("organisasjonsnummer", IsNullable = true)]
        public string Organisasjonsnummer { get; set; }
        [XmlElement("navn")]
        public string Navn { get; set; }
        [XmlElement("adresse")]
        public EnkelAdresseValidationEntity Adresse { get; set; }
        [XmlElement("telefonnummer")]
        public string Telefonnummer { get; set; }
        [XmlElement("mobilnummer")]
        public string Mobilnummer { get; set; }
        [XmlElement("epost")]
        public string Epost { get; set; }
        [XmlElement("kontaktperson")]
        public KontaktpersonValidationEntity Kontaktperson { get; set; }
    }
}
