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

        public ValidationResult Validate(Saksnummer saksnummerStuff)
        {
            base.ResetValidationMessages();

            if (Helpers.ObjectIsNullOrEmpty(saksnummerStuff))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (Helpers.ObjectIsNullOrEmpty(saksnummerStuff.Saksaar))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.saksaar);
                }

                if (Helpers.ObjectIsNullOrEmpty(saksnummerStuff.Sakssekvensnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.sakssekvensnummer);
                }

            }
            return ValidationResult;
        }
    }
}
