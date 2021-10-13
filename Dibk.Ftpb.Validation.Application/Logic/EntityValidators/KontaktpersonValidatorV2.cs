using System;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class KontaktpersonValidatorV2 : EntityValidatorBase, IKontaktpersonValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KontaktpersonValidatorV2(IList<EntityValidatorNode> entityValidatorTree, int nodeId)
            : base(entityValidatorTree, nodeId)
        {
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
            AddValidationRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.mobilnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);
        }

        public ValidationResult Validate(KontaktpersonValidationEntity kontaktperson = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(kontaktperson))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                if (string.IsNullOrEmpty(kontaktperson?.Navn))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);

                if (string.IsNullOrEmpty(kontaktperson.Telefonnummer) && string.IsNullOrEmpty(kontaktperson.Mobilnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.mobilnummer);
                }
                else
                {
                    if (!string.IsNullOrEmpty(kontaktperson.Telefonnummer))
                    {
                        var telefonNumber = kontaktperson?.Telefonnummer;
                        var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                        if (!isValidTelefonNumber)
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);
                        }
                    }
                    if (!string.IsNullOrEmpty(kontaktperson.Mobilnummer))
                    {
                        var mobilNummer = kontaktperson.Mobilnummer;
                        var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                        if (!isValidmobilnummer)
                        {
                            AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);
                        }
                    }
                }
                if (string.IsNullOrEmpty(kontaktperson?.Epost))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);
            }
            return _validationResult;
        }

    }
}
