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

        public KodelisteValidatorV2(IList<EntityValidatorNode> entityValidatorTree, int nodeId, object codeListName, RegistryType registryType, ICodeListService codeListService)
            : base(entityValidatorTree, nodeId)
        {
            _codeListName = codeListName;
            _registryType = registryType;
            _codeListService = codeListService;
        }
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        protected override void InitializeValidationRules()
        {
            //AddValidationRule(KodeListValidationEnum.utfylt, null);
            //AddValidationRule(KodeListValidationEnum.kode_KanIkkeValidere, null);
            //AddValidationRule(KodeListValidationEnum.kodeverdi_utfylt, "kodeverdi");
            //AddValidationRule(KodeListValidationEnum.kodeverdi_gyldig, "kodeverdi");
            //AddValidationRule(KodeListValidationEnum.kodebeskrivelse_utfylt, "kodebeskrivelse");
            //AddValidationRule(KodeListValidationEnum.kodebeskrivelse_gyldig, "kodebeskrivelse");

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
                        AddMessageFromRule(ValidationRuleEnum.kode_KanIkkeValidere, $"{xpath}/kodeverdi");
                    }
                    else
                    {
                        if (!isCodeValid.GetValueOrDefault())
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xpath}/kodeverdi", new[] { kodeEntry.ModelData?.Kodeverdi });
                        }
                    }

                    if (string.IsNullOrEmpty(kodeEntry.ModelData.Kodebeskrivelse))
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/kodebeskrivelse");
                    }
                    else
                    {
                        if (isCodeValid.GetValueOrDefault())
                        {
                            var isCodelistLabelValid = _codeListService.IsCodelistLabelValid(_codeListName,kodeEntry.ModelData?.Kodeverdi, kodeEntry.ModelData?.Kodebeskrivelse, _registryType);
                            if (!isCodelistLabelValid.GetValueOrDefault())
                            {
                                AddMessageFromRule(ValidationRuleEnum.kodeliste_gyldig, xpath, new[] { kodeEntry.ModelData?.Kodeverdi, kodeEntry.ModelData?.Kodebeskrivelse });
                            }
                        }
                    }
                }
            }
            return ValidationResult;
        }
    }
}
