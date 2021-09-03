using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Reporter;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators.EntityValidationTree
{
    public class AnsvarligSoekerValidatorLogic : ValidatorLogicBase
    {
        private readonly ICodeListService _codeListService;
        private readonly IPostalCodeService _postalCodeService;
        private IAktoerValidator _aktoerValidator;
        private IKontaktpersonValidator _ansvarligSoekerKontaktpersonValidator;
        private IKodelisteValidator _ansvarligSoekerPartstypeValidator;
        private IEnkelAdresseValidator _ansvarligSoekerEnkelAdresseValidator;

        public AnsvarligSoekerValidatorLogic(int mainNode, ICodeListService codeListService, IPostalCodeService postalCodeService)
        {
            _mainNode = mainNode;
            _codeListService = codeListService;
            _postalCodeService = postalCodeService;
            _entityValidatorNodes = ValidatorEntityNodeList();
        }

        //public override int LastNodeNumber()
        //{
        //    return _mainNode + 3;
        //}

        public int LastNodeNumber
        {
            get => _mainNode + 3;
        }


        protected override IAktoerValidator SetUpClasses()
        {
            if (_aktoerValidator != null)
                return _aktoerValidator;

            _ansvarligSoekerKontaktpersonValidator = new AnsvarligSoekerKontaktpersonValidator(Tree );
            _ansvarligSoekerPartstypeValidator = new AnsvarligSoekerPartstypeValidator(Tree, _codeListService);
            //_ansvarligSoekerEnkelAdresseValidator = new EnkelAdresseValidator(Tree, _mainNode + 3, _postalCodeService);

            //_aktoerValidator = new AnsvarligSoekerValidator(Tree, _mainNode, _ansvarligSoekerEnkelAdresseValidator, _ansvarligSoekerKontaktpersonValidator, _ansvarligSoekerPartstypeValidator, _codeListService);
            return _aktoerValidator;
        }
        private List<EntityValidatorNode> ValidatorEntityNodeList()
        {
            var validatorEntityNodeList = new List<EntityValidatorNode>()
            {
                new () {NodeId = _mainNode, EnumId = EntityValidatorEnum.AnsvarligSoekerValidator, ParentID = null},
                new () {NodeId = _mainNode + 1, EnumId = EntityValidatorEnum.AnsvarligSoekerKontaktpersonValidator, ParentID = _mainNode},
                new () {NodeId = _mainNode + 2, EnumId = EntityValidatorEnum.AnsvarligSoekerPartstypeValidator, ParentID = _mainNode},
                new () {NodeId = _mainNode + 3, EnumId = EntityValidatorEnum.AnsvarligSoekerEnkelAdresseValidator, ParentID = _mainNode}
            };
            return validatorEntityNodeList;
        }

        protected override List<ValidationRule> AllValidationRules()
        {
            var validationResults = new List<ValidationRule>();
            validationResults.AddRange(_ansvarligSoekerKontaktpersonValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_ansvarligSoekerPartstypeValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_ansvarligSoekerEnkelAdresseValidator.ValidationResult.ValidationRules);
            validationResults.AddRange(_aktoerValidator.ValidationResult.ValidationRules);
            return validationResults;
        }
    }
}
