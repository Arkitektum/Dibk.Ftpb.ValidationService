﻿using System;
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
    public class ArbeidsplasserValidatorLogic
    {
        private IList<EntityValidatorNode> _tree;
        private List<EntityValidatorNode> _entityValidatorNodes;
        private int _mainNode;

        public ArbeidsplasserValidatorLogic(int mainNode)
        {
            _mainNode = mainNode;
            _entityValidatorNodes = ValidatorEntityNodeList();
        }
        private IArbeidsplasserValidator _arbeidsplasserValidator;

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
        //public IArbeidsplasserValidator Validator
        //{
        //    get => SetUpClasses();
        //}

        public IArbeidsplasserValidator SetUpClasses(EntityValidatorEnum arbeidsplasserValidatorEnum)
        {
            if (_arbeidsplasserValidator == null)
            {
                if (arbeidsplasserValidatorEnum == EntityValidatorEnum.ArbeidsplasserValidator)
                {
                    _arbeidsplasserValidator = new ArbeidsplasserValidator(Tree, _mainNode);
                }
                else if (arbeidsplasserValidatorEnum == EntityValidatorEnum.ArbeidsplasserValidatorV2)
                {
                    _arbeidsplasserValidator = new ArbeidsplasserValidatorV2(Tree, _mainNode);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Invalid versjon of ArbeidsplasserValidator");
                }
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
