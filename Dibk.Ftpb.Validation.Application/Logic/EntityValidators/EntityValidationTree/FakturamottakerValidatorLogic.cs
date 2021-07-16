using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class FakturamottakerValidatorLogic
    {
        private readonly IPostalCodeService _postalCodeService;

        public int MainNode { get; }
        private IList<EntityValidatorNode> _tree;
        private List<EntityValidatorNode> _entityValidatorNodes;
        private int _mainNode;

        private IFakturamottakerValidator _fakturamottakerValidator;
        private IEnkelAdresseValidator _enkelAdresseValidator;


        public FakturamottakerValidatorLogic(int mainNode, IPostalCodeService postalCodeService)
        {
            _postalCodeService = postalCodeService;
            MainNode = mainNode;
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
            get => _mainNode + 1;
        }
        public List<ValidationRule> ValidationRules
        {
            get => AllValidationRules();

        }

        public IFakturamottakerValidator Validator
        {
            get => SetUpClasses();
        }

        private IFakturamottakerValidator SetUpClasses()
        {
            if (_fakturamottakerValidator == null)
            {
                _enkelAdresseValidator = new EnkelAdresseValidator(Tree, _mainNode + 1, _postalCodeService);
                _fakturamottakerValidator = new FakturamottakerValidator(Tree, _mainNode, _enkelAdresseValidator);
            }
            return _fakturamottakerValidator;
        }

        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var validatorEntityNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = _mainNode, EnumId = EntityValidatorEnum.FakturamottakerValidator, ParentID = null},
                new () {NodeId = _mainNode + 1, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = _mainNode}
            };
            return validatorEntityNodeList;
        }

        private List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_enkelAdresseValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_fakturamottakerValidator.ValidationResult.ValidationRules);

            return validationResults;
        }
    }
}
