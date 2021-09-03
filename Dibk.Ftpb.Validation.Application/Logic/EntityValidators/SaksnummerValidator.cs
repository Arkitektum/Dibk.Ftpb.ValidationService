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
    public class SaksnummerValidator : EntityValidatorBase, ISaksnummerValidator
    {
        private readonly object _codeListName;
        private readonly RegistryType _registryType;
        protected ICodeListService _codeListService;

        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public SaksnummerValidator(IList<EntityValidatorNode> entityValidatorTree)
                   : base(entityValidatorTree)
        {
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.saksaar);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.sakssekvensnummer);
        }

        public ValidationResult Validate(SaksnummerValidationEntity saksnummerStuff)
        {
            base.ResetValidationMessages();

            var xpath = saksnummerStuff.DataModelXpath;

            if (Helpers.ObjectIsNullOrEmpty(saksnummerStuff?.ModelData))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xpath);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(saksnummerStuff.ModelData.Saksaar))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/{FieldNameEnum.saksaar}");
                }

                if (Helpers.ObjectIsNullOrEmpty(saksnummerStuff.ModelData.Sakssekvensnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xpath}/{FieldNameEnum.sakssekvensnummer}");
                }

            }
            return ValidationResult;
        }
    }
}
