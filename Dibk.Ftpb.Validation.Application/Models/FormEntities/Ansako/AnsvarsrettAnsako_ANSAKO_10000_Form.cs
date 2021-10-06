using System.Xml.Serialization;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;

namespace Dibk.Ftpb.Validation.Application.Models.FormEntities.Ansako
{
    [XmlRoot("ErklaeringAnsvarsrett"), XmlType("ErklaeringAnsvarsrett")]
    public class AnsvarsrettAnsako_ANSAKO_10000_Form
    {
        [XmlElement("ansvarligSoeker")]
        public AktoerValidationEntity AnsvarligSoekerValidationEntity { get; set; }
    }
}
