using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Models.ValidationEntities
{
    public abstract class EntityValidatorBase : IEntityValidator
    {
        protected List<ValidationRule> ValidationRules = new List<ValidationRule>();
        public EntityValidatorBase()
        {
            ValidationRules = new List<ValidationRule>();
        }

        public abstract void InitializeValidationRules(string context);
        public abstract void ValidateEntityFields(object data);

        //void ValidateEntityFields(object entityData)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
