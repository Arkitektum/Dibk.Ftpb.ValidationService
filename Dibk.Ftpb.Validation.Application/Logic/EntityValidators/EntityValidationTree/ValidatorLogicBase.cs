using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public abstract class ValidatorLogicBase
    {
        protected IList<EntityValidatorNode> _tree;
        protected List<EntityValidatorNode> _entityValidatorNodes;
        protected int _mainNode;
        protected abstract List<ValidationRule> AllValidationRules();
        protected abstract IAktoerValidator SetUpClasses();
        //public abstract int LastNodeNumber;

        public IList<EntityValidatorNode> Tree
        {
            get => GetClassTree();
        }
        private IList<EntityValidatorNode> GetClassTree()
        {
            _tree ??= EntityValidatiorTree.BuildTree(_entityValidatorNodes);
            return _tree;
        }
        public List<EntityValidatorNode> NodeList
        {
            get => _entityValidatorNodes;
        }
        public List<ValidationRule> ValidationRules
        {
            get => AllValidationRules();

        }
        public IAktoerValidator Validator
        {
            get => SetUpClasses();
        }
    }
}
