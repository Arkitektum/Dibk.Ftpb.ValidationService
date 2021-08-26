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
            AddValidationRule(ValidationRuleEnum.utfylt, "adresselinje1");
            AddValidationRule(ValidationRuleEnum.utfylt, "landkode");
            AddValidationRule(ValidationRuleEnum.utfylt, "postnr");
            AddValidationRule(ValidationRuleEnum.gyldig, "postnr");
        }

        public ValidationResult Validate(EnkelAdresseValidationEntity enkelAdresse = null)
        {
            var xpath = enkelAdresse.DataModelXpath;
            if (Helpers.ObjectIsNullOrEmpty(enkelAdresse?.ModelData))
            {
                AddMessageFromRule(EnkelAdresseValidationEnum.utfylt, xpath);
            }
            else
            {
                ValidateEntityFields(enkelAdresse);
            }

            return _validationResult;
        }

        public void ValidateEntityFields(EnkelAdresseValidationEntity adresseValidationEntity)
        {
            var xPath = adresseValidationEntity.DataModelXpath;
            var adresse = adresseValidationEntity.ModelData;

            if (string.IsNullOrEmpty(adresse.Adresselinje1))
            {
                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/adresselinje1");
            }
            else
            {
                if (!CountryCodeHandler.IsCountryNorway(adresse.Landkode))
                {
                    if (!CountryCodeHandler.VerifyCountryCode(adresse.Landkode))
                    {
                        //AddMessageFromRule(EnkelAdresseValidationEnum.landkode_ugyldug, xPath);
                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/landkode");
                    }
                }
                else
                {
                    var postNr = adresse.Postnr;
                    var landkode = adresse.Landkode;

                    if (string.IsNullOrEmpty(postNr))
                    {
                        AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/postnr");
                    }
                    else
                    {
                        Match isPostNrValid = Regex.Match(postNr, "^([0-9])([0-9])([0-9])([0-9])$");
                        if (!isPostNrValid.Success)
                        {
                            AddMessageFromRule(ValidationRuleEnum.postnr_kontrollsiffer, $"{xPath}/postnr", new[] { postNr });
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(landkode))
                            {
                                AddMessageFromRule(ValidationRuleEnum.utfylt, $"{xPath}/landkode", new[] { postNr });
                            }
                            else
                            {
                                var postnrValidation = _postalCodeService.ValidatePostnr(postNr, landkode);
                                if (postnrValidation != null)
                                {
                                    if (!postnrValidation.Valid)
                                    {
                                        AddMessageFromRule(ValidationRuleEnum.gyldig, $"{xPath}/postnr", new[] { postNr });
                                    }
                                    else
                                    {
                                        if (!postnrValidation.Result.Equals(adresse.Poststed, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            AddMessageFromRule(ValidationRuleEnum.postnr_stemmerIkke, $"{xPath}/postnr", new[] { postNr, adresse.Poststed, postnrValidation.Result });
                                        }
                                    }
                                }
                                else
                                {
                                    AddMessageFromRule(ValidationRuleEnum.postnr_ikke_validert, $"{xPath}/postnr");
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}
