﻿using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class KodelisteValidationEntity
    {
        [XmlElement("kodeverdi")]
        public string Kodeverdi { get; set; }
        [XmlElement("kodebeskrivelse")]
        public string Kodebeskrivelse { get; set; }
    }
}
