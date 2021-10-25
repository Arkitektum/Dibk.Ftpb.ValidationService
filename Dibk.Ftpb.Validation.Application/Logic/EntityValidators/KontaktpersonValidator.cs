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
    public class KontaktpersonValidator : EntityValidatorBase, IKontaktpersonValidator
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        public KontaktpersonValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId)
            : base(entityValidatorTree, nodeId)
        {
        }
        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);
            AddValidationRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.telefonnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);
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

                if (string.IsNullOrEmpty(kontaktperson?.Epost))
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);

                if (string.IsNullOrEmpty(kontaktperson?.Telefonnummer) && string.IsNullOrEmpty(kontaktperson?.Mobilnummer))
                {
                    AddMessageFromRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.telefonnummer);
                }
                else
                {
                    if (!string.IsNullOrEmpty(kontaktperson.Telefonnummer))
                    {
                        var telefonNumber = kontaktperson.Telefonnummer;
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
            }

            return _validationResult;
        }
    }
}
