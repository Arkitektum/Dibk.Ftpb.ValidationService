using Dibk.Ftpb.Validation.Application.Enums;
using Newtonsoft.Json;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class RuleDocumentationModel
    {
        [JsonProperty("Regelnr")]
        public string RuleId { get; set; }

        [JsonProperty("sjkPkt")]
        public string CheckListPt { get; set; }

        [JsonProperty("Beskrivelse")]
        public string Description { get; set; }

        [JsonProperty("Valideringsresultat")]
        public string RuleType { get; set; }

        [JsonProperty("Betingelse")]
        public string XpathCondition { get; set; }
        
        [JsonProperty("Forutsetning")]
        public string XpathPrecondition { get; set; }
    }
}
