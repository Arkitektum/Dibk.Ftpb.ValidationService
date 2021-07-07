using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common
{
    public class EntityValidatorNode
    {
        public int Id { get; set; }
        public EntityValidatorEnum EnumId { get; set; }
        public int? ParentID { get; set; }
        public string RulePath { get; set; }
        public string EntityXPath { get; set; }
        public List<EntityValidatorNode> Children { get; set; }
    }
}
