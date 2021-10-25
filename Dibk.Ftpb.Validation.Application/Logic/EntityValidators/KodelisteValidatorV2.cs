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
        public string _entityXPath { get => base._entityXPath; }

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
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.kodebeskrivelse,$"{_entityXPath}/{FieldNameEnum.kodeverdi}");
        }

        public ValidationResult Validate(Kodeliste kodeEntry)
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

                    if (string.IsNullOrEmpty(kodeEntry.Kodebeskrivelse))
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.kodebeskrivelse);
                    }
                    else
                    {
                        if (isCodeValid.GetValueOrDefault())
                        {
                            var isCodelistLabelValid = _codeListService.IsCodelistLabelValid(_codeListName,kodeEntry.Kodeverdi, kodeEntry.Kodebeskrivelse, _registryType);
                            if (!isCodelistLabelValid.GetValueOrDefault())
                            {
                                AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.kodebeskrivelse, new[] { kodeEntry.Kodeverdi, kodeEntry.Kodebeskrivelse });
                            }
                        }
                    }
                }
            }
            return ValidationResult;
        }


    }
}
