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
        private ArbeidsplasserValidatorV2 _arbeidsplasserValidator;

        public ArbeidsplasserValidatorV2Logic(int mainNode) : base(mainNode)
        {
        }

        public new ArbeidsplasserValidatorV2 Validator
        {
            get => SetUpClasses();
        }

        private ArbeidsplasserValidatorV2 SetUpClasses()
        {
            if (_arbeidsplasserValidator == null)
            {
                //_arbeidsplasserValidator = new ArbeidsplasserValidatorV2(Tree, _mainNode);
            }
            return _arbeidsplasserValidator;
        }
        public List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var validatorEntityNodeList = new List<EntityValidatorNode>()
            {
                new() {NodeId = _mainNode, EnumId = EntityValidatorEnum.ArbeidsplasserValidatorV2, ParentID = null}

            };
            return validatorEntityNodeList;
        }

        protected override List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_arbeidsplasserValidator.ValidationResult.ValidationRules);
            return validationResults;
        }
    }
}
