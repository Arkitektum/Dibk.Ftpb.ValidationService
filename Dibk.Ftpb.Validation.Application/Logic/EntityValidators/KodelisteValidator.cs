﻿using System;
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

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KodelisteValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, object codeListName, RegistryType registryType, ICodeListService codeListService)
                   : base(entityValidatorTree, nodeId)
        {
            _codeListName = codeListName;
            _registryType = registryType;
            _codeListService = codeListService;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt, null);
            AddValidationRule(ValidationRuleEnum.kodeliste_gyldig, null);
            AddValidationRule(ValidationRuleEnum.utfylt, "kodeverdi");
            AddValidationRule(ValidationRuleEnum.gyldig, "kodeverdi");
            AddValidationRule(ValidationRuleEnum.utfylt, "kodebeskrivelse");
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeEntry)
        {
            base.ResetValidationMessages();

            var xpath = kodeEntry.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(kodeEntry?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(kodeEntry.ModelData.Kodeverdi))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/kodeverdi");
                }
                else
                {
                    var isCodeValid = _codeListService.IsCodelistValid(_codeListName, kodeEntry.ModelData?.Kodeverdi, _registryType);
                    if (!isCodeValid.HasValue)
                    {
                        //AddMessageFromRule(KodeListValidationEnum.kode_KanIkkeValidere, xpath);
                        AddMessageFromRule(ValidationRuleEnum.kodeliste_gyldig, xpath);
                    }
                    else
                    {
                        if (!isCodeValid.GetValueOrDefault())
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, xpath, new[] { kodeEntry.ModelData?.Kodeverdi });
                        }
                    }
                }
            }
            return ValidationResult;
        }
    }
}
