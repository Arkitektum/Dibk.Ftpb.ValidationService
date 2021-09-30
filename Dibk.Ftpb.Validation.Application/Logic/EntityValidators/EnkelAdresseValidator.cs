using System;
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
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt);
            }
            else
            {
                ValidateEntityFields(enkelAdresse);
            }

            return _validationResult;
        }

        public void ValidateEntityFields(EnkelAdresseValidationEntity enkelAdresse)
        {
            if (string.IsNullOrEmpty(enkelAdresse.Adresselinje1))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.adresselinje1);
            }
            else
            {

                if (!CountryCodeHandler.IsCountryNorway(enkelAdresse.Landkode))
                {
                    if (!CountryCodeHandler.VerifyCountryCode(enkelAdresse.Landkode))
                    {
                        AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.landkode);
                    }
                }
                else
                {
                    var postNr = enkelAdresse.Postnr;
                    var landkode = enkelAdresse.Landkode;

                    if (string.IsNullOrEmpty(postNr))
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, FieldNameEnum.postnr);
                    }
                    else
                    {
                        Match isPostNrValid = Regex.Match(postNr, "^([0-9])([0-9])([0-9])([0-9])$");
                        if (!isPostNrValid.Success)
                        {
                            AddMessageFromRule(ValidationRuleEnum.kontrollsiffer, FieldNameEnum.postnr, new[] { postNr });
                        }
                        else
                        {
                            var postnrValidation = _postalCodeService.ValidatePostnr(postNr, landkode);
                            if (postnrValidation != null)
                            {
                                if (!postnrValidation.Valid)
                                {
                                    AddMessageFromRule(ValidationRuleEnum.gyldig, FieldNameEnum.postnr, new[] { postNr });
                                }
                                else
                                {
                                    if (!postnrValidation.Result.Equals(enkelAdresse.Poststed, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        AddMessageFromRule(ValidationRuleEnum.postnr_stemmerIkke, FieldNameEnum.postnr, new[] { postNr, enkelAdresse.Poststed, postnrValidation.Result });
                                    }
                                }
                            }
                            else
                            {
                                AddMessageFromRule(ValidationRuleEnum.validert, FieldNameEnum.postnr);
                            }
                        }
                    }
                }
            }

        }
    }
}
