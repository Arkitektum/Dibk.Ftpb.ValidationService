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
        private readonly FtbKodeListeEnums _codeListName;
        protected ICodeListService _codeListService;

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KodelisteValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, FtbKodeListeEnums codeListName ,ICodeListService codeListService)
                   : base(entityValidatorTree, nodeId)
        {
            _codeListName = codeListName;
            _codeListService = codeListService;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(KodeListValidationEnums.utfylt, null);
            AddValidationRule(KodeListValidationEnums.kodeverdi_utfylt, "kodeverdi");
            AddValidationRule(KodeListValidationEnums.kodeverdi_ugyldig, "kodeverdi");
            AddValidationRule(KodeListValidationEnums.kodebeskrivelse_utfylt, "kodebeskrivelse");
            AddValidationRule(KodeListValidationEnums.kodebeskrivelse_ugyldig, "kodebeskrivelse");
        }

        public ValidationResult Validate(KodelisteValidationEntity kodeliste)
        {
            var xpath = kodeliste.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(kodeliste?.ModelData))
            {
                AddMessageFromRule(KodeListValidationEnums.utfylt, xpath);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(kodeliste.ModelData.Kodeverdi))
                {
                    AddMessageFromRule(KodeListValidationEnums.kodeverdi_utfylt, xpath);
                }
                else
                {
                    if (!_codeListService.IsCodelistValid(_codeListName, kodeliste.ModelData?.Kodeverdi))
                    {
                        AddMessageFromRule(KodeListValidationEnums.kodeverdi_ugyldig, xpath, new[] { kodeliste.ModelData?.Kodeverdi });
                    }
                }
            }
            return ValidationResult;
        }
    }
}
