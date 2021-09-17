﻿using System;
using System.Collections.Generic;
using Dibk.Ftpb.Validation.Application.Enums.ValidationEnums;
using Dibk.Ftpb.Validation.Application.Logic.Interfaces;
using Dibk.Ftpb.Validation.Application.Models.ValidationEntities;
using Dibk.Ftpb.Validation.Application.Reporter;
using Dibk.Ftpb.Validation.Application.Utils;
using Dibk.Ftpb.Validation.Application.Logic.EntityValidators.Common;
using System.Text.RegularExpressions;
using Dibk.Ftpb.Validation.Application.DataSources.ApiServices.PostalCode;
using Dibk.Ftpb.Validation.Application.Logic.GeneralValidations;
using Dibk.Ftpb.Validation.Application.Enums;

namespace Dibk.Ftpb.Validation.Application.Logic.EntityValidators
{
    public class EnkelAdresseValidator : EntityValidatorBase, IEnkelAdresseValidator
    {
        private readonly IPostalCodeService _postalCodeService;

        ValidationResult IEnkelAdresseValidator.ValidationResult { get => _validationResult; set => throw new System.NotImplementedException(); }

        public EnkelAdresseValidator(IList<EntityValidatorNode> entityValidatorTree, int nodeId, IPostalCodeService postalCodeService)
            : base(entityValidatorTree, nodeId)
        {
            _postalCodeService = postalCodeService;
        }

        protected override void InitializeValidationRules()
        {
            AddValidationRule(ValidationRuleEnum.utfylt);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje1);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.landkode);
            AddValidationRule(ValidationRuleEnum.utfylt, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.gyldig, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.postnr_stemmerIkke, FieldNameEnum.postnr);
            AddValidationRule(ValidationRuleEnum.validert, FieldNameEnum.postnr);
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse)
        {
            var xPath = enkelAdresse?.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, xPath);
            }
            else
            {
                ValidateEntityFields(enkelAdresse);
            }

            return _validationResult;
        }

        public void ValidateEntityFields(EnkelAdresseValidationEntity enkelAdresse)
        {
            var adresse = enkelAdresse.ModelData;
            var xPath = enkelAdresse.DataModelXpath;

            if (string.IsNullOrEmpty(adresse.Adresselinje1))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.adresselinje1}");
            }
            else
            {

                if (!CountryCodeHandler.IsCountryNorway(adresse.Landkode))
                {
                    if (!CountryCodeHandler.VerifyCountryCode(adresse.Landkode))
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/{FieldNameEnum.landkode}");
                    }
                }
                else
                {
                    var postNr = adresse.Postnr;
                    var landkode = adresse.Landkode;

                    if (string.IsNullOrEmpty(postNr))
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/{FieldNameEnum.postnr}");
                    }
                    else
                    {
                        Match isPostNrValid = Regex.Match(postNr, "^([0-9])([0-9])([0-9])([0-9])$");
                        if (!isPostNrValid.Success)
                        {
                            AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, $"{xPath}/{FieldNameEnum.postnr}", new[] { postNr });
                        }
                        else
                        {
                            var postnrValidation = _postalCodeService.ValidatePostnr(postNr, landkode);
                            if (postnrValidation != null)
                            {
                                if (!postnrValidation.Valid)
                                {
                                    AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/{FieldNameEnum.postnr}", new[] { postNr });
                                }
                                else
                                {
                                    if (!postnrValidation.Result.Equals(adresse.Poststed, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        AddMessageFromRule(ValidationRuleEnum.postnr_stemmerIkke, $"{xPath}/{FieldNameEnum.postnr}", new[] { postNr, adresse.Poststed, postnrValidation.Result });
                                    }
                                }
                            }
                            else
                            {
                                AddMessageFromRule(ValidationRuleEnum.validert, $"{xPath}/{FieldNameEnum.postnr}");
                            }
                        }
                    }
                }
            }

        }
    }
}
