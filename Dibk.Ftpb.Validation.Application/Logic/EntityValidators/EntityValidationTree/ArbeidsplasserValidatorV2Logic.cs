using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class ArbeidsplasserValidatorV2Logic : ArbeidsplasserValidatorLogic
    {
        private IList<EntityValidatorNode> _tree;
        private List<EntityValidatorNode> _entityValidatorNodes;
        private ArbeidsplasserValidatorV2 _arbeidsplasserValidator;
        private int _mainNode;

        public ArbeidsplasserValidatorV2Logic(int mainNode) : base(mainNode)
        {
            _mainNode = mainNode;
            _entityValidatorNodes = ValidatorEntityNodeList();
        }

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
        public int LastNodeNumber
        {
            //TODO: Er dette riktig?? Skal være "_mainNode"
            get => _mainNode + 1;
        }
        public List<ValidationRule> ValidationRules
        {
            get => AllValidationRules();

        }
        public ArbeidsplasserValidatorV2 Validator
        {
            get => SetUpClasses();
        }

        private ArbeidsplasserValidatorV2 SetUpClasses()
        {
            if (_arbeidsplasserValidator == null)
            {
                _arbeidsplasserValidator = new ArbeidsplasserValidatorV2(Tree, _mainNode);
            }
            return _arbeidsplasserValidator;
        }
        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var validatorEntityNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = _mainNode, EnumId = EntityValidatorEnum.ArbeidsplasserValidator, ParentID = null}

            };
            return validatorEntityNodeList;
        }

        private List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_arbeidsplasserValidator.ValidationResult.ValidationRules);
            return validationResults;
        }
    }
}
