using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common
{
    public class EntityValidatiorTree
    {
        public static IList<EntityValidatorNode> BuildTree(IEnumerable<EntityValidatorNode> source)
        {
            var groups = source.GroupBy(i => i.ParentID);

            var roots = groups.FirstOrDefault(g => g.Key.HasValue == false).ToList();

            if (roots.Count > 0)
            {
                var dict = groups.Where(g => g.Key.HasValue).ToDictionary(g => g.Key.Value, g => g.ToList());
                for (int i = 0; i < roots.Count; i++) AddChildren(roots[i], dict);
            }

            return roots;
        }

        private static void AddChildren(EntityValidatorNode node, IDictionary<int, List<EntityValidatorNode>> source, string parentName = null, string parentRuleNumber = null)
        {
            if (source.ContainsKey(node.Id))
            {
                node.Children = source[node.Id];
                node.EntityXPath = AddXPathToNode(parentName ?? node.RulePath, node.EnumId);
                node.RulePath = AddRulePathToNode(parentRuleNumber ?? node.RulePath, node.EnumId);

                for (int i = 0; i < node.Children.Count; i++) AddChildren(node.Children[i], source, node.EntityXPath, node.RulePath);
            }
            else
            {
                node.EntityXPath = AddXPathToNode(parentName ?? node.RulePath, node.EnumId);
                node.RulePath = AddRulePathToNode(parentRuleNumber ?? node.RulePath, node.EnumId);
                node.Children = new List<EntityValidatorNode>();
            }
        }

        private static string AddXPathToNode(string entityXpath, EntityValidatorEnum EnumId)
        {

            var xmlNode = Helpers.GetEnumXmlNodeName(EnumId);
            var entityName = xmlNode ?? EnumId.ToString();

            if (string.IsNullOrEmpty(entityName))
                return entityXpath;
            var newXpath = $"{entityXpath}/{entityName}";
            return newXpath;
        }
        private static string AddRulePathToNode(string entityXpath, EntityValidatorEnum? EnumId = null)
        {
            if (!EnumId.HasValue)
                return entityXpath;
            
            var enumValidatorNumber = Helpers.GetEnumValidatorRuleNumber(EnumId);
            var newXpath = $"{entityXpath}.{enumValidatorNumber}";
            return newXpath;
        }
    }
}
