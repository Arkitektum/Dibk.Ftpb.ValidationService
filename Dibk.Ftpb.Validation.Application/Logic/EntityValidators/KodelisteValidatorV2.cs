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
    public class KodelisteValidatorV2 : EntityValidatorBase, IKodelisteValidator
    {
        private readonly object _codeListName;
        private readonly RegistryType _registryType;
        protected ICodeListService _codeListService;

        public KodelisteValidatorV2(IList<EntityValidatorNode> entityValidatorTree, int? nodeId, object codeListName, RegistryType registryType, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId)
        {
            _codeListName = codeListName;
            _registryType = registryType;
            _codeListService = codeListService;
        }
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kodeverdi);
            AddValidationRule(ValidationRuleEnum.validert, FieldNameEnum.kodeverdi);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.kodeverdi);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kodebeskrivelse);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.kodebeskrivelse);
            
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeEntry)
        {
            base.ResetValidationMessages();

            var xpath = kodeEntry?.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(kodeEntry?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(kodeEntry.ModelData.Kodeverdi))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/{FieldNameEnum.kodeverdi}");
                }
                else
                {
                    var isCodeValid = _codeListService.IsCodelistValid(_codeListName, kodeEntry.ModelData?.Kodeverdi, _registryType);
                    if (!isCodeValid.HasValue)
                    {
                        AddMessageFromRule(ValidationRuleEnum.validert, $"{xpath}/{FieldNameEnum.kodeverdi}");
                    }
                    else
                    {
                        if (!isCodeValid.GetValueOrDefault())
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xpath}/{FieldNameEnum.kodeverdi}", new[] { kodeEntry.ModelData?.Kodeverdi });
                        }
                    }

                    if (string.IsNullOrEmpty(kodeEntry.ModelData.Kodebeskrivelse))
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/{FieldNameEnum.kodebeskrivelse}");
                    }
                    else
                    {
                        if (isCodeValid.GetValueOrDefault())
                        {
                            var isCodelistLabelValid = _codeListService.IsCodelistLabelValid(_codeListName,kodeEntry.ModelData?.Kodeverdi, kodeEntry.ModelData?.Kodebeskrivelse, _registryType);
                            if (!isCodelistLabelValid.GetValueOrDefault())
                            {
                                AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xpath}/{FieldNameEnum.kodebeskrivelse}", new[] { kodeEntry.ModelData?.Kodeverdi, kodeEntry.ModelData?.Kodebeskrivelse });
                            }
                        }
                    }
                }
            }
            return ValidationResult;
        }
    }
}
