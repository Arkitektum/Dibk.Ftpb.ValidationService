﻿using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class SaksnummerValidationEntity 
    {
        [XmlElement("saksaar")]
        public string Saksaar { get; set; }
        [XmlElement("sakssekvensnummer")]
        public string Sakssekvensnummer { get; set; }
    }
}