using Dibk.Ftpb.Validation.Application.Services;
using System.Text.Json;

namespace Dibk.Ftpb.Validation.Application.Reporter
{
    public class PrefillChecklistAnswerBuilder
    {
        //private readonly IChecklistService _checklistService;

        //public PrefillChecklistAnswerBuilder(IChecklistService checklistService)
        //{
        //    _checklistService = checklistService;
        //}
        //private XmlDocument xml = new XmlDocument();

        public static PrefillChecklist Build(ValidationResult validationResult, IChecklistService checklistService, string dataFormatVersion)
        {
            //var sjekklisteKrav = new List<ChecklistAnswer>();
            //var xmldata = validationInput.FormData;
            //xml.LoadXml(xmldata); //myXmlString is the xml file in string //copying xml to string: string myXmlString = xmldoc.OuterXml.ToString();
            var retVal = checklistService.GetPrefillChecklist("", validationResult);
            var prefillChecklist = JsonSerializer.Deserialize<PrefillChecklist>(retVal);

            return prefillChecklist;
        }



        //private void GetCriteriasForAllChecklistItems()
        //{

        //}

        //private bool ErEiendomByggestedUtfylt(string xPath)
        //{
        //    return NodeHasValue(xPath);
        //}

        //private bool NodeHasValue(string xPath)
        //{
        //    var node = xml.SelectSingleNode(xPath);
        //    if (!string.IsNullOrEmpty(node.Value))
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
