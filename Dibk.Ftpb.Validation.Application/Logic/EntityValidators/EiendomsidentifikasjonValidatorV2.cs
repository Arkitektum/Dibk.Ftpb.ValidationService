using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.DataSources;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EiendomsidentifikasjonValidatorV2 : EntityValidatorBase, IMatrikkelValidator
    {
        private readonly ICodeListService _codeListService;
        ValidationResult IMatrikkelValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EiendomsidentifikasjonValidatorV2(IList<EntityValidatorNode> entityValidatorTree, ICodeListService codeListService)
            : base(entityValidatorTree)
        {
            _codeListService = codeListService;
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.kommunenummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.kommunenummer);
            AddValidationRule(ValidationRuleEnum.status, FieldNameEnum.kommunenummer);
            AddValidationRule(ValidationRuleEnum.validert, FieldNameEnum.kommunenummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.gaardsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.gaardsnummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.bruksnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.bruksnummer);

        }

        public ValidationResult Validate(Matrikkel matrikkel)
        {
            base.ResetValidationMessages();

            if (Helpers.ObjectIsNullOrEmpty(matrikkel))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                var kommunenummer = matrikkel?.Kommunenummer;

                var codelistTagValue = _codeListService.GetCodelistTagValue(SosiKodelisterEnum.kommunenummer, kommunenummer, RegistryType.SosiKodelister);

                switch (codelistTagValue?.Status?.ToLower())
                {
                    case "gyldig":
                        break;
                    case "utfylt":
                        AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.kommunenummer);
                        break;
                    case "ugyldig":
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.kommunenummer);
                        break;
                    case "IkkeValidert":
                        AddMessageFromRule(ValidationRuleEnum.validert, FieldNameEnum.kommunenummer, new[] { kommunenummer });
                        break;
                    default:
                        AddMessageFromRule(ValidationRuleEnum.status, FieldNameEnum.kommunenummer, new[] { kommunenummer, codelistTagValue?.Status });
                        break;
                }

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.Gaardsnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.gaardsnummer);
                }
                else
                {
                    int gaardsnummer = 0;
                    if (int.TryParse(matrikkel.Gaardsnummer, out gaardsnummer))
                    {
                        if (gaardsnummer <= 0)
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.gaardsnummer, new []{matrikkel.Gaardsnummer});
                    }
                }

                if (Helpers.ObjectIsNullOrEmpty(matrikkel.Bruksnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.bruksnummer);
                }
                else
                {
                    int bruksnummer = 0;
                    if (int.TryParse(matrikkel.Bruksnummer, out bruksnummer))
                    {
                        if (bruksnummer <= 0)
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.bruksnummer, new []{matrikkel.Bruksnummer});
                    }
                }
            }
            return _validationResult;
        }
    }
}
