using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Dibk.Ftpb.Validation.Application.Models.Standard
{
    public class RuleDocumentationModel
    {
        [JsonPropertyName("Regelnr")]
        public string RuleId { get; set; }

        [JsonPropertyName("sjkPkt")]
        public string CheckListPt { get; set; }

        [JsonPropertyName("Beskrivelse")]
        public string Description { get; set; }

        [JsonPropertyName("Valideringsresultat")]
        public string RuleType { get; set; }

        [JsonPropertyName("Betingelse")]
        public string Xpath { get; set; }
        
        [JsonPropertyName("Forutsetning")]
        public string XpathPrecondition { get; set; }
    }
}
