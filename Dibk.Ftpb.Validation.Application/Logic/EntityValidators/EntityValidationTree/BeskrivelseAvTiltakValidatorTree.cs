using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class BeskrivelseAvTiltakValidatorTree
    {
        public static List<EntityValidatorNode> BeskrivelseAvTiltakValidatorEntityValidatorNodeList()
        {
            var beskrivelseAvTiltakTree = new List<EntityValidatorNode>()
            {
                new () {NodeId = 15, EnumId = EntityValidatorEnum.BeskrivelseAvTiltakValidator, ParentID = null},
                new () {NodeId = 16, EnumId = EntityValidatorEnum.FormaaltypeValidator, ParentID = 15},
                new () {NodeId = 17, EnumId = EntityValidatorEnum.AnleggstypeValidator, ParentID = 16},
                new () {NodeId = 18, EnumId = EntityValidatorEnum.NaeringsgruppeValidator, ParentID = 16},
                new () {NodeId = 19, EnumId = EntityValidatorEnum.BygningstypeValidator, ParentID = 16},
                new () {NodeId = 20, EnumId = EntityValidatorEnum.TiltaksformaalValidator, ParentID = 16},
                new () {NodeId = 21, EnumId = EntityValidatorEnum.TiltakstypeValidator, ParentID = 15},
            };
            return beskrivelseAvTiltakTree;
        }
    }
}
