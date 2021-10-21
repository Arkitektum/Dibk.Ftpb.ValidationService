using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common
{
    public class EntityValidatorNode
    {
        public int NodeId { get; set; }
        public EntityValidatorEnum EnumId { get; set; }
        public int? ParentID { get; set; }
        public string IdPath { get; set; }
        public string EntityXPath { get; set; }
        public List<EntityValidatorNode> Children { get; set; }
    }
}
