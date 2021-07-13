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
            AddValidationRule(KodeListValidationEnum.utfylt, null);
            AddValidationRule(KodeListValidationEnum.kode_KanIkkeValidere, null);
            AddValidationRule(KodeListValidationEnum.kodeverdi_utfylt, "kodeverdi");
            AddValidationRule(KodeListValidationEnum.kodeverdi_gyldig, "kodeverdi");
            AddValidationRule(KodeListValidationEnum.kodebeskrivelse_utfylt, "kodebeskrivelse");
            AddValidationRule(KodeListValidationEnum.kodebeskrivelse_gyldig, "kodebeskrivelse");
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeliste)
        {
            var xpath = kodeliste.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(kodeliste?.ModelData))
            {
                AddMessageFromRule(KodeListValidationEnum.utfylt, xpath);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(kodeliste.ModelData.Kodeverdi))
                {
                    AddMessageFromRule(KodeListValidationEnum.kodeverdi_utfylt, xpath);
                }
                else
                {
                    var isCodeValid = _codeListService.IsCodelistValid(_codeListName, kodeliste.ModelData?.Kodeverdi, _registryType);
                    if (!isCodeValid.HasValue)
                    {
                        AddMessageFromRule(KodeListValidationEnum.kode_KanIkkeValidere, xpath);
                    }
                    else
                    {
                        if (!isCodeValid.GetValueOrDefault())
                            AddMessageFromRule(KodeListValidationEnum.kodeverdi_gyldig, xpath, new[] { kodeliste.ModelData?.Kodeverdi });
                    }

                    if (string.IsNullOrEmpty(kodeliste.ModelData.Kodebeskrivelse))
                    {
                        AddMessageFromRule(KodeListValidationEnum.kodebeskrivelse_utfylt, xpath);
                    }
                    else
                    {
                        if (isCodeValid.GetValueOrDefault())
                        {
                            var isCodelistLabelValid = _codeListService.IsCodelistLabelValid(_codeListName,kodeliste.ModelData?.Kodeverdi, kodeliste.ModelData?.Kodebeskrivelse, _registryType);
                            if (!isCodelistLabelValid.GetValueOrDefault())
                            {
                                AddMessageFromRule(KodeListValidationEnum.kodeverdi_gyldig, xpath, new[] { kodeliste.ModelData?.Kodeverdi, kodeliste.ModelData?.Kodebeskrivelse });
                            }
                        }
                    }
                }
            }
            return ValidationResult;
        }
    }
}
