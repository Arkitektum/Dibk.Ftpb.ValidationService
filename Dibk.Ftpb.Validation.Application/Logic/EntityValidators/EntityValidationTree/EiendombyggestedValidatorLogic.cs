using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class EiendombyggestedValidatorLogic
    {
        private List<EntityValidatorNode> _entityValidatorNodes;
        private int _mainNode;

        private readonly IMunicipalityValidator _municipalityValidator;

        private IEiendomsAdresseValidator _eiendomsAdresseValidator;
        private IMatrikkelValidator _matrikkelValidator;
        private IEiendomByggestedValidator _eiendomByggestedValidator;
        private IList<EntityValidatorNode> _tree;

        public EiendombyggestedValidatorLogic(int startNode, IMunicipalityValidator municipalityValidator)
        {
            _mainNode = startNode;
            _municipalityValidator = municipalityValidator;
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
            get => _mainNode + 2;
        }
        public List<ValidationRule> ValidationRules
        {
            get => AllValidationRules();

        }

        public IEiendomByggestedValidator Validator
        {
            get => SetupCalsses();
        }

        private IEiendomByggestedValidator SetupCalsses()
        {
            _eiendomsAdresseValidator = new EiendomsAdresseValidator(Tree, _mainNode + 1);
            _matrikkelValidator = new MatrikkelValidator(Tree, _mainNode + 2, _municipalityValidator);
            _eiendomByggestedValidator = new EiendomByggestedValidator(Tree, _mainNode, _eiendomsAdresseValidator, _matrikkelValidator);

            return _eiendomByggestedValidator;
        }

        private List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_matrikkelValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_eiendomsAdresseValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_eiendomByggestedValidator.ValidationResult.ValidationRules);

            return validationResults;
        }



        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var eiendombyggestedNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = _mainNode, EnumId = EntityValidatorEnum.EiendomByggestedValidator, ParentID = null},
                new () {NodeId = _mainNode + 1, EnumId = EntityValidatorEnum.EiendomsAdresseValidator, ParentID = _mainNode},
                new () {NodeId = _mainNode + 2, EnumId = EntityValidatorEnum.MatrikkelValidator, ParentID = _mainNode},
            };
            return eiendombyggestedNodeList;
        }
    }
}
