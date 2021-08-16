using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class BeskrivelseAvTiltakValidatorLogic
    {
        private List<EntityValidatorNode> _entityValidatorNodes;
        private int _mainNode;

        private readonly ICodeListService _codeListService;
        private AnleggstypeValidator _anleggstypeValidator;
        private NaeringsgruppeValidator _naeringsgruppeValidator;
        private BygningstypeValidator _bygningstypeValidator;
        private TiltaksformaalValidator _tiltaksformaalValidator;
        private FormaaltypeValidator _formaaltypeValidator;
        private TiltakstypeValidator _tiltakstypeValidator;
        private IBeskrivelseAvTiltakValidator _beskrivelseAvTiltakValidator;
        private IList<EntityValidatorNode> _tree;


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
            get => _mainNode + 6;
        }
        public List<ValidationRule> ValidationRules
        {
            get => AllValidationRules();

        }
        public IBeskrivelseAvTiltakValidator Validator
        {
            get => SetUpClasses();
        }

        public BeskrivelseAvTiltakValidatorLogic(int startNode, ICodeListService codeListService)
        {
            _mainNode = startNode;
            _codeListService = codeListService;
            _entityValidatorNodes = ValidatorEntityNodeList();

        }
        private List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var beskrivelseAvTiltakNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = _mainNode, EnumId = EntityValidatorEnum.BeskrivelseAvTiltakValidator, ParentID = null},
                new () {NodeId = _mainNode + 1, EnumId = EntityValidatorEnum.FormaaltypeValidator, ParentID = _mainNode},
                new () {NodeId = _mainNode + 2, EnumId = EntityValidatorEnum.AnleggstypeValidator, ParentID = _mainNode + 1},
                new () {NodeId = _mainNode + 3, EnumId = EntityValidatorEnum.NaeringsgruppeValidator, ParentID = _mainNode + 1},
                new () {NodeId = _mainNode + 4, EnumId = EntityValidatorEnum.BygningstypeValidator, ParentID = _mainNode + 1},
                new () {NodeId = _mainNode + 5, EnumId = EntityValidatorEnum.TiltaksformaalValidator, ParentID = _mainNode + 1},
                new () {NodeId = _mainNode + 6, EnumId = EntityValidatorEnum.TiltakstypeValidator, ParentID = _mainNode},
            };
            return beskrivelseAvTiltakNodeList;
        }


        private IBeskrivelseAvTiltakValidator SetUpClasses()
        {
            _anleggstypeValidator = new AnleggstypeValidator(Tree, _mainNode + 2, _codeListService);
            _naeringsgruppeValidator = new NaeringsgruppeValidator(Tree, _mainNode + 3, _codeListService);
            _bygningstypeValidator = new BygningstypeValidator(Tree, _mainNode + 4, _codeListService);
            _tiltaksformaalValidator = new TiltaksformaalValidator(Tree, _mainNode + 5, _codeListService);
            _formaaltypeValidator = new FormaaltypeValidator(Tree, _mainNode + 1, _anleggstypeValidator, _naeringsgruppeValidator, _bygningstypeValidator, _tiltaksformaalValidator);
            _tiltakstypeValidator = new TiltakstypeValidator(Tree, _mainNode + 6, _codeListService);
            
            _beskrivelseAvTiltakValidator = new BeskrivelseAvTiltakValidator(Tree, _mainNode, _formaaltypeValidator, _tiltakstypeValidator);

            return _beskrivelseAvTiltakValidator;
        }

        private List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_formaaltypeValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_anleggstypeValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_naeringsgruppeValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_bygningstypeValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_tiltaksformaalValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_tiltakstypeValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_beskrivelseAvTiltakValidator.ValidationResult.ValidationRules);

            return validationResults;
        }

    }
}
