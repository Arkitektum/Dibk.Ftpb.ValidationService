using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class BetalingValidationEntity 
    {
        [XmlElement("beskrivelse")]
        public string Beskrivelse { get; set; }
        [XmlElement("ordreId")]
        public string OrdreId { get; set; }
        [XmlElement("sum")]
        public string Sum { get; set; }
        [XmlElement("transId")]
        public string TransId { get; set; }
        [XmlElement("gebyrkategori")]
        public string GebyrKategori { get; set; }
        [XmlElement("skalFaktureres")]
        public bool? SkalFaktureres { get; set; }
    }
}
