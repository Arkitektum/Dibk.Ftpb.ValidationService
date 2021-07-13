using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class BeskrivelseAvTiltakValidatorLogic
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
            get => mainNode + 6;
        }

        public BeskrivelseAvTiltakValidatorLogic(int startNode)
        {
            _entityValidatorNodes = ValidatorEntityNodeList();
            mainNode = startNode;
        }
        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var beskrivelseAvTiltakNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = mainNode, EnumId = EntityValidatorEnum.BeskrivelseAvTiltakValidator, ParentID = null},
                new () {NodeId = mainNode + 1, EnumId = EntityValidatorEnum.FormaaltypeValidator, ParentID = mainNode},
                new () {NodeId = mainNode + 2, EnumId = EntityValidatorEnum.AnleggstypeValidator, ParentID = mainNode + 1},
                new () {NodeId = mainNode + 3, EnumId = EntityValidatorEnum.NaeringsgruppeValidator, ParentID = mainNode + 1},
                new () {NodeId = mainNode + 4, EnumId = EntityValidatorEnum.BygningstypeValidator, ParentID = mainNode + 1},
                new () {NodeId = mainNode + 5, EnumId = EntityValidatorEnum.TiltaksformaalValidator, ParentID = mainNode + 1},
                new () {NodeId = mainNode + 6, EnumId = EntityValidatorEnum.TiltakstypeValidator, ParentID = mainNode},
            };
            return beskrivelseAvTiltakNodeList;
        }
    }
}
