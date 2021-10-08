using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Ansvarsrett
    {
        [XmlElement("erklaeringAnsvarligProsjekterende", IsNullable = true)]
        public bool? ErklaeringAnsvarligProsjekterende { get; set; }
        
        [XmlElement("erklaeringAnsvarligUtfoerende", IsNullable = true)]
        public bool? ErklaeringAnsvarligUtfoerende { get; set; }
        
        [XmlElement("erklaeringAnsvarligKontrollerende", IsNullable = true)]
        public bool? ErklaeringAnsvarligKontrollerende { get; set; }

        [XmlArray("ansvarsomraader", IsNullable = false)]
        [XmlArrayItem("ansvarsomraade", IsNullable = true)]
        public Ansvarsomraade[] Ansvarsomraades { get; set; }

        [XmlElement("foretak")]
        public Foretak Foretak { get; set; }
    }
}
