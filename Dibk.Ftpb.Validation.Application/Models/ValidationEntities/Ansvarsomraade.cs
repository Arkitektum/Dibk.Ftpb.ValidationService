using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Ansvarsomraade
    {
        [XmlElement("beskrivelseAvAnsvarsomraade", IsNullable = true)]
        public string BeskrivelseAvAnsvarsomraade { get; set; }

        [XmlElement("dekkesOmraadeAvSentralGodkjenning", IsNullable = true)]
        public bool? DekkesOmraadeAvSentralGodkjenning { get; set; }

        [XmlElement("funksjon", IsNullable = true)]
        public Kodeliste Funksjon { get; set; }

        [XmlElement("samsvarKontrollVedFerdigattest", IsNullable = true)]
        public bool? SamsvarKontrollVedFerdigattest { get; set; }
        
        [XmlElement("samsvarKontrollVedMidlertidigBrukstillatelse", IsNullable = true)]
        public bool? SamsvarKontrollVedMidlertidigBrukstillatelse { get; set; }
        
        [XmlElement("samsvarKontrollVedRammetillatelse", IsNullable = true)]
        public bool? SamsvarKontrollVedRammetillatelse { get; set; }

        [XmlElement("tiltaksklasse", IsNullable = true)]
        public Kodeliste tiltaksklasse { get; set; }

        [XmlElement("soeknadssystemetsReferanse", IsNullable = true)]
        public string SoeknadssystemetsReferanse { get; set; }
    }
}
