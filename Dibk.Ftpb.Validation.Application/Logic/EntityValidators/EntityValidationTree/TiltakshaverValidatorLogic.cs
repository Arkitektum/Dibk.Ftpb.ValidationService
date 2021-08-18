using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class TiltakshaverValidatorLogic
    {
        private IList<EntityValidatorNode> _tree;
        private List<EntityValidatorNode> _entityValidatorNodes;
        private int _mainNode;
        
        private IAktoerValidator _aktoerValidator;
        private readonly ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;

        private IEnkelAdresseValidator _tiltakshaverEnkelAdresseValidator;
        private IKontaktpersonValidator _tiltakshaverKontaktpersonValidator;
        private IKodelisteValidator _tiltakshaverPartstypeValidator;


        public TiltakshaverValidatorLogic(int mainNode, ICodeListService codeListService, IPostalCodeService postalCodeService)
        {
            _mainNode = mainNode;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;
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
        public IAktoerValidator Validator
        {
            get => SetUpClasses();
        }
        
        private IAktoerValidator SetUpClasses()
        {
            if (_aktoerValidator != null)
                return _aktoerValidator;

            _tiltakshaverKontaktpersonValidator = new KontaktpersonValidator(Tree, _mainNode + 1);
            _tiltakshaverPartstypeValidator = new PartstypeValidator(Tree, _mainNode + 2, _codeListService);
            _tiltakshaverEnkelAdresseValidator = new EnkelAdresseValidator(Tree, _mainNode + 3, _postalCodeService);

            _aktoerValidator = new TiltakshaverValidator(Tree, _mainNode, _tiltakshaverEnkelAdresseValidator, _tiltakshaverKontaktpersonValidator, _tiltakshaverPartstypeValidator, _codeListService);
            return _aktoerValidator;
        }

        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var validatorEntityNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = _mainNode, EnumId = EntityValidatorEnum.TiltakshaverValidator, ParentID = null},
                new () {NodeId = _mainNode + 1, EnumId = EntityValidatorEnum.KontaktpersonValidator, ParentID = _mainNode},
                new () {NodeId = _mainNode + 2, EnumId = EntityValidatorEnum.PartstypeValidator, ParentID = _mainNode},
                new () {NodeId = _mainNode + 3, EnumId = EntityValidatorEnum.EnkelAdresseValidator, ParentID = _mainNode}
            };
            return validatorEntityNodeList;
        }

        private List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_tiltakshaverKontaktpersonValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_tiltakshaverPartstypeValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_tiltakshaverEnkelAdresseValidator.ValidationResult.ValidationRules);
            return validationResults;
        }
    }
}
