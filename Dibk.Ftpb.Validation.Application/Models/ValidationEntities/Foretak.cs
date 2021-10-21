using System.Xml.Serialization;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public class Foretak : AktoerV2
    {
        [XmlElement("harSentralGodkjenning")]
        public bool? HarSentralGodkjenning { get; set; }
    }
}
