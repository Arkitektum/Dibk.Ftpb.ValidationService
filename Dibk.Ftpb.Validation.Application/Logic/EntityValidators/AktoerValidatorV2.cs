﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.CodeList;
using Dibk.Ftpb.Validation.Application.Enums;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public abstract class AktoerValidatorV2 : EntityValidatorBase, IAktoerValidatorV2
    {
        public ValidationResult ValidationResult { get => _validationResult; set => throw new NotImplementedException(); }

        protected ICodeListService _codeListService;

        protected IEnkelAdresseValidator _enkelAdresseValidator;
        protected IKontaktpersonValidator _kontaktpersonValidator;
        protected IKodelisteValidator _partstypeValidator;

        public AktoerValidatorV2(IList<EntityValidatorNode> entityValidatorTree, IEnkelAdresseValidator enkelAdresseValidator,
            IKontaktpersonValidator kontaktpersonValidator, IKodelisteValidator partstypeValidator, ICodeListService codeListService)
            : base(entityValidatorTree)
        {
            _codeListService = codeListService;
            _enkelAdresseValidator = enkelAdresseValidator;
            _kontaktpersonValidator = kontaktpersonValidator;
            _partstypeValidator = partstypeValidator;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);

            AddValidationRule(ValidationRuleEnum.utfylt, null, $"{_entityXPath}/partstype/{FieldNameEnum.kodeverdi}");
            AddValidationRule(ValidationRuleEnum.gyldig, null, $"{_entityXPath}/partstype/{FieldNameEnum.kodeverdi}");

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.foedselsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.foedselsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.foedselsnummer);
            AddValidationRule(ValidationRuleEnum.dekryptering, FieldNameEnum.foedselsnummer);

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);

            AddValidationRule(ValidationRuleEnum.telmob_utfylt);

            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);
        }


        public ValidationResult Validate(AktoerV2 aktoer = null)
        {
            if (Helpers.ObjectIsNullOrEmpty(aktoer))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                var aktoerPartsType = aktoer.Partstype;

                if (string.IsNullOrEmpty(aktoerPartsType.Kodeverdi))
                {
                    AddMessageFromRule(ValidationRuleEnum.utfylt, $"{_entityXPath}/partstype/{FieldNameEnum.kodeverdi}");
                }
                else
                {
                    if (!aktoerPartsType.Kodeverdi.Equals("Foretak") && !aktoerPartsType.Kodeverdi.Equals("Organisasjon"))
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{_entityXPath}/partstype/{FieldNameEnum.kodeverdi}");
                        //var partstypeValidatinResults = _partstypeValidator.Validate(aktoerPartsType);
                        //UpdateValidationResultWithSubValidations(partstypeValidatinResults);
                    }
                    else
                    {
                        ValidateEntityFields(aktoer);

                    }
                }
            }
            return _validationResult;
        }

        private void ValidateEntityFields(AktoerV2 aktoer)
        {
            var organisasjonsnummerValidation = NorskStandardValidator.Validate_OrgnummerEnum(aktoer.Organisasjonsnummer);
            switch (organisasjonsnummerValidation)
            {
                case OrganisasjonsnummerValidation.Empty:
                    AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.organisasjonsnummer);
                    break;
                case OrganisasjonsnummerValidation.InvalidDigitsControl:
                    AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.organisasjonsnummer);
                    break;
                case OrganisasjonsnummerValidation.Invalid:
                    AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.organisasjonsnummer);
                    break;
            }

       
            if (string.IsNullOrEmpty(aktoer.Navn))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.navn);

            if (string.IsNullOrEmpty(aktoer.Telefonnummer) && string.IsNullOrEmpty(aktoer.Mobilnummer))
            {
                AddMessageFromRule(ValidationRuleEnum.telmob_utfylt, FieldNameEnum.mobilnummer);
            }
            else
            {
                if (!string.IsNullOrEmpty(aktoer.Telefonnummer))
                {
                    var telefonNumber = aktoer?.Telefonnummer;
                    var isValidTelefonNumber = telefonNumber.All(c => "+0123456789".Contains(c));
                    if (!isValidTelefonNumber)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.telefonnummer);
                    }
                }
                if (!string.IsNullOrEmpty(aktoer.Mobilnummer))
                {
                    var mobilNummer = aktoer.Mobilnummer;
                    var isValidmobilnummer = mobilNummer.All(c => "+0123456789".Contains(c));
                    if (!isValidmobilnummer)
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.mobilnummer);
                    }
                }
            }

            if (string.IsNullOrEmpty(aktoer.Epost))
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.epost);

            var enkeladressResult = _enkelAdresseValidator.Validate(aktoer.Adresse);
            UpdateValidationResultWithSubValidations(enkeladressResult);
            
            var kontaktpersonValidationResult = _kontaktpersonValidator.Validate(aktoer.Kontaktperson);
            UpdateValidationResultWithSubValidations(kontaktpersonValidationResult);

          

        }
    }
}
