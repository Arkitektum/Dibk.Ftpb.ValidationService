using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class KodelisteValidator : EntityValidatorBase, IKodelisteValidator
    {
        private readonly object _codeListName;
        private readonly RegistryType _registryType;
        protected ICodeListService _codeListService;
        public string _entityXPath { get=> base._entityXPath; }

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KodelisteValidator(IList<EntityValidatorNode> entityValidatorTree, int? nodeId, object codeListName, RegistryType registryType, ICodeListService codeListService)
                   : base(entityValidatorTree, nodeId)
        {
            _codeListName = codeListName;
            _registryType = registryType;
            _codeListService = codeListService;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kodeverdi);
            AddValidationRule(ValidationRuleEnum.validert, FieldNameEnum.kodeverdi);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.kodeverdi);
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeEntry = null)
        {
            base.ResetValidationMessages();

            if (Helpers.ObjectIsNullOrEmpty(kodeEntry))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(kodeEntry.Kodeverdi))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.kodeverdi); 
                }
                else
                {
                    var isCodeValid = _codeListService.IsCodelistValid(_codeListName, kodeEntry.Kodeverdi, _registryType);
                    if (!isCodeValid.HasValue)
                    {
                        AddMessageFromRule(ValidationRuleEnum.validert, FieldNameEnum.kodeverdi);
                    }
                    else
                    {
                        if (!isCodeValid.GetValueOrDefault())
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.kodeverdi, new[] { kodeEntry.Kodeverdi });
                        }
                    }
                }
            }
            
            return ValidationResult;
        }
    }
}
