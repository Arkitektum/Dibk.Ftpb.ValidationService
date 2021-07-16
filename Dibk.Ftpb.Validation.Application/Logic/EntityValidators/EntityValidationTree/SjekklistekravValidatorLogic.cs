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
    public class SjekklistekravValidatorLogic
    {
        private IList<EntityValidatorNode> _tree;
        private List<EntityValidatorNode> _entityValidatorNodes;
        private int _mainNode;

        private ISjekklistekravValidator _sjekklistekravValidator;
        public SjekklistekravValidatorLogic(int mainNode)
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
            get => _mainNode + 3;
        }
        public List<ValidationRule> ValidationRules
        {
            get => AllValidationRules();

        }

        public ISjekklistekravValidator Validator
        {
            get => SetupClasses();
        }

        private ISjekklistekravValidator SetupClasses()
        {
            if (_sjekklistekravValidator == null)
            {
                //_sjekklistepunktValidator = new SjekklistepunktValidator(EntityValidatorTree, 23, _codeListService);
                //_sjekklistekravValidator = new SjekklistekravValidator(EntityValidatorTree, 22, _sjekklistepunktValidator);
                _sjekklistekravValidator = new SjekklistekravValidator(Tree, 22);
            }

            return _sjekklistekravValidator;
        }

        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var validatorEntityNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = _mainNode, EnumId = EntityValidatorEnum.SjekklistekravValidator, ParentID = null},
                //new() {NodeId = _mainNode + 1, EnumId = EntityValidatorEnum.SjekklistepunktValidator, ParentID = _mainNode}
            };
            return validatorEntityNodeList;
        }

        private List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_sjekklistekravValidator.ValidationResult.ValidationRules);


            return validationResults;
        }
    }
}
