using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class EiendombyggestedLogic
    {
        private List<EntityValidatorNode> _entityValidatorNodes;
        private int mainNode;
        public IList<EntityValidatorNode> GetTree
        {
            get => EntityValidatiorTree.BuildTree(_entityValidatorNodes);
        }
        public List<EntityValidatorNode> GetNodeList
        {
            get => _entityValidatorNodes;
        }
        public int LastNodeNumber
        {
            get => mainNode + 2;
        }

        public EiendombyggestedLogic(int startNode)
        {
            _entityValidatorNodes = ValidatorEntityNodeList();
            mainNode = startNode;
        }

        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var eiendombyggestedNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = mainNode, EnumId = EntityValidatorEnum.EiendomByggestedValidator, ParentID = null},
                new () {NodeId = mainNode+1, EnumId = EntityValidatorEnum.EiendomsAdresseValidator, ParentID = mainNode},
                new () {NodeId = mainNode+2, EnumId = EntityValidatorEnum.MatrikkelValidator, ParentID = mainNode},
            };
            return eiendombyggestedNodeList;
        }
    }
}
