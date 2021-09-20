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
        private string _xpath;

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
            AddValidationRule(ValidationRuleEnum.gyldig);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kodeverdi);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.kodeverdi);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kodebeskrivelse);
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeEntry = null)
        {
            base.ResetValidationMessages();

            _xpath = kodeEntry?.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(kodeEntry?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, AddXpathToMessage());
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(kodeEntry.ModelData.Kodeverdi))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, AddXpathToMessage(FieldNameEnum.kodeverdi));
                }
                else
                {
                    var isCodeValid = _codeListService.IsCodelistValid(_codeListName, kodeEntry.ModelData?.Kodeverdi, _registryType);
                    if (!isCodeValid.HasValue)
                    {
                        AddMessageFromRule(ValidationRuleEnum.validert, AddXpathToMessage(FieldNameEnum.kodeverdi));
                    }
                    else
                    {
                        if (!isCodeValid.GetValueOrDefault())
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, AddXpathToMessage(FieldNameEnum.kodeverdi), new[] { kodeEntry.ModelData?.Kodeverdi });
                        }
                    }
                }
            }
            
            return ValidationResult;
        }
        private string AddXpathToMessage(FieldNameEnum? fieldName = null)
        {

            //var xpathComposed = string.Format("{0}/{1}", new[] { _xpath, fieldName?.ToString() });

            string xpathComposed = _xpath;
            if (fieldName != null)
            {
                xpathComposed += $"/{fieldName?.ToString()}";
            }
            return xpathComposed;
        }
    }
}
